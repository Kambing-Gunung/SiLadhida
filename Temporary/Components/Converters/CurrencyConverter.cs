using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SiLadhida.App.Converters;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal d)
            return $"Rp {d:N0}";

        return "Rp 0";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}