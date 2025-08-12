using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Stock.Api.Swagger;

public class FluentValidationRules : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type is null) return;

        var validator = CreateValidator(context.Type);
        if (validator is null) return;

        ApplyValidationRules(schema, context, validator);
    }

    private static IValidator? CreateValidator(Type type)
    {
        // Create validator instances directly by finding the validator type in loaded assemblies
        var validatorType = FindValidatorType(type);
        if (validatorType == null) return null;

        try
        {
            // Create instance directly - validators typically have parameterless constructors
            return Activator.CreateInstance(validatorType) as IValidator;
        }
        catch
        {
            return null;
        }
    }

    private static Type? FindValidatorType(Type modelType)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            // try
            // {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface) continue;

                // Check if this type implements IValidator<T> for our model type
                var interfaces = type.GetInterfaces();
                foreach (var iface in interfaces)
                {
                    if (iface.IsGenericType &&
                        iface.GetGenericTypeDefinition() == typeof(IValidator<>) &&
                        iface.GetGenericArguments()[0] == modelType)
                    {
                        return type;
                    }
                }

                // Also check inheritance chain for AbstractValidator<T>
                var baseType = type.BaseType;
                while (baseType != null)
                {
                    if (baseType.IsGenericType &&
                        baseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>) &&
                        baseType.GetGenericArguments()[0] == modelType)
                    {
                        return type;
                    }

                    baseType = baseType.BaseType;
                }
            }
            // }
            // catch
            // {
            //     // Skip assemblies that can't be inspected
            // }
        }

        return null;
    }

    private static void ApplyValidationRules(OpenApiSchema schema, SchemaFilterContext context, IValidator validator)
    {
        var validatorDescriptor = validator.CreateDescriptor();

        var members = GetModelProperties(context.Type);

        foreach (var member in members)
        {
            var rules = validatorDescriptor.GetRulesForMember(member.Name);
            var propertyKey = ToCamelCase(member.Name);

            if (!schema.Properties.TryGetValue(propertyKey, out var property))
                continue;

            foreach (var rule in rules)
            {
                ApplyRuleToProperty(schema, property, rule, propertyKey);
            }
        }
    }

    private static void ApplyRuleToProperty(OpenApiSchema schema, OpenApiSchema property, IValidationRule rule,
        string propertyName)
    {
        foreach (var component in rule.Components)
        {
            ApplyValidatorToProperty(schema, property, component.Validator, propertyName);
        }
    }

    private static void ApplyValidatorToProperty(OpenApiSchema schema, OpenApiSchema property,
        IPropertyValidator propertyValidator, string propertyName)
    {
        var validatorName = propertyValidator.GetType().Name;

        switch (propertyValidator)
        {
            case ILengthValidator lengthValidator:
                if (lengthValidator.Max > 0)
                    property.MaxLength = lengthValidator.Max;
                if (lengthValidator.Min > 0)
                    property.MinLength = lengthValidator.Min;
                break;

            case IRegularExpressionValidator regexValidator:
                property.Pattern = regexValidator.Expression;
                break;

            case IEmailValidator:
                property.Format = "email";
                break;

            case IBetweenValidator betweenValidator:
                ApplyBetweenValidator(property, betweenValidator, validatorName.Contains("Exclusive"));
                break;

            case IComparisonValidator comparisonValidator:
                ApplySpecificComparisonValidator(property, comparisonValidator, validatorName);
                break;

            case var _ when validatorName.Contains("NotNull") || validatorName.Contains("NotEmpty"):
                AddRequiredProperty(schema, propertyName);
                break;
        }
    }

    private static void ApplySpecificComparisonValidator(OpenApiSchema property,
        IComparisonValidator comparisonValidator, string validatorName)
    {
        if (validatorName.Contains("GreaterThan"))
        {
            ApplyGreaterThanValidator(property, comparisonValidator, validatorName.Contains("OrEqual"));
        }
        else if (validatorName.Contains("LessThan"))
        {
            ApplyLessThanValidator(property, comparisonValidator, validatorName.Contains("OrEqual"));
        }
        else
        {
            ApplyComparisonValidator(property, comparisonValidator);
        }
    }

    private static void ApplyComparisonValidator(OpenApiSchema property, IComparisonValidator comparisonValidator)
    {
        if (comparisonValidator.ValueToCompare is null) return;

        if (decimal.TryParse(comparisonValidator.ValueToCompare.ToString(), out var value))
        {
            switch (comparisonValidator.Comparison)
            {
                case Comparison.GreaterThan:
                    property.Minimum = value;
                    property.ExclusiveMinimum = true;
                    break;
                case Comparison.GreaterThanOrEqual:
                    property.Minimum = value;
                    break;
                case Comparison.LessThan:
                    property.Maximum = value;
                    property.ExclusiveMaximum = true;
                    break;
                case Comparison.LessThanOrEqual:
                    property.Maximum = value;
                    break;
            }
        }
    }

    private static void ApplyBetweenValidator(OpenApiSchema property, IBetweenValidator validator, bool exclusive)
    {
        if (decimal.TryParse(validator.From?.ToString(), out var from))
        {
            property.Minimum = from;
            if (exclusive) property.ExclusiveMinimum = true;
        }

        if (decimal.TryParse(validator.To?.ToString(), out var to))
        {
            property.Maximum = to;
            if (exclusive) property.ExclusiveMaximum = true;
        }
    }

    private static void ApplyGreaterThanValidator(OpenApiSchema property, IComparisonValidator validator, bool orEqual)
    {
        if (validator.ValueToCompare is null) return;

        if (decimal.TryParse(validator.ValueToCompare.ToString(), out var value))
        {
            property.Minimum = value;
            property.ExclusiveMinimum = !orEqual;
        }
    }

    private static void ApplyLessThanValidator(OpenApiSchema property, IComparisonValidator validator, bool orEqual)
    {
        if (validator.ValueToCompare is null) return;

        if (decimal.TryParse(validator.ValueToCompare.ToString(), out var value))
        {
            property.Maximum = value;
            property.ExclusiveMaximum = !orEqual;
        }
    }

    private static void AddRequiredProperty(OpenApiSchema schema, string propertyName)
    {
        schema.Required ??= new HashSet<string>();
        schema.Required.Add(propertyName);

        if (schema.Properties.TryGetValue(propertyName, out var property))
        {
            property.Nullable = false;
        }
    }

    private static PropertyInfo[] GetModelProperties(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetGetMethod()?.IsPublic == true)
            .ToArray();
    }

    private static string ToCamelCase(string input)
    {
        //Todo: move to string extensions
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLowerInvariant(input[0]) + input[1..];
    }
}