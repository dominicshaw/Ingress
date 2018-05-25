using System;
using System.Windows;

namespace Ingress.WPF.Views.Controls
{
    public partial class TimeSpanEditor
    {
        public static readonly DependencyProperty EditValueProperty = DependencyProperty.Register(
            nameof(EditValue),
            typeof(TimeSpan),
            typeof(TimeSpanEditor),
            new FrameworkPropertyMetadata(OnEditValueChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(
            nameof(Hours),
            typeof(int?),
            typeof(TimeSpanEditor),
            new FrameworkPropertyMetadata(OnHoursChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
            nameof(Minutes),
            typeof(int?),
            typeof(TimeSpanEditor),
            new FrameworkPropertyMetadata(OnMinutesChanged) { BindsTwoWayByDefault = true });

        public TimeSpan EditValue
        {
            get { return (TimeSpan)GetValue(EditValueProperty); }
            set { SetValue(EditValueProperty, value); }
        }

        public int? Hours
        {
            get => (int?)GetValue(HoursProperty);
            set => SetValue(HoursProperty, value);
        }

        public int? Minutes
        {
            get => (int?)GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, value);
        }

        public TimeSpanEditor()
        {
            InitializeComponent();
        }

        private static void OnEditValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TimeSpanEditor editor && e.NewValue is TimeSpan ts)
            {
                editor.SetCurrentValue(HoursProperty, ts.Hours);
                editor.SetCurrentValue(MinutesProperty, ts.Minutes);
            }
        }

        private static void OnHoursChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TimeSpanEditor editor && e.NewValue is int i)
            {
                editor.SetCurrentValue(EditValueProperty, new TimeSpan(0, i, editor.Minutes ?? 0, 0));
            }
        }

        private static void OnMinutesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TimeSpanEditor editor && e.NewValue is int i)
            {
                editor.SetCurrentValue(EditValueProperty, new TimeSpan(0, editor.Hours ?? 0, i, 0));
            }
        }
    }
}
