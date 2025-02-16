namespace Utilities.Validators;

public static class NumberValidator
{
    public static bool IsNumerical(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return double.TryParse(value, out _);
    }
}
