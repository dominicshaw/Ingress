using System.Windows;
using DevExpress.Mvvm.UI;
using Ingress.WPF.ViewModels;

namespace Ingress.WPF.Converters
{
    public class DataContextChangedFromEventConverter : EventArgsConverterBase<DependencyPropertyChangedEventArgs>
    {
        protected override object Convert(object sender, DependencyPropertyChangedEventArgs args)
        {
            return args.OldValue as SelectableViewModel;
        }
    }
}