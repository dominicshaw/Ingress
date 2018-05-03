using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Ingress.WPF.ViewModels.Data;

namespace Ingress.WPF.Converters
{
    public class SelectedViewIsEditableConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            return value is ActivityViewModel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(SelectedViewIsEditableConverter)} can only be used in OneWay bindings");
        }
    }
}