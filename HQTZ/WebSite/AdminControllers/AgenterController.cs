using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using EntityLibrary.Entities;
using G.Util.Account;

namespace HQWZ.Controllers
{
    public class AgenterController : Controller
    {
		public JsonResult LoadAgenterList(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

			se.SearchID="HQ_Agenter";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_Agenter>();
            return ExController.JsonNet(result);
        }


        [ValidateInput(false)]
		public JsonResult SaveAgenter(HQ_Agenter entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

		        entity.CreateBy=LoginInfo.Current.UserName;
                entity.CreateOn=DateTime.Now;
	            
                #endregion
            }
            else
            {
                #region 维护修改数据
		        
                entity.EditBy=LoginInfo.Current.UserName;
                entity.EditOn=DateTime.Now;

	            #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }
		
		
		
		public long DeleteAgenter(HQ_Agenter entity)
        {
            return entity.Delete();
        }
		
    }
}