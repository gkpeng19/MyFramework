using G.BaseWeb.Controllers;
using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class ClientBaseController : BaseController
    {
        public override void LoginOut()
        {
            LoginInfo.LoginOut();
            base.HttpContext.Response.Redirect("~/AdminPages/Login.html");
        }

        public string GetLoginUserName()
        {
            return LoginInfo.Current.UserName;
        }
    }
}