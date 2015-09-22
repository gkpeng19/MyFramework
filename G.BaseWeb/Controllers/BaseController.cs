using G.BaseWeb.Models;
using G.Util.Account;
using G.Util.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;

namespace G.BaseWeb
{
    public class AppEnvironment
    {
        public static LoginInfo LoginUser
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null)
                {
                    return identity as LoginInfo;
                }
                return null;
            }
        }
    }
}

namespace G.BaseWeb.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult LoginIn(string uname, string psw, int remember)
        {
            BaseSearchModel sm = new BaseSearchModel("bw_user");
            sm.LoginUserName = uname;
            var user = sm.LoadEntity<BW_User>();
            if (user != null)
            {
                if (user.UserPsw.Equals(Encryption.GetMD5(psw)))
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

        class Search_MenuAuthority : BaseSearchModel
        {
            public Search_MenuAuthority()
            {
                base.SearchID = "uv_rolemenu";
            }

            [Search(Field = "MenuID", Operator = SearchOperator.In)]
            public string MenuIds
            {
                set { SetValue("MenuIds", value); }
            }

            public int RoleID
            {
                set { SetValue("RoleID", value); }
            }
        }

        public JsonResult LoadAuthorities(string menuids)
        {
            var user = AppEnvironment.LoginUser;
            if (user == null)
            {
                return this.JsonNet(new { ID = 0 });
            }
            Search_MenuAuthority search = new Search_MenuAuthority();
            search.MenuIds = menuids;
            search.RoleID = AppEnvironment.LoginUser.UserRole;
            var menus = search.Load<BW_Menu>().Data;

            var canView = false;
            foreach (var menu in menus)
            {
                if (menu.Code.Equals("view", StringComparison.CurrentCultureIgnoreCase))
                {
                    canView = true;
                    menus.Remove(menu);
                    break;
                }
            }

            if (!canView)
            {
                return this.JsonNet(new { ID = 0 });
            }
            return this.JsonNet(new { ID = 1, Menus = menus });
        }
    }
}