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
using G.Util.Mvc;
using System.Data.SqlClient;
using GOMFrameWork.DataEntity;
using NM.Util;
using System.Text.RegularExpressions;

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

            if (member.UserPsw != MD5.EncryptString(userpsw))
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

        public int MemberReg(string username, string userpsw, string phone, string msgcode)
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

            var omcode = base.HttpContext.Cache["msg-" + phone];
            if (omcode == null || omcode.ToString().Length == 0)
            {
                return -3;
            }
            if (msgcode == null || msgcode.Length == 0 || !msgcode.Equals(omcode.ToString()))
            {
                return -4;
            }

            HQ_Member member = new HQ_Member();
            member.UserName = username;
            member.UserPsw = MD5.EncryptString(userpsw);
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
                var title = d.Title;
                if (title.Length > 30)
                {
                    title = title.Substring(0, 30) + "...";
                    d.Title = title;
                }
                var content = System.Text.RegularExpressions.Regex.Replace(d.AContent, "<[^>]*>", "");
                if (content.Length > 180)
                {
                    content = content.Substring(0, 180);
                }
                var cindex = content.LastIndexOf('<');
                if (cindex >= 0)
                {
                    content = content.Substring(0, cindex);
                }
                d.AContent = content;
            }

            return this.JsonNet(new { list = result.Data, pager = Pager.InitPager(pindex_g, result.PageCount, "loadTraveGuides") });
        }

        public ActionResult ViewGuidInfo(int id)
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ID"] = id;
            var article = sm.LoadEntity<HQ_Article>();
            return View(article);
        }

        [LoginVerify("Client")]
        public ActionResult OrderIndex()
        {
            return View();
        }

        [LoginVerify("Client")]
        public JsonResult LoadOrders(SearchModel sm, int page_g, int psize_g = 8)
        {
            sm.SearchID = "uv_bookroom";
            sm["MemberID"] = LoginInfo.Current.UserID;
            sm.OrderBy("id", EnumOrderBy.Desc);
            sm.PageIndex = page_g;
            sm.PageSize = psize_g;
            var result = sm.Load<HQ_BookRoom>();

            return this.JsonNet(new { Data = result.Data, pager = Pager.InitPager(page_g, result.PageCount, "loadOrders") });
        }

        public long CancelBook(HQ_BookRoom broom)
        {
            var sm = new SearchModel("hq_bookroom");
            sm["id"] = broom.ID;
            var room = sm.LoadEntity<HQ_BookRoom>();
            if (room.CanCancelBook_G == 1)
            {
                return broom.Delete();
            }
            return -1;
        }

        public static int IsRoomEnough(int roomid, DateTime sdate, DateTime edate)
        {
            if (sdate > edate)
            {
                return 0;
            }
            SearchModel smroom = new SearchModel("hq_room");
            smroom["ID"] = roomid;
            smroom.AddSearch("RCount");
            var rcount = smroom.LoadEntity<HQ_Room>().RCount;

            SearchEntity sm = SearchEntity.FormSql("select count(1) id from hq_bookroom where roomid=@roomid and isnull(isdelete,0)=0 and bookendtime>=@stime and bookstarttime<=@etime",
            new SqlParameter("roomid", roomid), new SqlParameter("stime", sdate), new SqlParameter("etime", edate));

            var bcount = sm.LoadValue<Int32>();

            if (bcount >= rcount)
            {
                return 0;
            }
            return 1;
        }

        public static object _object = new object();
        public long BookRoom(int roomid, DateTime sdate, DateTime edate)
        {
            if (!LoginVerify.IsLogin("Client"))
            {
                return -1;
            }

            lock (_object)
            {
                if (IsRoomEnough(roomid, sdate, edate) > 0)
                {
                    HQ_BookRoom entity = new HQ_BookRoom();
                    entity.MemberID = (int)LoginInfo.Current.UserID;
                    entity.RoomID = roomid;
                    entity.BookStartTime = sdate;
                    entity.BookEndTime = edate;
                    entity.CreateOn = DateTime.Now;
                    return entity.Save();
                }
                else
                {
                    return -2;
                }
            }
        }

        [LoginVerify("Client")]
        public long ChangeUserPsw(string oldPsw, string newPsw)
        {
            SearchModel se = new SearchModel("hq_member");
            se["ID"] = LoginInfo.Current.UserID;
            var user = se.LoadEntity<HQ_Member>();
            if (user.UserPsw.Equals(MD5.EncryptString(oldPsw)))
            {
                user = new HQ_Member();
                user["ID"] = LoginInfo.Current.UserID;
                user.UserPsw = MD5.EncryptString(newPsw);
                return user.Save();
            }
            return 0;
        }

        [LoginVerify("Client")]
        public long ChangeUserPhone(string phone, string yzm)
        {
            if (phone == null || phone.Length == 0 || !(Regex.Match(phone, @"^[1][3,5,8][0-9]{9}$", RegexOptions.Compiled).Success))
            {
                return -1;//手机号码错误
            }

            var omcode = base.HttpContext.Cache["msg-" + phone];
            if (omcode == null || omcode.ToString().Length == 0)
            {
                return -2;//已发送的短信验证码不存在
            }

            if (yzm == null || yzm.Length == 0 || !yzm.Equals(omcode.ToString()))
            {
                return -2;//输入的短信验证码错误
            }


            HQ_Member user = new HQ_Member();
            user["ID"] = LoginInfo.Current.UserID;
            user.PhoneNum = phone;
            return user.Save();
        }

        public int SendMsgCode(string phone, string type)
        {
            if (type == null || type.Length == 0)
            {
                return -2;//系统错误
            }

            if (phone != null && phone.Length > 0 && Regex.Match(phone, @"^[1][3,5,8][0-9]{9}$", RegexOptions.Compiled).Success)
            {
                var msgKey = string.Empty;
                if ("1".Equals(type))
                {
                    SearchModel se = new SearchModel("hq_member");
                    se["PhoneNum"] = phone;
                    se.AddSearch("count(1)");
                    var count = se.LoadValue<int>();
                    if (count > 0)
                    {
                        return 9;
                    }

                    msgKey = "用户注册短信验证码：";
                }

                if ("3".Equals(type))
                {
                    SearchModel se = new SearchModel("hq_member");
                    se["PhoneNum"] = phone;
                    se.AddSearch("count(1)");
                    var count = se.LoadValue<int>();
                    if (count > 0)
                    {
                        return 9;
                    }

                    msgKey = "用户修改手机号码短信验证码：";
                }

                if ("2".Equals(type))
                {
                    msgKey = "用户找回密码验证码：";
                }

                var yzm = string.Empty;
                foreach (var i in Ran.GetRandomArray(6, 0, 9))
                {
                    yzm += i.ToString();
                }
                SendUserInfo _U = new SendUserInfo() { isLog = 1, orgid = 555, username = phone };
                if (MsgSend.DirectSend(msgKey + yzm, phone, _U))
                {
                    base.HttpContext.Cache["msg-" + phone] = yzm;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public int FindPsw(string phone, string psw, string msgcode)
        {
            if (phone == null || phone.Length == 0)
            {
                return -1;
            }

            if (psw == null || psw.Length == 0)
            {
                return 0;
            }

            var omcode = base.HttpContext.Cache["msg-" + phone];
            if (omcode == null || omcode.ToString().Length == 0)
            {
                return -2;
            }

            if (msgcode == null || msgcode.Length == 0 || !msgcode.Equals(omcode.ToString()))
            {
                return -3;
            }

            SearchModel sm = new SearchModel("hq_member");
            sm["phonenum"] = phone;
            sm.AddSearch("id");
            var id = sm.LoadValue<int>();
            if (id > 0)
            {
                HQ_Member member = new HQ_Member();
                member["id"] = id;
                member.UserPsw = MD5.EncryptString(psw);
                if (member.Save() > 0)
                {
                    return 1;
                }
                return 0;
            }

            return -4;
        }

        [LoginVerify("Client")]
        public ActionResult MemberIndex()
        {
            SearchModel sm = new SearchModel("hq_member");
            sm["id"] = LoginInfo.Current.UserID;
            ViewBag.Member = sm.LoadEntity<HQ_Member>();

            sm = new SearchModel("uv_bookroom");
            sm.BeginInDate = DateTime.Now.Date;
            sm["MemberID"] = LoginInfo.Current.UserID;
            sm.OrderBy("id", EnumOrderBy.Desc);
            ViewBag.Orders = sm.Load<HQ_BookRoom>().Data;

            return View();
        }

        public long SaveMemberInfo(HQ_Member member)
        {
            member["id"] = LoginInfo.Current.UserID;
            return member.Save();
        }

        public JsonResult LoadCurrUserInfo()
        {
            var user = LoginInfo.Current;
            if (user != null && user.UserID > 0)
            {
                SearchModel sm = new SearchModel("hq_member");
                sm["id"] = user.UserID;
                var member = sm.LoadEntity<HQ_Member>();
                if (member != null)
                {
                    return this.JsonNet(new { Phone = member.PhoneNum, Pwd = MD5.DecryptString(member.UserPsw) });
                }
            }
            return this.JsonNet(new { Phone = "" });
        }
    }
}