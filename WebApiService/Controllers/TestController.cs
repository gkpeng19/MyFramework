using GOMFrameWork.DataEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using G.Util.Net.Http;

namespace BaseApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [AcceptVerbs("get", "post")]
        [Route("GetName")]
        public HttpResponseMessage GetName(string name, string psw)
        {
            return this.JsonMessage(new { name = name, psw = psw });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("GetExtName")]
        public HttpResponseMessage GetExtName(User user)
        {
            return this.JsonMessage(new { name = user.Name, psw = user.Psw });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("GetStringName")]
        public string GetStringName()
        {
            return "gkpeng";
        }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class User : EntityBase
    {
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        public string Psw
        {
            get { return GetString("Psw"); }
            set { SetValue("Psw", value); }
        }
    }
}