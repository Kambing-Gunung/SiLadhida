using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SiLadhida.App.Components.Converters;

public class LoadingOpacityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Console.WriteLine($"IsLoading: {value}");

        return (value is bool isLoading && isLoading)
            ? 0.5   // 🔥 redup saat loading
            : 1.0;  // normal
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}