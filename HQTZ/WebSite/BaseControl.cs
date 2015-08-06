using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite
{
    public class BaseController : Controller
    {
        public LoginInfo LoginUser
        {
            get
            {
                return HttpContext.User.Identity as LoginInfo;
            }
        }
    }
}