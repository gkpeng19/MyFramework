using EntityLibrary.Entities;
using G.Util.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.AdminControllers
{
    public class FLinkController : Controller
    {
        public JsonResult GetLinks(SearchModel sm)
        {
            sm.SearchID = "HQ_FLink";
            sm.OrderBy("ID", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            var result = sm.Load<HQ_FLink>();
            return ExController.JsonNet(result);
        }

        public JsonResult SaveLink(HQ_FLink flink)
        {
            try
            {
                flink.Save();
            }
            catch
            {
                return ExController.JsonNet(new { ID = 0 });
            }
            return ExController.JsonNet(flink);
        }

        public long DeleteLink(HQ_FLink flink)
        {
            return flink.Delete();
        }
    }
}