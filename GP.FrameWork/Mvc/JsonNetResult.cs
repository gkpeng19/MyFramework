using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GP.FrameWork.Mvc
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (base.ContentType == null || base.ContentType.Length == 0)
            {
                response.ContentType = "application/json";
            }
            else
            {
                response.ContentType = base.ContentType;
            }


            if (base.ContentEncoding != null)
            {
                response.ContentEncoding = base.ContentEncoding;
            }

            if (base.Data != null)
            {
                var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
