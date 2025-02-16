namespace Utilities.Validators;

public static class EnumsValidator
{
    public static bool IsDefinedInEnum<TEnum, TValue>(TValue value) where TEnum : Enum
    {
        return Enum.IsDefined(typeof(TEnum), value);
    }
}
