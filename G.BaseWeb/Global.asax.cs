﻿using G.BaseWeb.Models;
using G.Util.Account;
using G.Util.Extension;
using GOMFrameWork;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace G.BaseWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DbContext.InitContext<BaseModel>("BaseWeb");
            DbContext.InitContext<BaseSearchModel>("BaseWeb");
            DbContext.InitContext<BaseProcModel>("BaseWeb");

            //DbContext.InitContext<SimpleBase>("SimpleOracle");
            //DbContext.InitContext<SimpleSearchModel>("SimpleOracle");
            //DbContext.InitContext<OracleProcEntity>("SimpleOracle");
            DbContext.InitContext<SimpleBase>("SimpleSqlServer");
            DbContext.InitContext<SimpleSearchModel>("SimpleSqlServer");
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
