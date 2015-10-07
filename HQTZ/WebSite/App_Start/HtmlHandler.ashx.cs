using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.App_Start
{
    /// <summary>
    /// HtmlHandler 的摘要说明
    /// </summary>
    public class HtmlHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            if (LoginInfo.Current == null)
            {
                context.Response.WriteFile(context.Server.MapPath("~/AdminPages/Login.html"));
                return;
            }

            context.Response.WriteFile(context.Server.MapPath(context.Request.RawUrl));
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