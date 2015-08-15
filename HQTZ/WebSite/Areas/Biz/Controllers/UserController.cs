using EntityLibrary;
using G.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using EntityLibrary.SearchEntities;
using EntityLibrary.Entities;

namespace WebSite.Areas.Biz.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Biz/User/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCtnData()
        {
            var userLevels = CommonUtil.GetHtmlSelectItems(typeof(UserLevelEnum));
            var userTypes = CommonUtil.GetHtmlSelectItems(typeof(UserTypeEnum));
            return this.JsonNet(new { UserLevels = userLevels, UserTypes = userTypes });
        }

        public JsonResult LoadUsers(int page, int psize = 10)
        {
            Sh_HZ_User sh = new Sh_HZ_User();
            sh.PageIndex = page;
            sh.PageSize = psize;
            sh.OrderBy("CreateOn", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            return this.JsonNet(sh.Load<HZ_User>());
        }

        public JsonResult SaveUser(HZ_User entity)
        {
            if (entity.ID == 0)
            {
                entity.UPassword = CommonUtil.GetMD5("123456");
                entity.CreateOn = DateTime.Now;
            }

            var r = entity.Save();
            if (r > 0)
            {
                return this.JsonNet(entity);
            }
            else
            {
                return this.JsonNet(new { ID = 0 });
            }
        }

        public long DeleteUser(HZ_User entity)
        {
            return base.DeleteEntity(entity);
        }
    }
}