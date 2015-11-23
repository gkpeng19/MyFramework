using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using System.Web.Security;

namespace G.Simple.Controllers
{
    public class LoginController : Controller
    {
        public int LoginIn()
        {
            LoginInfo user = new LoginInfo("gkpeng");
            user.UserID = 1;
            user.UserRole = 1;
            LoginInfo.SetLoginToken(user, true);
            return 1;
        }

        public void LoginOut()
        {
            LoginInfo.LoginOut();
            base.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl);
        }

        public string GetLoginUserName()
        {
            return LoginInfo.Current.Name;
        }
    }
}