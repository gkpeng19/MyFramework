﻿using G.Simple.Entity;
using G.Util.Account;
using G.Util.Extension;
using GOMFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace G.Simple.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DbContext.InitContext<SimpleBase>("SimpleOracle");
            DbContext.InitContext<SimpleSearchModel>("SimpleOracle");
            DbContext.InitContext<OracleProcEntity>("SimpleOracle");
            //DbContext.InitContext<SimpleBase>("SimpleSqlServer");
            //DbContext.InitContext<SimpleSearchModel>("SimpleSqlServer");
        }
    }
}
