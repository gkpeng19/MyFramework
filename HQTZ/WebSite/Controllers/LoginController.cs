using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using EntityLibrary.SearchEntities;
using EntityLibrary.Entities;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoginIn(string uname, string psw, int remember)
        {
            Sh_HZ_Admin sh = new Sh_HZ_Admin();
            sh["UserName"] = uname;
            var user = sh.LoadEntity<HZ_Admin>();
            if (user != null)
            {
                if (user.UPassword.Equals(CommonUtil.GetMD5(psw)))
                {
                    LoginInfo loginInfo = new LoginInfo(uname);
                    loginInfo.UserID = user.ID;
                    loginInfo.UserRole = user.UserRole;
                    LoginInfo.SetLoginToken(loginInfo, remember == 1 ? true : false);
                    return this.JsonNet(new { result = 1, url = "" });
                }
            }

            return this.JsonNet(new { result = 0 });
        }

        public void LoginOut()
        {
            LoginInfo.LoginOut();
        }
    }
}