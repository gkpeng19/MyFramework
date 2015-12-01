using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using System.Web.Http.Cors;
using G.Util.Tool;
using Newtonsoft.Json;
using GOMFrameWork.DataEntity;
using System.Text.RegularExpressions;
using G.Util.Account;
using System.Configuration;
using NM.Util;

namespace WebSite.Controllers
{
    public class PhoneController : Controller
    {
        static Regex reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
        public void LoadAllValliages(int page = 1, int psize = 999)
        {
            SearchModel sm = new SearchModel("uv_DisplayContent");
            sm["DPanelType"] = (int)EnumDPanelType.Distination;
            sm.OrderBy("id");
            sm.PageIndex = page;
            sm.PageSize = psize;
            sm.AddSearch("id", "DPanel_G", "Name", "ImgName_G");
            var result = sm.Load<HQ_DisplayContent>();
            OutResult(result);
        }

        public void LoadTraveGuides(int page = 1, int psize = 999)
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.TravelGuide;
            sm.OrderBy("id", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            sm.PageIndex = page;
            sm.PageSize = psize;
            var result = sm.Load<HQ_Article>();
            foreach (var d in result.Data)
            {
                var title = d.Title;
                if (title.Length > 25)
                {
                    title = title.Substring(0, 25) + "...";
                    d.Title = title;
                }
                var content = System.Text.RegularExpressions.Regex.Replace(d.AContent, "<[^>]*>", "");
                content = System.Text.RegularExpressions.Regex.Replace(content, @"&(nbsp|#160);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                content = System.Text.RegularExpressions.Regex.Replace(content, @"　+", "");
                content = System.Text.RegularExpressions.Regex.Replace(content, @" +", "");
                if (content.Length > 30)
                {
                    content = content.Substring(0, 30);
                }
                var cindex = content.LastIndexOf('<');
                if (cindex >= 0)
                {
                    content = content.Substring(0, cindex);
                }
                d.AContent = content;
            }
            OutResult(result);
        }

        public void LoadMemberBenefit(int page = 1, int psize = 999)
        {
            SearchModel sm = new SearchModel("uv_DisplayContent");
            sm["DPanelType"] = (int)EnumDPanelType.MemberBenefit;
            sm.OrderBy("id");
            sm.PageIndex = page;
            sm.PageSize = psize;
            sm.AddSearch("id", "DPanel_G", "Name", "ImgName_G");
            var result = sm.Load<HQ_DisplayContent>();
            OutResult(result);
        }

        public void LoadMemberService(int page = 1, int psize = 999)
        {
            SearchModel sm = new SearchModel("uv_DisplayContent");
            sm["DPanelType"] = (int)EnumDPanelType.MemberService;
            sm.OrderBy("id");
            sm.PageIndex = page;
            sm.PageSize = psize;
            sm.AddSearch("id", "DPanel_G", "Name", "ImgName_G");
            var result = sm.Load<HQ_DisplayContent>();
            OutResult(result);
        }

        public void LoadNewCheaper(int page = 1, int psize = 999)
        {
            SearchModel sm = new SearchModel("uv_DisplayContent");
            sm["DPanelType"] = (int)EnumDPanelType.NewCheaper;
            sm.OrderBy("id");
            sm.PageIndex = page;
            sm.PageSize = psize;
            sm.AddSearch("id", "DPanel_G", "Name", "ImgName_G");
            var result = sm.Load<HQ_DisplayContent>();
            OutResult(result);
        }

        public void LoadUserOrders(int uid, int page = 1, int psize = 15)
        {
            SearchModel sm = new SearchModel("uv_memberorder");
            sm["MemberID"] = uid;
            sm.OrderBy("id", EnumOrderBy.Desc);
            sm.PageIndex = page;
            sm.PageSize = psize;
            var result = sm.Load<HQ_BookRoom>();
            OutResult(new { List = result.Data, PageCount = result.PageCount });
        }

        public void CancelBook(HQ_BookRoom broom)
        {
            var sm = new SearchModel("hq_bookroom");
            sm["id"] = broom.ID;
            var room = sm.LoadEntity<HQ_BookRoom>();
            if (room.CanCancelBook_G == 1)
            {
                broom.Delete();
                OutResult(1);
            }
            else
            {
                OutResult(0);
            }
        }

        public void LoadArticleDetail(int ptype, int vid)
        {
            SearchModel sm = null;
            if (ptype == 1 || ptype == 3 || ptype == 4 || ptype == 5)
            {
                sm = new SearchModel("HQ_DisplayContent");
                sm["ID"] = vid;
                sm.AddSearch("Name", "DContent");
                var dcon = sm.LoadEntity<HQ_DisplayContent>();
                if (dcon != null)
                {
                    OutResult(new { Name = dcon.Name, Content = reg.Replace(dcon.DContent, (match) => { return "<img class='uimg' _src='" + match.Groups[1].Value + "' />"; }) });
                    return;
                }
            }
            else if (ptype == 2)
            {
                sm = new SearchModel("HQ_Article");
                sm["ID"] = vid;
                sm.AddSearch("Title", "AContent");
                var article = sm.LoadEntity<HQ_Article>();
                if (article != null)
                {
                    OutResult(new { Name = article.Title, Content = reg.Replace(article.AContent, (match) => { return "<img class='uimg' _src='" + match.Groups[1].Value + "' />"; }) });
                    return;
                }
            }

            OutResult(new { Name = "...", Content = "您要查找的内容不存在！" });
        }

        public void LoadAllRooms()
        {
            SearchModel sm = new SearchModel("uv_VillageRoomWithImg");
            var result = sm.Load<HQ_Room>();
            OutResult(result);
        }

        public void LoadRoomDetail(int roomid)
        {
            SearchModel sm = new SearchModel("hq_room");
            sm["id"] = roomid;
            var result = sm.LoadEntity<HQ_Room>();
            if (result != null)
            {
                OutResult(new { Name = result.Name, Description = reg.Replace(result.Description, (match) => { return "<img class='uimg' _src='" + match.Groups[1].Value + "' />"; }) });
            }
        }

        public void BookRoom(int roomid, int userid, DateTime sdate, DateTime edate)
        {
            if (sdate.AddDays(1) < DateTime.Now)
            {
                OutResult(-1);
                return;
            }
            if (edate < sdate)
            {
                OutResult(-2);
                return;
            }
            lock (MemberController._object)
            {
                if (MemberController.IsRoomEnough(roomid, sdate, edate) > 0)
                {
                    HQ_BookRoom entity = new HQ_BookRoom();
                    entity.MemberID = userid;
                    entity.RoomID = roomid;
                    entity.BookStartTime = sdate;
                    entity.BookEndTime = edate;
                    entity.CreateOn = DateTime.Now;
                    var rid = entity.Save();
                    OutResult(1);
                }
                else
                {
                    OutResult(0);
                }
            }
        }

        public void Login(string uname, string psw)
        {
            SearchModel sm = new SearchModel("HQ_Member");
            sm.MemberName = uname;
            var member = sm.LoadEntity<HQ_Member>();
            if (member == null || member.UserPsw != Encryption.GetMD5(psw))
            {
                OutResult(new { id = 0 });
                return;
            }

            OutResult(new { id = member.ID, phone = member.PhoneNum, header = member.HeaderImg_G });
        }

        public void Register(string uname, string psw, string phone, string yzm)
        {
            SearchModel sm = new SearchModel("HQ_Member");
            sm.MemberName = uname;
            var entity = sm.LoadEntity<HQ_Member>();
            if (entity != null)
            {
                OutResult(new { ResultID = -1 });
                return;
            }

            var omcode = base.HttpContext.Cache["msg-" + phone];
            if (omcode == null || omcode.ToString().Length == 0)
            {
                OutResult(new { ResultID = -3 });
                return;
            }
            if (yzm == null || yzm.Length == 0 || !yzm.Equals(omcode.ToString()))
            {
                OutResult(new { ResultID = -4 });
                return;
            }

            HQ_Member member = new HQ_Member();
            member.UserName = uname;
            member.UserPsw = Encryption.GetMD5(psw);
            member.PhoneNum = phone;
            member.UserType = (int)EnumUserType.Normal;
            if (member.Save() > 0)
            {
                OutResult(new { ResultID = 1 });
                return;
            }
            OutResult(new { ResultID = 0 });
            return;
        }

        public void FindPsw(string phone, string psw, string msgcode)
        {
            if (phone == null || phone.Length == 0)
            {
                OutResult(new { r = -1 });
                return;
            }

            if (psw == null || psw.Length == 0)
            {
                OutResult(new { r = 0 });
                return;
            }

            var omcode = base.HttpContext.Cache["msg-" + phone];
            if (omcode == null || omcode.ToString().Length == 0)
            {
                OutResult(new { r = -2 });
                return;
            }

            if (msgcode == null || msgcode.Length == 0 || !msgcode.Equals(omcode.ToString()))
            {
                OutResult(new { r = -3 });
                return;
            }

            SearchModel sm = new SearchModel("hq_member");
            sm["phonenum"] = phone;
            sm.AddSearch("id");
            var id = sm.LoadValue<int>();
            if (id > 0)
            {
                HQ_Member member = new HQ_Member();
                member["id"] = id;
                member.UserPsw = G.Util.Tool.Encryption.GetMD5(psw);
                if (member.Save() > 0)
                {
                    OutResult(new { r = 1 });
                    return;
                }
                OutResult(new { r = 0 });
                return;
            }

            OutResult(new { r = -4 });
            return;
        }

        public void SaveAsk(int uid, string ask)
        {
            HQ_MemberAsk entity = new HQ_MemberAsk();
            entity.MemberID = uid;
            entity.AskContent = ask;
            entity.CreateOn = DateTime.Now;
            var id = entity.Save();
            OutResult(new { ID = id });
        }

        public void LoadMyAsk(int uid)
        {
            SearchModel sm = new SearchModel("uv_memberask");
            sm["MemberID"] = uid;
            sm.OrderBy("id", EnumOrderBy.Desc);
            var result = sm.Load<HQ_MemberAsk>();
            OutResult(new { List = result.Data });
        }

        public void LoadAnswer(int answerid)
        {
            SearchModel sm = new SearchModel("HQ_MemberAnswer");
            sm["id"] = answerid;
            var entity = sm.LoadEntity<HQ_MemberAnswer>();
            if (entity != null)
            {
                var e = new HQ_MemberAnswer();
                e["ID"] = entity.ID;
                e.IsView = 1;
                e.Save();
                OutResult(new { Answer = entity.Answer });
            }
            else
            {
                OutResult(new { Answer = string.Empty });
            }
        }

        public void CheckNewAnswer(int uid)
        {
            SearchModel sm = new SearchModel("uv_memberask");
            sm["MemberID"] = uid;
            sm.NewAnswerID = 0;
            sm.AnswerViewed = 0;
            sm.AddSearch("count(1)");
            var newcount = sm.LoadValue<int>();
            OutResult(new { ncount = newcount });
        }

        public void CheckUpdate(string pversion)
        {
            var newversion = ConfigurationManager.AppSettings["appversion"];
            if (newversion == null || pversion.Equals(newversion))
            {
                OutResult(new { updated = 0 });
            }
            else
            {
                OutResult(new { updated = 1, path = ConfigurationManager.AppSettings["apppath"] });
            }
        }

        public void SendMsgCode(string phone, string type)
        {
            if (type == null || type.Length == 0)
            {
                OutResult(new { r = -2 });//系统错误
                return;
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
                        OutResult(new { r = 9 });
                        return;
                    }

                    msgKey = "用户注册短信验证码：";
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
                    OutResult(new { r = 1 });
                    return;
                }
                else
                {
                    OutResult(new { r = 0 });
                    return;
                }
            }
            else
            {
                OutResult(new { r = -1 });
                return;
            }
        }

        public void LoadSilderImgs()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.SilderImg;
            var entity = sm.LoadEntity<HQ_Article>();
            string plstr = string.Empty;
            if (entity != null)
            {
                plstr = entity.AContent;
            }
            OutResult(plstr);
        }

        public ActionResult AppDownload()
        {
            var appname = ConfigurationManager.AppSettings["appname"];
            var path = Server.MapPath("~/appstore/" + appname);
            return File(path, "application/octet-stream");
        }

        void OutResult(object result)
        {
            HttpResponseBase response = this.HttpContext.Response;
            response.AddHeader("Access-Control-Allow-Origin", "http://127.0.0.1:8020");
            response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");
            response.AddHeader("Cache-Control", "no-cache");
            response.Write(JSON.Stringify(result));
            response.End();
        }
    }
}