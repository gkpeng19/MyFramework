using EntityLibrary.Entities;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.AdminControllers
{
    public class MemberAskController : Controller
    {
        public JsonResult LoadAsks(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "uv_memberask";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_MemberAsk>();
            return this.JsonNet(result);
        }

        [ValidateInput(false)]
        public long SaveAnswer(HQ_MemberAnswer answer)
        {
            return answer.Save();
        }
    }
}