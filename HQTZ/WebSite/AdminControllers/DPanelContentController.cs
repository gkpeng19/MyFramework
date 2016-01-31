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

namespace WebSite.AdminControllers
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

        public JsonResult LoadDContentDesc(SearchModel se, int page_g, int psize_g)
        {
            se.SearchID = "hq_article";
            se["acategory"] = (int)EnumArticleCategory.RoomDesc;
            se.OrderBy("id");
            se.PageIndex = page_g;
            se.PageSize = psize_g;

            return ExController.JsonNet(se.Load<HQ_Article>());
        }

        [ValidateInput(false)]
        public JsonResult SaveDDetail(HQ_DisplayContent entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateBy = LoginInfo.Current.Name;
                entity.CreateOn = DateTime.Now;

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = LoginInfo.Current.Name;
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

        [ValidateInput(false)]
        public JsonResult SaveDDetailDesc(HQ_Article article)
        {
            if (article.ID == 0)
            {
                article.ACategory = (int)EnumArticleCategory.RoomDesc;
                article.CreateBy = LoginInfo.Current.UserName;
                article.CreateOn = DateTime.Now;
            }
            else
            {
                article.EditBy = LoginInfo.Current.UserName;
                article.EditOn = DateTime.Now;
            }

            var r = article.Save();
            if (r > 0)
            {
                return ExController.JsonNet(article);
            }
            return ExController.JsonNet(new { ID = 0 });
        }

        public long DeleteDDetailDesc(HQ_Article article)
        {
            return article.Delete();
        }
    }
}