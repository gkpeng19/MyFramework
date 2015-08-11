using EntityLibrary;
using G.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using EntityLibrary.Entities;

namespace WebSite.Areas.Biz.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetContentData()
        {
            List<HtmlSelectItem> list = new List<HtmlSelectItem>();
            var items = EnumExtension.GetEnumCollection(typeof(UserRoleEnum));
            foreach(var item in items)
            {
                list.Add(new HtmlSelectItem() { k = item.Description, v = item.Value });
            }
            return this.JsonNet(new { URoles = list });
        }

        public JsonResult SaveAdmin(HZ_Admin entity)
        {
            return this.JsonNet(entity);
        }
	}
}