using System;
using System.Threading;
using log4net;

namespace Ingress.WPF.Layouts
{
    public abstract class LayoutStrategy
    {
        private readonly SemaphoreSlim _working = new SemaphoreSlim(1, 1);

        protected readonly ILog Log;

        protected LayoutStrategy(ILog log)
        {
            Log = log;
        }

        private void Block()
        {
            _working.Wait();
        }

        private void Unblock()
        {
            _working.Release();
        }

        public abstract string RestoreLayout(string username, string application, string controlId, string tabId);
        protected abstract void SaveLayout(string username, string application, string controlId, string tabId, string layoutData);

        public void SaveLayoutAsync(string username, string application, string controlId, string tabId, string layoutData)
        {
            var action = new Action(() => SaveLayout(username, application, controlId, tabId, layoutData));

            Block();
            action.BeginInvoke(o => Unblock(), null);
        }

        public bool Close()
        {
            try
            {
                _working.Wait();
                return true;
            }
            finally
            {
                _working.Release();
            }
        }
    }
}