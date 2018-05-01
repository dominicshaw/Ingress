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
            new FrameworkPropertyMetadata(OnStartDateChanged)  { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(
            nameof(StartTime),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnStartTimeChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(
            nameof(EndDate),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnEndDateChanged){  BindsTwoWayByDefault = true });

        public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(
            nameof(EndTime),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnEndTimeChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            nameof(Date),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnDateChanged) { BindsTwoWayByDefault = true });

        public DateAndTimeRangeControl()
        {
            InitializeComponent();
        }

        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }
        
        public DateTime StartTime
        {
            get => (DateTime) GetValue(StartTimeProperty);
            set => SetValue(StartTimeProperty, value);
        }

        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        public DateTime EndTime
        {
            get => (DateTime) GetValue(EndTimeProperty);
            set => SetValue(EndTimeProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime) GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        private static void OnStartDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control)
            {
                control.SetCurrentValue(DateProperty, e.NewValue);
                control.SetCurrentValue(StartTimeProperty, e.NewValue);
            }
        }

        private static void OnEndDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control)
            {
                control.SetCurrentValue(EndTimeProperty, e.NewValue);
            }
        }

        private static void OnStartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control && e.NewValue is DateTime time)
            {
                control.SetCurrentValue(StartDateProperty, CreateDateTime(control.Date, time));
            }
        }

        private static void OnEndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control && e.NewValue is DateTime time)
            {
                control.SetCurrentValue(EndDateProperty, CreateDateTime(control.Date, time));
            }
        }

        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control && e.NewValue is DateTime date)
            {
                control.SetCurrentValue(StartDateProperty, CreateDateTime(date, control.StartTime));
                control.SetCurrentValue(EndDateProperty, CreateDateTime(date, control.EndTime));
            }
        }

        private static DateTime CreateDateTime(DateTime date, DateTime time) => new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
    }
}
