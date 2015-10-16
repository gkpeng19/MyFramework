using EntityLibrary.Entities;
using G.BaseWeb.Controllers;
using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Controllers
{
    public class ClientBaseController : BaseController
    {
        public override void LoginOut()
        {
            LoginInfo.LoginOut();
            base.HttpContext.Response.Redirect("~/AdminPages/Login.html");
        }

        public string GetLoginUserName()
        {
            return LoginInfo.Current.UserName;
        }

        public JsonResult GetContactUs()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.ContactUs;
            var entity = sm.LoadEntity<HQ_Article>();
            if (entity != null)
            {
                return this.JsonNet(entity);
            }
            return null;
        }

        [ValidateInput(false)]
        public long SaveContactUs(HQ_Article entity)
        {
            if (entity.ID > 0)
            {
                entity.EditBy = LoginInfo.Current.UserName;
                entity.EditOn = DateTime.Now;
            }
            else
            {
                entity.ACategory = (int)EnumArticleCategory.ContactUs;
                entity.CreateBy = LoginInfo.Current.UserName;
                entity.CreateOn = DateTime.Now;
            }

            return entity.Save();
        }

        public ActionResult ContactIndex()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.ContactUs;
            var entity = sm.LoadEntity<HQ_Article>();
            if(entity==null)
            {
                return View(new HQ_Article() { AContent = "维护中。。。" });
            }
            return View(entity);
        }
    }
}