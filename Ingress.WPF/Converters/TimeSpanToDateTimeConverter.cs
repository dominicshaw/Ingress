﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ingress.WPF.Converters
{
    public class TimeSpanToDateTimeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var ts = (TimeSpan) value;

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Hours, ts.Minutes, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
                return new TimeSpan(0, dt.Hour, dt.Minute, dt.Second);

            if (value is TimeSpan ts)
                return ts;

            throw new Exception("Could not convert '" + value?.GetType() + "' to TimeSpan.");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}