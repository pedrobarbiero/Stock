using System.Globalization;
using Framework.Domain.Exceptions;

namespace Framework.Domain.CustomTypes;

public readonly struct PositiveDecimal
{
    public decimal Value { get; }

    public PositiveDecimal(decimal value)
    {
        if (value < 0)
            throw new InvalidDecimalValueException(value);
        Value = value;
    }

    public static implicit operator decimal(PositiveDecimal pd) => pd.Value;
    public static explicit operator PositiveDecimal(decimal d) => new PositiveDecimal(d);

    public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

    public static PositiveDecimal operator +(PositiveDecimal a, PositiveDecimal b) =>
        new PositiveDecimal(a.Value + b.Value);

    public static PositiveDecimal operator -(PositiveDecimal a, PositiveDecimal b)
    {
        var result = a.Value - b.Value;
        return new PositiveDecimal(result); // throws if result < 0
    }

    public static PositiveDecimal operator *(PositiveDecimal a, PositiveDecimal b) =>
        new PositiveDecimal(a.Value * b.Value);

    public static PositiveDecimal operator /(PositiveDecimal a, PositiveDecimal b)
    {
        if (b.Value == 0)
            throw new DivideByZeroException();
        return new PositiveDecimal(a.Value / b.Value);
    }

    public static bool operator >(PositiveDecimal a, PositiveDecimal b) => a.Value > b.Value;
    public static bool operator <(PositiveDecimal a, PositiveDecimal b) => a.Value < b.Value;
    public static bool operator >=(PositiveDecimal a, PositiveDecimal b) => a.Value >= b.Value;
    public static bool operator <=(PositiveDecimal a, PositiveDecimal b) => a.Value <= b.Value;
    public static bool operator ==(PositiveDecimal a, PositiveDecimal b) => a.Value == b.Value;
    public static bool operator !=(PositiveDecimal a, PositiveDecimal b) => a.Value != b.Value;

    public override bool Equals(object? obj) => obj is PositiveDecimal other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}