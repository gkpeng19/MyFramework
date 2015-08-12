﻿using EntityLibrary.Entities;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using GOMFrameWork;
using EntityLibrary.SearchEntities;
using EntityLibrary;

namespace WebSite.Areas.Biz.Controllers
{
    public class ContextController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoIndex()
        {
            return View();
        }

        public ActionResult AdvertIndex()
        {
            return View();
        }

        #region 内容管理

        public JsonResult LoadContext(int page, int psize = 10)
        {
            Sh_HZ_Context sh = new Sh_HZ_Context();
            sh.CType = (int)ContextTypeEnum.Context;
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("IsTop", EnumOrderBy.Desc);
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            CommonResult<HZ_Context> result = sh.Load<HZ_Context>();
            return this.JsonNet(result);
        }

        [ValidateInput(false)]
        public JsonResult SaveContext(HZ_Context entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)ContextTypeEnum.Context;
                entity.CreateBy = base.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = base.LoginUser.Name;
                entity.EditOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        #endregion

        #region 资讯管理

        #region 资讯分类管理

        public JsonResult LoadInfoCategories()
        {
            Sh_HZ_Catagory sh = new Sh_HZ_Catagory();
            sh["ctype"] = (int)CategoryTypeEnum.Info;
            sh.OrderBy("parentid", EnumOrderBy.Asc);

            var r = sh.Load<HZ_Catagory>();
            List<HtmlTreeNode> treeNodes = new List<HtmlTreeNode>();
            foreach (var c in r.Data)
            {
                treeNodes.Add(new HtmlTreeNode() { id = (int)c.ID, text = c.Name, parentid = c.ParentID });
            }

            return this.JsonNet(CommonUtil.InitTree(treeNodes));
        }

        public JsonResult SaveCategory(HZ_Catagory entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)CategoryTypeEnum.Info;
                entity.CreateBy = base.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = base.LoginUser.Name;
                entity.EditOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        #endregion

        public JsonResult LoadInfo(int page, int psize = 10, int categoryid = 0)
        {
            Sh_HZ_Context sh = new Sh_HZ_Context();
            sh.CType = (int)ContextTypeEnum.Info;
            if (categoryid > 0)
            {
                sh["categoryid"] = categoryid;
            }
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("IsTop", EnumOrderBy.Desc);
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            CommonResult<HZ_Context> result = sh.Load<HZ_Context>();
            return this.JsonNet(result);
        }

        [ValidateInput(false)]
        public JsonResult SaveInfo(HZ_Context entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)ContextTypeEnum.Info;
                entity.CreateBy = base.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = base.LoginUser.Name;
                entity.EditOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        #endregion

        #region 广告管理

        public JsonResult LoadAdvert(int page, int psize = 10)
        {
            Sh_HZ_Context sh = new Sh_HZ_Context();
            sh.CType = (int)ContextTypeEnum.Advert;
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            CommonResult<HZ_Context> result = sh.Load<HZ_Context>();
            return this.JsonNet(result);
        }

        [ValidateInput(false)]
        public JsonResult SaveAdvert(HZ_Context entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)ContextTypeEnum.Advert;
                entity.CreateBy = base.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = base.LoginUser.Name;
                entity.EditOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            return this.JsonNet(new { ID = 0 });
        }

        #endregion

        public long DeleteContext(HZ_Context entity)
        {
            return base.DeleteEntity(entity);
        }
    }
}