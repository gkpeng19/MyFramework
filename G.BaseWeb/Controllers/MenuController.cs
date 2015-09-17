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
    public class MenuController : Controller
    {
        public JsonNetResult LoadMenus()
        {
            BaseSearchModel se = new BaseSearchModel("bw_menu");

            se.OrderBy("parentid", EnumOrderBy.Asc);
            se.OrderBy("id", EnumOrderBy.Asc);

            var r = se.Load<BW_Menu>();
            List<HtmlTreeNode> treeNodes = new List<HtmlTreeNode>();
            foreach (var d in r.Data)
            {
                treeNodes.Add(new HtmlTreeNode() { id = (int)d.ID, text = d.Name, parentid = d.ParentID });
            }

            return ExController.JsonNet(HtmlTree.InitTree(treeNodes));
        }




        public JsonNetResult SaveMenu(BW_Menu entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateOn = DateTime.Now;
                entity.CreateBy = "";

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditOn = DateTime.Now;
                entity.EditBy = "";

                #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }



        public long DeleteMenu(BW_Menu entity)
        {
            return entity.Delete();
        }

    }
}