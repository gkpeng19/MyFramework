using EntityLibrary.Entities;
using G.BaseWeb;
using G.Util.Account;
using G.Util.Mvc.Permission;
using G.Util.Tool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index(string op = null, string ReturnUrl = null)
        {
            if (LoginInfo.Current == null || !"Client".Equals(LoginInfo.Current.SystemID))
            {
                if ("login" == op)
                {
                    ViewBag.Action = "login";
                }
            }
            else
            {
                ViewBag.IsLogin = 1;
            }

            if (ReturnUrl != null && ReturnUrl.Length > 0)
            {
                ViewBag.ReturnUrl = ReturnUrl;
            }
            return View();
        }

        public int MemberLogin(string username, string userpsw, string yzm, int isrem)
        {
            var syzm = base.Session["yzm"];
            if (syzm == null || !syzm.ToString().Equals(yzm, StringComparison.CurrentCultureIgnoreCase))
            {
                return -2;
            }
            SearchModel sm = new SearchModel("HQ_Member");
            sm.MemberName = username;
            var member = sm.LoadEntity<HQ_Member>();
            if (member == null)
            {
                return -1;
            }

            if (member.UserPsw != Encryption.GetMD5(userpsw))
            {
                return 0;
            }

            LoginInfo info = new LoginInfo(username);
            info.SystemID = "Client";
            info.UserID = member.ID;
            info.UserType = member.UserType;


            LoginInfo.SetLoginToken(info, isrem == 1 ? true : false);

            return 1;
        }

        public int IsMemberExist(string username)
        {
            if (username == null || username.Length == 0)
            {
                return -1;
            }
            SearchModel sm = new SearchModel("HQ_Member");
            sm.MemberName = username;
            var entity = sm.LoadEntity<HQ_Member>();
            if (entity == null)
            {
                return 0;
            }
            return 1;
        }

        public int MemberReg(string username, string userpsw, string phone)
        {
            var exist = IsMemberExist(username);
            if (exist == -1)
            {
                return -2;
            }
            if (exist == 1)
            {
                return -1;
            }
            HQ_Member member = new HQ_Member();
            member.UserName = username;
            member.UserPsw = Encryption.GetMD5(userpsw);
            member.PhoneNum = phone;
            member.UserType = (int)EnumUserType.Normal;
            if (member.Save() > 0)
            {
                return 1;
            }
            return 0;
        }

        [LoginVerify("Client")]
        public ActionResult OrderIndex()
        {
            return View();
        }
    }
}