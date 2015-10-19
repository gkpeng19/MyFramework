using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace G.Util.Mvc.Permission
{
    public class LoginVerifyAttribute : ActionFilterAttribute
    {
        private string _systemId = string.Empty;
        public LoginVerifyAttribute() { }
        public LoginVerifyAttribute(string systemId)
        {
            this._systemId = systemId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string loginUrl = string.Empty;
            if (!LoginVerify.IsLogin(ref loginUrl, this._systemId))
            {
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }

    public static class LoginVerify
    {
        public static bool IsLogin(string systemId = null)
        {
            if (systemId == null)
            {
                systemId = string.Empty;
            }
            var context = HttpContext.Current;
            if (!context.User.Identity.IsAuthenticated || !LoginInfo.Current.SystemID.Equals(systemId, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool IsLogin(ref string loginUrl, string systemId = null)
        {
            if (systemId == null)
            {
                systemId = string.Empty;
            }
            var context = HttpContext.Current;
            if (!context.User.Identity.IsAuthenticated || !LoginInfo.Current.SystemID.Equals(systemId, StringComparison.CurrentCultureIgnoreCase))
            {
                string returnUrl = context.Request.Url.AbsolutePath;
                if (FormsAuthentication.LoginUrl.IndexOf('?') > -1)
                {
                    loginUrl = FormsAuthentication.LoginUrl + string.Format("&ReturnUrl={0}", returnUrl);
                }
                else
                {
                    loginUrl = FormsAuthentication.LoginUrl + string.Format("?ReturnUrl={0}", returnUrl);
                }
                return false;
            }
            return true;
        }
    }
}
