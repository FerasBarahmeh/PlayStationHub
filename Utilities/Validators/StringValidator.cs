namespace Utilities.Validators;

public static class StringValidator
{
    public static bool NotContainSpaces(string value)
    {
        return !value.Contains(" ");
    }
}
