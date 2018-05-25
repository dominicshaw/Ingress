using Xamarin.Forms;

namespace Ingress.Mobile.Helpers
{
    public static class PageExtensions
    {
        public static void SetPlatformSpecificProperties(this Page page)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    page.Padding = new Thickness(0, 40, 0, 0);
                    break;
                default:
                    // This is just an example. You wouldn't actually need to do this, since Padding is already 0 by default.
                    page.Padding = new Thickness(0);
                    break;
            }
        }

        public static void SetBackText(this Page page, string text = "Back")
        {
            NavigationPage.SetBackButtonTitle(page, "Back");
        }

        public static void DisableBackButton(this Page page)
        {
            NavigationPage.SetBackButtonTitle(page, "");
            NavigationPage.SetHasBackButton(page, false);
        }
    }
}