using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using log4net;

namespace Ingress.WPF.Layouts
{
    [UsedImplicitly]
    public class LayoutManager
    {
        private readonly List<LayoutStrategy> _strategies;

        public LayoutManager(ILog log) 
        {
            _strategies = new List<LayoutStrategy> { new SqlLayoutStrategy(log), new FileLayoutStrategy(log) };
        }

        private static byte[] GetBytes(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public byte[] RestoreLayout(string username, string application, string controlId, string tabId = null)
        {            
            foreach (var s in _strategies)
            {
                var layout = s.RestoreLayout(username, application, controlId, tabId);

                if (!string.IsNullOrEmpty(layout))
                    return GetBytes(layout);
            }

            return null;
        }

        public void SaveLayoutAsync(string username, string application, string controlId, Stream layoutData)
        {
            layoutData.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(layoutData))
            {
                SaveLayoutAsync(username, application, controlId, null, reader.ReadToEnd());
            }
        }
        
        private void SaveLayoutAsync(string username, string application, string controlId, string tabId, string layoutData)
        {
            _strategies.ForEach(s => s.SaveLayoutAsync(username, application, controlId, tabId, layoutData));
        }

        public bool Close()
        {
            return _strategies.All(s => s.Close());
        }
    }
}
