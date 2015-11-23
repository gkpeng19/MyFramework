using G.Util.Web.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G.Simple
{
    public class HtmlPageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            if (!LoginVerify.IsLogin())
            {
                context.Response.WriteFile(context.Server.MapPath("~/Login.html"));
                return;
            }

            var filepath = context.Request.RawUrl;
            var spIndex = filepath.IndexOf('?');
            if (spIndex > 0)
            {
                filepath = filepath.Substring(0, spIndex);
            }
            context.Response.WriteFile(context.Server.MapPath(filepath));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}