using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}