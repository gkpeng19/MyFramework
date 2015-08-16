using EntityLibrary;
using EntityLibrary.Entities;
using EntityLibrary.SearchEntities;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;

namespace WebSite.Areas.Biz.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 产品分类管理

        public JsonResult LoadProductCategories()
        {
            Sh_HZ_Catagory sh = new Sh_HZ_Catagory();
            sh["ctype"] = (int)CategoryTypeEnum.Product;
            sh.OrderBy("parentid", EnumOrderBy.Asc);
            sh.OrderBy("createon", EnumOrderBy.Desc);

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
                entity.CType = (int)CategoryTypeEnum.Product;
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

        public long DeleteCategory(HZ_Catagory entity)
        {
            return base.DeleteEntity(entity);
        }

        #endregion

        public JsonResult LoadInfo(int page, string categoryids, int psize = 10)
        {
            Sh_HZ_Product sh = new Sh_HZ_Product();
            sh.CategoryIds = categoryids;
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("Sort", EnumOrderBy.Desc);
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            var result = sh.Load<HZ_Product>();
            return this.JsonNet(result);
        }

        public JsonResult SaveProduct(HZ_Product entity)
        {
            if (entity.ID == 0)
            {
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

        public long DeleteProduct(HZ_Product entity)
        {
            return base.DeleteEntity(entity);
        }

        public int TransferProducts(EntityProducts entity)
        {
            try
            {
                entity.Save();
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}