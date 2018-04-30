using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace Ingress.WPF.Views.Controls
{
    public partial class DateAndTimeRangeControl : INotifyPropertyChanged
    {
        public DateTime DateStart
        {
            get { return (DateTime)this.GetValue(DateStartProperty); }
            set { this.SetValue(DateStartProperty, value); }
        }
        public DateTime DateEnd
        {
            get { return (DateTime)this.GetValue(DateEndProperty); }
            set { this.SetValue(DateEndProperty, value); }
        }

        public static readonly DependencyProperty DateStartProperty = DependencyProperty.Register(
            "DateStart",
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty DateEndProperty = DependencyProperty.Register(
            "DateEnd",
            typeof(DateTime),
            typeof(DateAndTimeRangeControl),
            new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });
        
        public DateAndTimeRangeControl()
        {
            InitializeComponent();
        }

        public DateTime DatePart
        {
            get
            {
                return DateStart;
            }
            set
            {
                DateStart = new DateTime(value.Year, value.Month, value.Day, TimePart1.Hour, TimePart1.Minute, 0);
                DateEnd = new DateTime(value.Year, value.Month, value.Day, TimePart2.Hour, TimePart2.Minute, 0);

                OnPropertyChanged();
            }
        }

        public DateTime TimePart1
        {
            get
            {
                return DateStart;
            }
            set
            {
                DateStart = new DateTime(DatePart.Year, DatePart.Month, DatePart.Day, value.Hour, value.Minute, 0);
                OnPropertyChanged();
            }
        }

        public DateTime TimePart2
        {
            get
            {
                return DateEnd;
            }
            set
            {
                DateEnd = new DateTime(DatePart.Year, DatePart.Month, DatePart.Day, value.Hour, value.Minute, 0);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
