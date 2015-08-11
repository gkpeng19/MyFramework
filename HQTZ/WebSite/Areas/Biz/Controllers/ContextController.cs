using EntityLibrary.Entities;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using GOMFrameWork;
using EntityLibrary.SearchEntities;
using EntityLibrary;

namespace WebSite.Areas.Biz.Controllers
{
    public class ContextController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadContext(int page, int psize = 10)
        {
            Sh_HZ_Context sh = new Sh_HZ_Context();
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("IsTop", EnumOrderBy.Desc);
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            CommonResult<HZ_Context> result = sh.Load<HZ_Context>();
            return this.JsonNet(result);
        }

        [ValidateInput(false)]
        public JsonResult EditContext(HZ_Context entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)ContextTypeEnum.Context;
                entity.CreateBy = base.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = base.LoginUser.Name;
                entity.EditOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        public long DeleteContext(HZ_Context entity)
        {
            return base.DeleteEntity(entity);
        }
    }
}