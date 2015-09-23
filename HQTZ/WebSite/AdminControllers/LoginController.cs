using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using EntityLibrary.SearchEntities;
using EntityLibrary.Entities;
using WebSite.Models;
using G.BaseWeb.Controllers;

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
            BaseController bctr = new BaseController();
            return bctr.LoginIn(uname, psw, remember);
        }

        public void LoginOut()
        {
            LoginInfo.LoginOut();
        }
    }
}