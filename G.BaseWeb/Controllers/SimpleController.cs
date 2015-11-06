using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using G.Util.Extension;
using G.BaseWeb.Models;

namespace Project1.Controllers
{
    public class SimpleController : Controller
    {

        public JsonResult LoadPContent()
        {
            return ExController.JsonNet(new { STypes = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumSType)), PStates = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumGLState)) });
        }

        public JsonResult LoadEditContent(int pid)
        {
            SimpleSearchModel se = new SimpleSearchModel("GreenLand");
            se["ID"] = pid;
            var green= se.LoadEntity<GreenLand>();
            return ExController.JsonNet(new
            {
                STypes = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumSType)),
                PStates = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumGLState)),
                ProjectInfo = green
            });
        }


        public JsonResult LoadProjects(SimpleSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "GreenLand";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<GreenLand>();
            return ExController.JsonNet(result);
        }


        [ValidateInput(false)]
        public JsonResult SaveProject(GreenLand entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.CreateOn = DateTime.Now;
                entity.CreateBy = "gkpeng";

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditOn = DateTime.Now;
                entity.EditBy = "gkpeng";

                #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }

        public long DeleteProject(GreenLand entity)
        {
            return entity.Delete();
        }
    }
}