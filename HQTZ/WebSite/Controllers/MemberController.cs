﻿using EntityLibrary.Entities;
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
using G.Util.Mvc;

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

        public string SearchAgenter(string name)
        {
            if (name == null || name.Length == 0)
            {
                return string.Empty;
            }
            SearchModel sm = new SearchModel("HQ_Agenter");
            sm.AgenterName = name;
            var agenter = sm.LoadEntity<HQ_Agenter>();
            if (agenter != null)
            {
                return agenter.AContent;
            }
            return string.Empty;
        }

        public ActionResult TraveGuide()
        {
            return View();
        }

        public JsonResult LoadTraveGuides(SearchModel sm, int pindex_g, int psize_g = 8)
        {
            sm.SearchID = "HQ_Article";
            sm["ACategory"] = (int)EnumArticleCategory.TravelGuide;
            sm.PageIndex = pindex_g;
            sm.PageSize = psize_g;
            sm.OrderBy("id", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            var result = sm.Load<HQ_Article>();
            foreach (var d in result.Data)
            {
                var content = d.AContent.Substring(0, 500);
                content = System.Text.RegularExpressions.Regex.Replace(content, "<[^>]*>", "");
                if(content.Length>180)
                {
                    content = content.Substring(0, 180);
                }
                d.AContent = content;
            }

            return this.JsonNet(new { list = result.Data, pager = Pager.InitPager(pindex_g, result.PageCount, "loadTraveGuides") });
        }

        public ActionResult ViewGuidInfo(int id)
        {
            SearchModel sm=new SearchModel("HQ_Article");
            sm["ID"]=id;
            var article = sm.LoadEntity<HQ_Article>();
            return View(article);
        }

        [LoginVerify("Client")]
        public ActionResult OrderIndex()
        {
            return View();
        }
    }
}