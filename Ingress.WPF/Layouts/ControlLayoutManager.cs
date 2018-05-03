using System;
using System.IO;
using System.Windows;
using DevExpress.Xpf.Core.Serialization;
using log4net;

namespace Ingress.WPF.Layouts
{
    public static class ControlLayoutManager
    {
        private static readonly LayoutManager _manager;

        static ControlLayoutManager()
        {
            _manager = new LayoutManager(LogManager.GetLogger(typeof(ControlLayoutManager)));
        }

        public static void SaveControlLayout(string controlName, DependencyObject ctrl)
        {
            using (var stream = new MemoryStream())
            {
                DXSerializer.Serialize(ctrl, stream, "Ingress", null);
                _manager.SaveLayoutAsync(Environment.UserName, "Ingress", controlName, stream);
            }
        }

        public static void RestoreControlLayout(string controlName, DependencyObject ctrl)
        {
            var data = _manager.RestoreLayout(Environment.UserName, "Ingress", controlName);

            if (data == null)
                return;

            using (var stream = new MemoryStream(data))
            {
                DXSerializer.Deserialize(ctrl, stream, "Ingress", null);
            }
        }

        public static bool Close()
        {
            return _manager.Close();
        }
    }
}