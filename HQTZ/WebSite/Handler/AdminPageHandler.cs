using G.Util.Mvc.Permission;
using G.Util.Web.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Handler
{
    public class AdminPageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            if (!LoginVerify.IsLogin())
            {
                context.Response.WriteFile(context.Server.MapPath("~/AdminPages/Login.html"));
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