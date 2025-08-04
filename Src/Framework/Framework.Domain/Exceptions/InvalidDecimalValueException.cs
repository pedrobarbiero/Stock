namespace Framework.Domain.Exceptions;

using System.Globalization;

public class InvalidDecimalValueException(decimal value)
    : DomainException($"Value must be positive. Current: {value.ToString("F4", CultureInfo.CurrentCulture)}");