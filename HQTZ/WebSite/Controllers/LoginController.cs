using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoginIn(string uname, string psw)
        {
            if (uname == "admin" && psw == "admin")
            {
                LoginInfo user = new LoginInfo(uname);
                user.UserID = 1;
                user.UserType = 1;
                user.UserRole = 1;
                LoginInfo.SetLoginToken(user, true);
                return this.JsonNet(new { result = 1, url = "" });
            }
            return this.JsonNet(new { result = 0 });
        }
    }
}