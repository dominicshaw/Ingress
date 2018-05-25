using System;
using System.Windows;

namespace Ingress.WPF.Views.Controls
{
    public partial class DateAndTimeRangeControl
    {
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(
            nameof(StartDate),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnStartDateChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(
            nameof(EndDate),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });

        public DateAndTimeRangeControl()
        {
            InitializeComponent();
        }

        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        private static void OnStartDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control && e.NewValue is DateTime dt)
            {
                if (dt == default(DateTime))
                    return;

                control.SetCurrentValue(EndDateProperty, CreateDateTime(dt, control.EndDate));
            }
        }

        private static DateTime CreateDateTime(DateTime date, DateTime time) => new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
    }
}
