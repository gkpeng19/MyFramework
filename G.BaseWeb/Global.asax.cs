using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using GOMFrameWork;
using G.BaseWeb.Models;
using System.Web.Mvc;
using System.Security.Principal;
using G.Util.Account;
using System.Threading;

namespace G.BaseWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DbContext.InitContext<BaseModel>("BaseWeb");
            DbContext.InitContext<BaseSearchModel>("BaseWeb");
            DbContext.InitContext<BaseProcModel>("BaseWeb");
        }

        void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            IPrincipal user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated
                && user.Identity.AuthenticationType == "Forms")
            {
                LoginInfo identity = new LoginInfo();
                CustomPrincipal principal = new CustomPrincipal(identity);
                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}