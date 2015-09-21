using G.BaseWeb;
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
            context.Response.ContentType = "text/plain";

            if (AppEnvironment.LoginUser == null)
            {
                context.Response.Redirect("~/Login/Index", false);
            }

            context.Response.Write("Hello");
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