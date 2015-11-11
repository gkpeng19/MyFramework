using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using G.Util.Html;
using System.Web.Mvc;
using G.Util.Extension;
using G.BaseWeb.Models;
using GOMFrameWork.DataEntity;
using System.Text.RegularExpressions;

namespace Project1.Controllers
{
    [ModelBinder(typeof(EntityModelBinder))]
    [OracleOutParam("DList", "DEntity")]
    public class OracleDataProcEntity : OracleProcEntity
    {
        public OracleDataProcEntity() { }
    }

    public class DataResult
    {
        public List<GreenLand> DList { get; set; }
        public S_Order DEntity { get; set; }
    }

    public class SimpleController : Controller
    {
        public void AAAAA()
        {
            SimpleSearchModel se = new SimpleSearchModel("GreenLand");
            se["ID"] = 1;
            var green = se.LoadEntity<GreenLand>();
            Regex reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            var value= reg.Replace(green.Description, (match) => { return string.Format("<img _src='{0}' />", match.Groups[1].Value); });
        }

        public void TestProc(OracleProcEntity pe)
        {
        }

        public void TestListProc(OracleListProcEntity pe)
        {
            pe.ProcName = "usp_outlist";
            var result = pe.Execute<GreenLand>();
        }

        public void TestPagerProc(OraclePagerProcEntity pe)
        {
            pe.ProcName = "usp_outpager";
            var result = pe.Execute<GreenLand>();
        }

        public void TestDataProc(OracleDataProcEntity pe)
        {
            pe.ProcName = "usp_outdata";
            var result = pe.ExecuteData<DataResult>();
        }


        public JsonResult LoadPContent()
        {
            return ExController.JsonNet(new { STypes = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumSType)), PStates = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumGLState)) });
        }

        public JsonResult LoadEditContent(int pid)
        {
            SimpleSearchModel se = new SimpleSearchModel("GreenLand");
            se["ID"] = pid;
            var green = se.LoadEntity<GreenLand>();
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