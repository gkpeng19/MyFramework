using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using G.BaseWeb.Models;
using System.Web.Mvc;
using G.Util.Tool;

namespace G.BaseWeb.Controllers
{
    public class UserController : Controller
    {


        public JsonNetResult LoadRoleTree()
        {
            BaseSearchModel se = new BaseSearchModel("bw_role");

            se.OrderBy("parentid", EnumOrderBy.Asc);
            se.OrderBy("id");

            var r = se.Load<BW_Role>();
            List<HtmlTreeNode> treeNodes = new List<HtmlTreeNode>();
            foreach (var d in r.Data)
            {
                treeNodes.Add(new HtmlTreeNode() { id = (int)d.ID, text = d.RoleName, parentid = d.ParentID });
            }

            return ExController.JsonNet(HtmlTree.InitTree(treeNodes));
        }



        public JsonNetResult LoadUsers(BaseSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "uv_bw_user";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<BW_User>();
            return ExController.JsonNet(result);
        }



        public JsonNetResult SaveRoleNode(BW_Role entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateBy = "";
                entity.CreateOn = DateTime.Now;

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = "";
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


        public JsonNetResult SaveUser(BW_User entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateBy = "";
                entity.CreateOn = DateTime.Now;
                entity.UserPsw = Encryption.GetMD5("123456");

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = "";
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

        public int TransferUser(TransferModel<BW_User> entity)
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

        public long DeleteRoleNode(BW_Role entity)
        {
            return entity.Delete();
        }


        public long DeleteUser(BW_User entity)
        {
            return entity.Delete();
        }

    }
}