using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using WebSite.Models;
using G.BaseWeb;

namespace HQWZ.Controllers
{
    public class DPanelController : Controller
    {
        public JsonResult LoadDestinations(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_DisplayPanel";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_DisplayPanel>();
            return ExController.JsonNet(result);
        }
		
		
		
		public JsonResult SaveDestination(HQ_DisplayPanel entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据
		        
                entity.DPanelType=(int)DPanelTypeEnum.Distination;
                entity.CreateBy=AppEnvironment.LoginUser.Name;
                entity.CreateOn=DateTime.Now;

	            #endregion
            }
            else
            {
                #region 维护修改数据
		        
                entity.EditBy=AppEnvironment.LoginUser.Name;
                entity.CreateOn=DateTime.Now;

	            #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }
		
		
		
		public long DeletePanel(HQ_DisplayPanel entity)
        {
            return entity.Delete();
        }
		
    }
}