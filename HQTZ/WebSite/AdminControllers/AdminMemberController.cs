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
using WebSite.Controllers;
using G.Util.Extension;

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

        public long ImportMember(string fpath)
        {
            fpath = Server.MapPath("~/" + fpath);
            if (System.IO.File.Exists(fpath))
            {
                var list = G.Util.Tool.ExcelHelper.Read<HQ_Member>(fpath, new string[] {
                "UserName","PhoneNum"
                }, 1, (e) =>
                {
                    e.UserPsw = MD5.EncryptString(e.PhoneNum.Substring(5));
                    e.UserType = (int)EnumUserType.Normal;
                    e.CreateBy = LoginInfo.Current.UserName;
                    e.CreateOn = DateTime.Now;
                });

                EntityList<HQ_Member> elist = new EntityList<HQ_Member>() { List = list };
                return elist.Save();
            }
            else
            {
                return 0;
            }
        }

        public JsonResult SaveMember(HQ_Member entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.UserPsw = MD5.EncryptString("123456");
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