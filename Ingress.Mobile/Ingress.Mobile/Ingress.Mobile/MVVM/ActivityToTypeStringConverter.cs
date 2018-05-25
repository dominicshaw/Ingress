using System;
using System.Globalization;
using Ingress.DTOs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.MVVM
{
    public class ActivityToTypeStringConverter : IMarkupExtension, IValueConverter
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case AnalystMeetingDTO _:
                    return "Analyst Meeting";
                case BrokerEmailDTO _:
                    return "Email";
                case CompanyMeetingDTO _:
                    return "Company Meeting";
                case ModelAccessDTO _:
                    return "Model Access";
                case PhoneCallDTO _:
                    return "Phone Call";
                default:
                    return "Unknown Type";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}