using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.AdminControllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDestinations(int page, int psize = 7)
        {
            SearchModel sm = new SearchModel("HQ_DisplayPanel");
            sm["DPanelType"] = (int)DPanelTypeEnum.Distination;
            sm.OrderBy("id", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            sm.PageIndex = page;
            sm.PageSize = psize;

            var result = sm.Load<HQ_DisplayPanel>();
            return this.JsonNet(result.Data);
        }
    }
}