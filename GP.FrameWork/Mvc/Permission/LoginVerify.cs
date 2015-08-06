using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace GP.FrameWork.Mvc.Permission
{
    public class LoginVerifyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
                string loginUrl = FormsAuthentication.LoginUrl + string.Format("?ReturnUrl={0}", returnUrl);
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }
}
