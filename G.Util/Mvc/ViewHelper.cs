using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G.Util.Mvc
{
    public static class ViewHelper
    {
        public static string RenderViewToString(this ControllerContext context, string viewName, object model = null)
        {
            if (viewName == null && viewName.Length == 0)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            if (model != null)
            {
                context.Controller.ViewData.Model = model;
            }

            using (var sw = new StringWriter())
            {
                ViewEngineResult vr = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, vr.View, context.Controller.ViewData, context.Controller.TempData, sw);

                vr.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }

        }
    }
}
