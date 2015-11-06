using G.Util.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace G.Util.Mvc
{
    public static class ExController
    {
        public static JsonNetResult JsonNet(object data)
        {
            return JsonNet(null, data);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data)
        {
            return JsonNet(data, null, null, JsonRequestBehavior.DenyGet);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data, string contentType)
        {
            return JsonNet(data, contentType, null, JsonRequestBehavior.DenyGet);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data, string contentType, Encoding contentEncoding)
        {
            return JsonNet(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data, JsonRequestBehavior behavior)
        {
            return JsonNet(data, null, null, behavior);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data, string contentType, JsonRequestBehavior behavior)
        {
            return JsonNet(data, contentType, null, behavior);
        }

        public static JsonNetResult JsonNet(this Controller controller, object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return JsonNet(data, contentType, contentEncoding, behavior);
        }

        private static JsonNetResult JsonNet(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}

namespace G.Util.Net.Http
{
    public static class ExController
    {
        public static HttpResponseMessage JsonMessage(object data)
        {
            return JsonMessage(null, data);
        }
        public static HttpResponseMessage JsonMessage(this ApiController controller, object data)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(JSON.Stringify(data), Encoding.UTF8)
            };
        }
    }
}
