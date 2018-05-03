using System;
using System.IO;
using System.Linq;
using log4net;

namespace Ingress.WPF.Layouts
{
    public class FileLayoutStrategy : LayoutStrategy
    {
        public FileLayoutStrategy(ILog log) : base(log)
        {
            _ttDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TT");
        }

        private readonly string _ttDir;

        private void Initialize()
        {
            if (!Directory.Exists(_ttDir))
            {
                Log.Debug(string.Format("Creating folder {0} for file layout", _ttDir));
                Directory.CreateDirectory(_ttDir);
            }
        }

        private string GetFileName(string username, string application, string controlId, string tabId)
        {
            var filename = string.Format("{0}-{1}-{2}", username.Split('\\').Last(), application, controlId);

            if (!string.IsNullOrEmpty(tabId))
                filename += "-" + tabId;

            filename += ".xml";

            return Path.Combine(_ttDir, filename);
        }

        public override string RestoreLayout(string username, string application, string controlId, string tabId)
        {
            try
            {
                Initialize();

                var fileName = GetFileName(username, application, controlId, tabId);

                return !File.Exists(fileName) ? null : File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                Log.Warn("Could not restore layout from App directory: {0}", e);
                return null;
            }
        }

        protected override void SaveLayout(string username, string application, string controlId, string tabId, string layoutData)
        {
            var fileName = string.Empty;
            try
            {
                Initialize();

                fileName = GetFileName(username, application, controlId, tabId);

                File.WriteAllText(fileName, layoutData);
            }
            catch (Exception e)
            {
                Log.Warn(string.Format("Could not save layout to file {0}: {1}", fileName, e));
            }
        }

        public override string ToString()
        {
            return _ttDir;
        }
    }
}