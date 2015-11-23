using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Html;
using G.Util.Extension;
using GOMFrameWork.DataEntity;
using System.Text.RegularExpressions;
using G.Simple.Entity;
using System.Web.Http.ModelBinding;
using G.Util.Net.Http;
using System.Web.Http;
using System.Net.Http;
using G.Util.Account;

namespace G.Simple.Api.Controllers
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

    [Authorize]
    [RoutePrefix("api/Simple")]
    public class SimpleController : ApiController
    {
        [AcceptVerbs("post")]
        [AllowAnonymous]
        [Route("Login")]
        public void Login()
        {
            LoginInfo user = new LoginInfo("gkpeng");
            user.UserID = 1;
            user.UserRole = 1;
            LoginInfo.SetLoginToken(user, true);
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoginOut")]
        public void LoginOut()
        {
            LoginInfo.LoginOut();
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadPContent")]
        public HttpResponseMessage LoadPContent()
        {
            var user = LoginInfo.Current;
            return ExController.JsonMessage(new { STypes = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumSType)), PStates = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumGLState)) });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadEditContent")]
        public HttpResponseMessage LoadEditContent(int pid)
        {
            SimpleSearchModel se = new SimpleSearchModel("GreenLand");
            se["ID"] = pid;
            var green = se.LoadEntity<GreenLand>();
            return ExController.JsonMessage(new
            {
                STypes = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumSType)),
                PStates = HtmlSelect.GetHtmlSelectByEnum(typeof(EnumGLState)),
                ProjectInfo = green
            });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadProjects")]
        public HttpResponseMessage LoadProjects(SimpleSearchModel se)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "GreenLand";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = 1;
            se.PageSize = 10;

            var result = se.Load<GreenLand>();
            return ExController.JsonMessage(result);
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("SaveProject")]
        public HttpResponseMessage SaveProject(GreenLand entity)
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
                return ExController.JsonMessage(entity);
            }
            return ExController.JsonMessage(new { ID = 0 });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("DeleteProject")]
        public long DeleteProject(GreenLand entity)
        {
            return entity.Delete();
        }
    }
}