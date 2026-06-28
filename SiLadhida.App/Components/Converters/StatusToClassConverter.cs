using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SiLadhida.App.Components.Converters;

public class StatusToClassConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Console.WriteLine("=== CONVERTER CALLED ===");

        if (value == null)
        {
            Console.WriteLine("Value NULL");
            return "";
        }

        Console.WriteLine($"Value masuk: {value}");

        var result = value.ToString() switch
        {
            "MenungguPembayaran" => "status-pending",
            "SiapDiambil" => "status-ready",
            "Selesai" => "status-done",
            "Dibatalkan" => "status-cancel",
            _ => ""
        };

        Console.WriteLine($"Class hasil: {result}");
        Console.WriteLine("========================");

        return result;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}