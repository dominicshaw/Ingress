namespace Ingress.Mobile.ViewModels
{
    public class Notification
    {
        public Notification(string title, string message, string accept = "Ok", string cancel = "")
        {
            Title = title ?? "Notification";
            Message = message ?? "";
            Accept = accept ?? "Ok";
            Cancel = cancel ?? "";
        }
        
        public string Title { get; }
        public string Message { get; }
        public string Accept { get; }
        public string Cancel { get; }
    }
}