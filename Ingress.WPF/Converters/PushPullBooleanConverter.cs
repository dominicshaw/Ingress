using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ingress.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class PushPullBooleanConverter : MarkupExtension, IValueConverter
    {
        public bool Push { get; set; }
        public bool Pull { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (Push && value.ToString().ToUpper() == "PUSH")
                return true;

            if (Pull && value.ToString().ToUpper() == "PULL")
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b && Push)
                    return "Push";

                if (b && Pull)
                    return "Pull";
            }

            return string.Empty;
        }
    }
}
