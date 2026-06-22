using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SiLadhida.Core.Enums;

namespace SiLadhida.App.Components.Converters;

public class IsCancelConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is StateOrder state && state == StateOrder.Dibatalkan;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}