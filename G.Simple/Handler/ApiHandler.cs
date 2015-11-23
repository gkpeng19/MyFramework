using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace G.Simple
{
    public class ApiHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            var t = HttpClientUtil.DoRequest(context);
            context.Response.BinaryWrite(t);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}