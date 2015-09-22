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
using G.Util.Html;
using G.BaseWeb;

namespace WebSite.Areas.Biz.Controllers
{
    public class ProductController : Controller
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

            return this.JsonNet(HtmlTree.InitTree(treeNodes));
        }

        public JsonResult SaveCategory(HZ_Catagory entity)
        {
            if (entity.ID == 0)
            {
                entity.CType = (int)CategoryTypeEnum.Product;
                entity.CreateBy = AppEnvironment.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = AppEnvironment.LoginUser.Name;
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
            return entity.Delete();
        }

        #endregion

        public JsonResult LoadInfo(int page_g, string categoryids, int psize_g = 10)
        {
            Sh_HZ_Product sh = new Sh_HZ_Product();
            sh.CategoryIds = categoryids;
            sh.PageIndex = page_g;
            sh.PageSize = psize_g;
            sh.OrderBy("Sort", EnumOrderBy.Desc);
            sh.OrderBy("CreateOn", EnumOrderBy.Desc);
            var result = sh.Load<HZ_Product>();
            return this.JsonNet(result);
        }

        public JsonResult SaveProduct(HZ_Product entity)
        {
            if (entity.ID == 0)
            {
                entity.CreateBy = AppEnvironment.LoginUser.Name;
                entity.CreateOn = DateTime.Now;
            }
            else
            {
                entity.EditBy = AppEnvironment.LoginUser.Name;
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
            return entity.Delete();
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