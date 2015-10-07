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
    public class DPanelController : Controller
    {
        #region 目的地

        public JsonResult LoadDestinations(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_DisplayPanel";
            se["DPanelType"] = (int)EnumDPanelType.Distination;
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

                entity.DPanelType = (int)EnumDPanelType.Distination;
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

        #endregion

        #region 得天独厚

        public JsonResult LoadMemberBenefit(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_DisplayPanel";
            se["DPanelType"] = (int)EnumDPanelType.MemberBenefit;
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_DisplayPanel>();
            return ExController.JsonNet(result);
        }

        public JsonResult SaveBenefit(HQ_DisplayPanel entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.DPanelType = (int)EnumDPanelType.MemberBenefit;
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

        #endregion

        #region 尊享服务

        public JsonResult LoadMemberService(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_DisplayPanel";
            se["DPanelType"] = (int)EnumDPanelType.MemberService;
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_DisplayPanel>();
            return ExController.JsonNet(result);
        }

        public JsonResult SaveService(HQ_DisplayPanel entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.DPanelType = (int)EnumDPanelType.MemberService;
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

        #endregion

        #region 最新优惠

        public JsonResult LoadNewCheaper(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_DisplayPanel";
            se["DPanelType"] = (int)EnumDPanelType.NewCheaper;
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_DisplayPanel>();
            return ExController.JsonNet(result);
        }

        public JsonResult SaveNewCheaper(HQ_DisplayPanel entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.DPanelType = (int)EnumDPanelType.NewCheaper;
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

        #endregion

        public long DeletePanel(HQ_DisplayPanel entity)
        {
            return entity.Delete();
        }

    }
}