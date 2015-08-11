using EntityLibrary;
using G.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using EntityLibrary.Entities;
using EntityLibrary.SearchEntities;

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

        public JsonResult LoadAdmin(int page,int psize)
        {
            Sh_HZ_Admin sh = new Sh_HZ_Admin();
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("createon", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            var r= sh.Load<HZ_Admin>();
            return this.JsonNet(r);
        }

        public JsonResult SaveAdmin(HZ_Admin entity)
        {
            if(entity.ID==0)
            {
                entity.UPassword = CommonUtil.GetMD5("123456");
                entity.CreateOn = DateTime.Now;
            }
            var r = entity.Save();
            if(r>0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        public long DeleteAdmin(HZ_Admin entity)
        {
            return base.DeleteEntity(entity);
        }
	}
}