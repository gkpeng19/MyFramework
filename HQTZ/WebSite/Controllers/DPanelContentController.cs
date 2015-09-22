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
    public class DPanelContentController : Controller
    {
        public JsonResult LoadJsonData(int dPanelType)
        {
            SearchModel sm = new SearchModel("HQ_DisplayPanel");
            sm["DPanelType"] = dPanelType;
            var distinations = sm.Load<HQ_DisplayPanel>().Data;
            var select = HtmlSelect.GetHtmlSelectByCollection<HQ_DisplayPanel>(distinations, (e) =>
                {
                    return new HtmlSelectItem() { k = e.Name, v = (int)e.ID };
                });
            return ExController.JsonNet(new { Detinations = select });
        }

        public JsonResult LoadDContentList(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "uv_DisplayContent";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_DisplayContent>();
            return ExController.JsonNet(result);
        }


        [ValidateInput(false)]
        public JsonResult SaveDDetail(HQ_DisplayContent entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateBy = AppEnvironment.LoginUser.Name;
                entity.CreateOn = DateTime.Now;

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = AppEnvironment.LoginUser.Name;
                entity.EditOn = DateTime.Now;

                #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }

        public long DeleteDDetail(HQ_DisplayContent entity)
        {
            return entity.Delete();
        }

    }
}