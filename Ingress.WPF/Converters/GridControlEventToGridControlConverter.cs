using System;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;

namespace Ingress.WPF.Converters
{
    public class GridControlEventToGridControlConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs args)
        {
            return sender as GridControl;
        }
    }
}