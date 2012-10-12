using System.Web.Mvc;

namespace FriendlyForms.Helpers
{
    public static class UrlHelperExtensions
    {
        public static bool IsValidReturnUrl(this UrlHelper url, string returnUrl)
        {
            return url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\");
        }
    }
}