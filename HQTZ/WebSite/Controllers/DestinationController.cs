using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Controllers
{
    public class DestinationController : Controller
    {
        //
        // GET: /Default/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDestinations(int page)
        {
            var psize = 7;
            if (page > 1)
            {
                psize = psize + (page - 1) * 4;
            }
            SearchModel sm = new SearchModel("HQ_DisplayPanel");
            sm["DPanelType"] = (int)DPanelTypeEnum.Distination;
            sm.OrderBy("id");
            sm.PageIndex = 1;
            sm.PageSize = psize;
            var result = sm.Load<HQ_DisplayPanel>();

            List<HQ_DisplayPanel> list = null;
            if (page == 1)
            {
                list = result.Data;
            }
            else
            {
                list = new List<HQ_DisplayPanel>();
                for (var i = 7 + (page - 2) * 4 ; i < result.Data.Count; ++i)
                {
                    list.Add(result.Data[i]);
                }
            }

            return this.JsonNet(new { Data = list, PageCount = result.PageCount });
        }

        public JsonResult LoadDestinationDetails(int destinationId)
        {
            SearchModel sm = new SearchModel("HQ_DisplayContent");
            sm["DPanelID"] = destinationId;
            sm.AddSearch("ID", "Name");
            var result = sm.Load<HQ_DisplayContent>();
            return this.JsonNet(result.Data);
        }
    }
}