using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SiLadhida.App.Components.Converters;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        decimal d;

        if (value is decimal dec) d = dec;
        else if (value is double db) d = (decimal)db;
        else if (value is int i) d = i;
        else if (value is long l) d = l;
        else if (value is float f) d = (decimal)f;
        else if (value != null &&
                 decimal.TryParse(value.ToString(), NumberStyles.Any,
                     CultureInfo.InvariantCulture, out var parsed)) d = parsed;
        else return "Rp 0";

        // Bulatkan ke rupiah penuh, format ribuan pakai TITIK
        long whole = (long)Math.Round(d, MidpointRounding.AwayFromZero);
        string formatted = whole.ToString("#,##0", CultureInfo.InvariantCulture)
                                .Replace(',', '.');   // 24,000 -> 24.000

        return $"Rp {formatted}";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value ?? "";
}