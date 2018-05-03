using System.Windows;
using DevExpress.Mvvm.UI;
using Ingress.WPF.ViewModels;

namespace Ingress.WPF.Converters
{
    public class DataContextChange
    {
        public DataContextChange(SelectableViewModel old, SelectableViewModel @new)
        {
            Old = old;
            New = @new;
        }

        public SelectableViewModel Old { get; }
        public SelectableViewModel New { get; }
    }

    public class DataContextChangedEventConverter : EventArgsConverterBase<DependencyPropertyChangedEventArgs>
    {
        protected override object Convert(object sender, DependencyPropertyChangedEventArgs args)
        {
            return new DataContextChange(args.OldValue as SelectableViewModel, args.NewValue as SelectableViewModel);
        }
    }
}