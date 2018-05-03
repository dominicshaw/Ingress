using System.Windows;
using Jot;

namespace Ingress.WPF.Layouts
{
    public class WindowLayoutManager
    {
        private readonly StateTracker _tracker;

        public WindowLayoutManager(StateTracker tracker)
        {
            _tracker = tracker;
        }

        public void ApplyJot(Window window)
        {
            _tracker.Configure(window)
                .IdentifyAs(GetType().ToString())
                .AddProperties<Window>(w => w.Height, w => w.Width, w => w.Left, w => w.Top, w => w.WindowState)
                .RegisterPersistTrigger("Closed")
                .Apply();
        }
    }
}