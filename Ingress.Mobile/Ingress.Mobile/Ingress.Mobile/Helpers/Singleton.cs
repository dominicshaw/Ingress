using System;
using System.IO;

namespace Ingress.Mobile.Helpers
{
    public sealed class Singleton
    {
        private static volatile Singleton _instance;
        private static readonly object _syncRoot = new object();

        private Singleton() {}

        public static Singleton Instance
        {
            get 
            {
                if (_instance == null) 
                {
                    lock (_syncRoot) 
                    {
                        if (_instance == null) 
                            _instance = new Singleton();
                    }
                }

                return _instance;
            }
        }

        private string _username;

        public string Username
        {
            get => LoadUsername();
            set => SaveUsername(value);
        }

        private void SaveUsername(string username)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), @"Username.txt");

            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, username);
            _username = username;
        }

        private string LoadUsername()
        {
            if (!string.IsNullOrEmpty(_username)) 
                return _username;

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), @"Username.txt");

            if (!File.Exists(path))
                return string.Empty;

            _username = File.ReadAllText(path).Trim();
            return _username;
        }
    }
}