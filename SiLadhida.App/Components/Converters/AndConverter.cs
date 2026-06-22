using Avalonia.Data.Converters;
using System;
using System.Collections.Generic; // 🔥 WAJIB
using System.Globalization;

namespace SiLadhida.App.Components.Converters;

public class AndConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var v in values)
        {
            if (v is bool b && b == false)
                return false;
        }

        return true;
    }
}