using G.Util.Account;
using GOMFrameWork.DataEntity;
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

        public long DeleteEntity(EntityBase entity)
        {
            entity["IsDelete"] = 1;
            return entity.Save();
        }
    }
}