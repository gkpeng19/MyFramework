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

            return View();
        }
    }
}