using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using EntityLibrary.Entities;
using G.BaseWeb;

namespace HQWZ.Controllers
{
    public class VillageRoomController : Controller
    {
		
		public JsonResult LoadJsonData()
		{
            SearchModel sm=new SearchModel("uv_DisplayContent");
            sm.AddSearch("ID","Name");
            sm["DPanelType"]=(int)DPanelTypeEnum.Distination;
            sm.OrderBy("DPanelID");
            var result= sm.Load<HQ_DisplayContent>();
            var items= HtmlSelect.GetHtmlSelectByCollection<HQ_DisplayContent>(result.Data,(e)=>
                {
                    return new HtmlSelectItem(){ k=e.Name,v=(int)e.ID};
                });
			return ExController.JsonNet(new { Villages=items });
		}
		
		
		public JsonResult LoadRoomList(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

			se.SearchID="uv_VillageRoom";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_Room>();
            return ExController.JsonNet(result);
        }
		
		
		
		public JsonResult SaveRoom(HQ_Room entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据
		        
                entity.CreateBy=AppEnvironment.LoginUser.Name;
                entity.CreateOn=DateTime.Now;

	            #endregion
            }
            else
            {
                #region 维护修改数据
		        
                entity.EditBy=AppEnvironment.LoginUser.Name;
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
		
		
		
		public long DeleteRoom(HQ_Room entity)
        {
            return entity.Delete();
        }
		
    }
}