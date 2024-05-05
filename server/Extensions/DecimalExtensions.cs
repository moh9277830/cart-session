namespace Server.Extensions;

public static class DecimalExtensions
{
    public static string GetValueString(this decimal value)
    {
        return $"BHD {value.ToString("0.000")}";
    }
}