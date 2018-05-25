using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ingress.Mobile.MVVM;
using Ingress.Mobile.ViewModels;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Ingress.Mobile.Helpers
{
    public static class Reporter
    {
        public static void Identify(Dictionary<string, string> data)
        {
            Debug.WriteLine("Login - " + string.Join("; ", data.Select(x => x.Key + ": " + x.Value)));
            Analytics.TrackEvent("Login", data);
        }

        public static void Track(string eventName, Dictionary<string, string> data)
        {
            data.Add("User", Singleton.Instance.Username);

            Debug.WriteLine(eventName + " - " + string.Join("; ", data.Select(x => x.Key + ": " + x.Value)));
            Analytics.TrackEvent(eventName, data);
        }

        [Conditional("DEBUG")]
        public static void TrackDebug(string message)
        {
            Debug.WriteLine(message);
            Track("Debug", new Dictionary<string, string> { { "Message", message }, { "Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") } });
        }

        public static void TrackError(string eventName, Dictionary<string, string> data)
        {
            data.Add("User", Singleton.Instance.Username);

            Debug.WriteLine(eventName + " - " + string.Join("; ", data.Select(x => x.Key + ": " + x.Value)));
            Crashes.TrackError(new Exception(eventName), data);
        }

        internal static void ReportException(Exception ex, Dictionary<string, string> data = null)
        {
            if (ex == null)
                return;

            if (data == null)
                data = new Dictionary<string, string>();

            data.Add("User", Singleton.Instance.Username);

            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);

            Crashes.TrackError(ex, data);

            Messenger.Instance.NotifyColleagues("Notification", new Notification("Error", "An error occurred. Please try again and if the problem persists, contact IT.\n\n" + ex.Message));
        }
    }
}
