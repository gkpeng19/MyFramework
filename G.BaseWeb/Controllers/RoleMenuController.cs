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
    public class RoleMenuController : Controller
    {
        public JsonNetResult LoadMenuTree()
        {
            BaseSearchModel se = new BaseSearchModel("bw_menu");

            se.OrderBy("parentid", EnumOrderBy.Asc);
            se.OrderBy("id");

            var r = se.Load<BW_Menu>();
            List<HtmlTreeNode> treeNodes = new List<HtmlTreeNode>();
            foreach (var d in r.Data)
            {
                treeNodes.Add(new HtmlTreeNode() { id = (int)d.ID, text = d.Name, parentid = d.ParentID });
            }

            return ExController.JsonNet(HtmlTree.InitTree(treeNodes));
        }



        public JsonNetResult LoadRoles(BaseSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "bw_role";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<BW_Role>();
            return ExController.JsonNet(result);
        }

        public JsonResult LoadRoleMenus(int roleid)
        {
            BaseSearchModel se = new BaseSearchModel("bw_rolemenu");
            se["roleid"] = roleid;
            var result = se.Load<BW_RoleMenu>();
            return this.JsonNet(result.Data);
        }

        public int SaveRoleMenus(int roleid_g, ReferenceModel<BW_RoleMenu> data)
        {
            BaseSearchModel sm = new BaseSearchModel("BW_RoleMenu");
            sm["roleid"] = roleid_g;
            var olds = sm.Load<BW_RoleMenu>();
            foreach (BW_RoleMenu old in olds.Data)
            {
                var target = data.Contexts.Where(e => e.MenuID == old.MenuID);
                if (target.Count() > 0)
                {
                    data.Contexts.Remove(target.ElementAt(0));
                }
                else
                {
                    old["IsDelete"] = 1;
                    data.Contexts.Add(old);
                }
            }

            try
            {
                data.Save();
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}