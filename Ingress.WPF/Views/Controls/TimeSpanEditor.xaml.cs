using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace Ingress.WPF.Views.Controls
{
    public partial class TimeSpanEditor : INotifyPropertyChanged
    {
        public TimeSpan EditValue
        {
            get { return (TimeSpan)this.GetValue(EditValueProperty); }
            set { this.SetValue(EditValueProperty, value); } 
        }

        public static readonly DependencyProperty EditValueProperty = DependencyProperty.Register(
            "EditValue",
            typeof(TimeSpan),
            typeof(TimeSpanEditor),
            new FrameworkPropertyMetadata(new TimeSpan(0, 30, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Hours
        {
            get
            {
                return EditValue.Hours;
            }
            set
            {
                EditValue = new TimeSpan(0, value, Minutes, 0);
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get
            {
                return EditValue.Minutes;
            }
            set
            {
                EditValue = new TimeSpan(0, Hours, value, 0);
                OnPropertyChanged();
            }
        }

        public TimeSpanEditor()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
