using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;

namespace HQWZ.Controllers
{
    public class MemberAskController : Controller
    {
		
		
		
		
		public JsonResult LoadAsks(*SearchEntityType* se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

			se.SearchID="xxxx";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<*EntityType*>();
            return ExController.JsonNet(result);
        }
		
		
		
    }
}