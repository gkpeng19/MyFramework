using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using G.BaseWeb.Models;
using System.Web.Mvc;

namespace G.BaseWeb.Controllers
{
    public class RoleController : Controller
    {		
		
		
		
		public JsonNetResult LoadRoles(BaseSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "BW_Role";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<BW_Role>();
            return ExController.JsonNet(result);
        }
		
		
		
		public JsonNetResult SaveRole(BW_Role entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据
		        
                entity.CreateOn=DateTime.Now;
                entity.CreateBy="";

	            #endregion
            }
            else
            {
                #region 维护修改数据
		        
                entity.EditOn=DateTime.Now;
                entity.EditBy="";

	            #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }
		
		
		public long DeleteRole(BW_Role entity)
        {
            return entity.Delete();
        }
		
    }
}