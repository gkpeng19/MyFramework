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
    public class MemberController : Controller
    {
		
		
		
		
		public JsonResult LoadMemberList(*SearchEntityType* se, int page_g, int psize_g)
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
		
		
		
		public JsonResult SaveMember(*EntityType* entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据
		        
	            #endregion
            }
            else
            {
                #region 维护修改数据
		        
	            #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }
		
		
    }
}