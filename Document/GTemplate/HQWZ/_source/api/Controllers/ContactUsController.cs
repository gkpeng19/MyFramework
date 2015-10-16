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
    public class ClientBaseController : Controller
    {
		
		public JsonResult GetContactUs()
		{
			return ExController.JsonNet(new {  });
		}
		
		
		
		
		
		
		public JsonResult SaveContactUs(*EntityType* entity)
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