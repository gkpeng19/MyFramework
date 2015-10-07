﻿using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || !LoginInfo.Current.SystemID.Equals(this._systemId, StringComparison.CurrentCultureIgnoreCase))
            {
                string loginUrl = string.Empty;
                string returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
                if (FormsAuthentication.LoginUrl.IndexOf('?') > -1)
                {
                    loginUrl = FormsAuthentication.LoginUrl + string.Format("&ReturnUrl={0}", returnUrl);
                }
                else
                {
                    loginUrl = FormsAuthentication.LoginUrl + string.Format("?ReturnUrl={0}", returnUrl);
                }
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }
}
