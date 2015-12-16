using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.SilderImg;
            var entity = sm.LoadEntity<HQ_Article>();
            if (entity != null)
            {
                ViewBag.SilderImgs = entity.AContent;
            }
            else
            {
                ViewBag.SilderImgs = string.Empty;
            }

            sm = new SearchModel("HQ_DisplayPanel");
            sm["DPanelType"] = (int)EnumDPanelType.Distination;
            sm.PageIndex = 1;
            sm.PageSize = 10;
            sm.OrderBy("id");
            ViewBag.DistList = sm.Load<HQ_DisplayPanel>().Data;

            return View();
        }

        public JsonResult LoadHotRoom(int destinationid, int pindex = 1, int psize = 3)
        {
            SearchModel sm = new SearchModel("uv_VillageRoom_Hot");
            sm["DPanelID"] = destinationid;
            sm.PageIndex = pindex;
            sm.PageSize = psize;
            sm.OrderBy("bcount", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            var rooms = sm.Load<HQ_Room>();
            return this.JsonNet(rooms);
        }
    }
}