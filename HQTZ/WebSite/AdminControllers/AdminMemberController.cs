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

namespace HQWZ.Controllers
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class Search_AMember : SearchEntity
    {
        #region 按会员用户名查询会员

        [Search(Operator = SearchOperator.Like)]
        public string UserName
        {
            set { SetValue("UserName", value); }
        }

        #endregion
    }

    public class AdminMemberController : Controller
    {
        public JsonResult LoadMemberList(Search_AMember se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "HQ_Member";
            //.........................
            se.OrderBy("isdelete", EnumOrderBy.Asc);
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_Member>();
            return ExController.JsonNet(result);
        }



        public JsonResult SaveMember(HQ_Member entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.UserPsw = G.Util.Tool.Encryption.GetMD5("123456");
                entity.CreateBy = LoginInfo.Current.UserName;
                entity.CreateOn = DateTime.Now;

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = LoginInfo.Current.UserName;
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


    }
}