using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Scripts.umeditor.net
{
    public class fileUp : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;

            string progresskey = context.Request["progresskey"];

            //上传配置
            string pathbase = "upload/"; //保存路径

            //文件允许格式
            string[] filetypes = null;
            string callback = null;
            if (context.Request["iseditor"] == null)//从百度编辑器上传
            {
                string editorId = context.Request["editorid"];
                filetypes = new string[] { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };
                pathbase = pathbase + "images/";

                callback = context.Request["callback"];
            }
            else
            {
                string filter = context.Request["filter"];
                if (filter != null && filter.Length > 0)
                {
                    if (filter.Equals("image"))
                    {
                        filetypes = new string[] { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };
                        pathbase = pathbase + "images/";
                    }
                    else if (filter.Equals("video"))
                    {
                        filetypes = new string[] { ".mp4", ".flv" };
                        pathbase = pathbase + "videos/";
                    }
                    else if (filter.Equals("application"))
                    {
                        filetypes = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
                        pathbase = pathbase + "documents/";
                    }
                }
            }

            int size = 0;                     //文件大小限制,单位mb
            string maxSize = context.Request["maxSize"];
            if (maxSize != null && maxSize.Length > 0)
            {
                int.TryParse(maxSize, out size);
            }
            if (size == 0)
            {
                size = 10;
            }

            //上传图片
            Hashtable info;
            UMeditorUploader up = new UMeditorUploader();
            info = up.upFile(context, "../../../" + pathbase, filetypes, size, progresskey); //获取上传状态
            info["path"] = pathbase + info["path"];
            string json = BuildJson(info);

            context.Response.ContentType = "text/html";
            if (callback != null)
            {
                context.Response.Write(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
            }
            else
            {
                context.Response.Write(json);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "originalName", "name", "url", "path", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }
    }
}