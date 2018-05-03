using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ingress.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertedBooleanConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {          
            if (value is bool b) 
                return !b;
            
            return null;
        }
    }
}