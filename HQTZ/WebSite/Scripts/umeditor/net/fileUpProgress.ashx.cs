using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Scripts.umeditor.net
{
    public class fileUpProgress : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;

            var progress = HttpContext.Current.Cache[context.Request["progresskey"]];
            if (progress == null)
            {
                progress = 1;
            }
            context.Response.Write(progress);
            context.Response.End();
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