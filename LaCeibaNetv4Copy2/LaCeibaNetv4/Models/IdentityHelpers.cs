using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using LaCeibaNetv4;

namespace LaCeibaNetv4.Models
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr
                = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}
