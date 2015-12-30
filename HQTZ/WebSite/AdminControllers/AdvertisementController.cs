using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using EntityLibrary.Entities;

namespace Project1.Controllers
{
    public class AdvertisementController : Controller
    {
        public JsonResult LoadData(SearchModel se)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "hq_advertisement";
            se.OrderBy("id");

            #endregion

            var result = se.Load<HQ_Advertisement>();
            return ExController.JsonNet(result);
        }

        public JsonResult SaveEntity(HQ_Advertisement entity)
        {
            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }
    }
}