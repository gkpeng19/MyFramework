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

        public void LoadUserOrders(int uid,int page=1,int psize=20)
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
            if (room.CanCancelBook_G==1)
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
            if (ptype == 1 || ptype == 3 || ptype == 4)
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
                OutResult(0);
                return;
            }

            OutResult(member.ID);
        }

        public void Register(string uname, string psw, string email)
        {
            SearchModel sm = new SearchModel("HQ_Member");
            sm.MemberName = uname;
            var entity = sm.LoadEntity<HQ_Member>();
            if (entity != null)
            {
                OutResult(new { ResultID = 0, Error = "用户名已存在！" });
                return;
            }

            entity = new HQ_Member();
            entity.UserName = uname;
            entity.UserPsw = Encryption.GetMD5(psw);
            entity.Email = email;
            entity.UserType = (int)EnumUserType.Normal;
            var uid = entity.Save();
            if (uid > 0)
            {
                OutResult(new { ResultID = uid });
            }
            else
            {
                OutResult(new { ResultID = 0, Error = "注册失败，请重试！" });
            }
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