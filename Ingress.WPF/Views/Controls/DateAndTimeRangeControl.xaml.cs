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
            new FrameworkPropertyMetadata  { BindsTwoWayByDefault = true });
        
        public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(
            nameof(StartTime),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata(OnStartTimeChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(
            nameof(EndDate),
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata{  BindsTwoWayByDefault = true });

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
            get => (DateTime)this.GetValue(StartDateProperty);
            set => this.SetValue(StartDateProperty, value);
        }
        
        public DateTime StartTime
        {
            get => (DateTime) this.GetValue(StartTimeProperty);
            set => this.SetValue(StartTimeProperty, value);
        }

        public DateTime EndDate
        {
            get => (DateTime)this.GetValue(EndDateProperty);
            set => this.SetValue(EndDateProperty, value);
        }

        public DateTime EndTime
        {
            get => (DateTime) this.GetValue(EndTimeProperty);
            set => this.SetValue(EndTimeProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime) this.GetValue(DateProperty);
            set => this.SetValue(DateProperty, value);
        }

        private static void OnStartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control &&
                e.NewValue is DateTime time)
            {
                var date = control.Date;
                control.SetCurrentValue(StartDateProperty, new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0));
            }
        }

        private static void OnEndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control &&
                e.NewValue is DateTime time)
            {
                var date = control.Date;
                control.SetCurrentValue(EndDateProperty, new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0));
            }
        }

        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateAndTimeRangeControl control &&
                e.NewValue is DateTime date)
            {
                var startTime = control.StartTime;
                control.SetCurrentValue(StartDateProperty, new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0));

                var endTime = control.EndTime;
                control.SetCurrentValue(StartDateProperty, new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, 0));
            }
        }
    }
}
