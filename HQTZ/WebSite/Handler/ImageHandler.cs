using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Handler
{
    public class ImageHandler : IHttpHandler
    {
        static int[] wsizes = new int[] { 42, 260, 420, 480, 580 };
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string path = context.Request.Path;

            var size = context.Request["size"];
            if (size != null && size.Length > 0)
            {
                int width = int.Parse(size);
                for (int i = 0; i < wsizes.Length; ++i)
                {
                    if ((i == 0 && width < wsizes[0]) ||
                        (i == wsizes.Length - 1) ||
                        (width == wsizes[i] || (width > wsizes[i] && width <= wsizes[i + 1])))
                    {
                        path = path.Insert(path.IndexOf('/', 8), "/size" + wsizes[i]);
                        break;
                    }
                }
            }
            path = context.Server.MapPath(path);
            if (File.Exists(path))
            {
                context.Response.WriteFile(path);
                return;
            }
            context.Response.Write(null);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}