using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace SiLadhida.App.Components.Converters;

public class StatusToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "MenungguPembayaran" => Brushes.Gray,
            "SiapDiambil" => Brushes.Orange,
            "Selesai" => Brushes.Green,
            "Dibatalkan" => Brushes.Red,
            _ => Brushes.Transparent
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}