using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Controllers
{
    public class DetailController : Controller
    {
        public ActionResult Index(int id)
        {
            SearchModel sm = new SearchModel("HQ_DisplayContent");
            sm["ID"] = id;
            var entity = sm.LoadEntity<HQ_DisplayContent>();
            return View(entity);
        }

        public ActionResult RoomIndex(int id)
        {
            SearchModel sm = new SearchModel("HQ_DisplayContent");
            sm["ID"] = id;
            var entity = sm.LoadEntity<HQ_DisplayContent>();
            ViewBag.VillageID = id;

            sm = new SearchModel("hq_article");
            sm["refid"] = id;
            ViewBag.Descs = sm.Load<HQ_Article>().Data;

            return View(entity);
        }

        public JsonResult LoadRooms(int villageId)
        {
            SearchModel sm = new SearchModel("HQ_Room");
            sm["VillageID"] = villageId;
            var data = sm.Load<HQ_Room>().Data;
            return this.JsonNet(data);
        }
    }
}