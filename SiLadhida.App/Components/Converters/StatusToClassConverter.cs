using Avalonia.Data.Converters;
using SiLadhida.Core.Enums;
using System;
using System.Globalization;

namespace SiLadhida.App.Components.Converters;

public class StatusToClassConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is StateOrder status)
        {
            return status switch
            {
                StateOrder.MenungguPembayaran => "status-pending",
                StateOrder.SiapDiambil => "status-ready",
                StateOrder.Selesai => "status-done",
                StateOrder.Dibatalkan => "status-cancel",
                _ => ""
            };
        }

        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}