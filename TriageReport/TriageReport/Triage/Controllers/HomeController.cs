using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triage.Models;
using G.Util.Mvc;
using Triage.Models.ViewModels;
using GOMFrameWork;
using G.Util;

namespace Triage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id=363)
        {
            BaseTriageProcEntity pe = new BaseTriageProcEntity();
            pe.ProcName = "usp_getTriageRegionAndOSName";
            pe["testrunid"] = id;
            var laos = pe.ExecuteData<LanguageAndOSName>();
            ViewBag.Languages = laos.Data.LanguageList;
            ViewBag.OSNames = laos.Data.OSNameList;
            ViewBag.TPID = id;
            return View();
        }

        public JsonResult LoadTriageApps(BaseTriageProcEntity pe, int pageindex_c, int pagesize_c = 200)
        {
            pe.ProcName = "usp_getTriageApps";
            pe["pageindex"] = pageindex_c <= 0 ? 1 : pageindex_c;
            pe["pagesize"] = pagesize_c;
            var data = pe.Execute<TriageApp>();
            return this.JsonNet(new { data = data.Data, pager = Pager.InitPager(pageindex_c, data.PageCount, "loadDataGrid") });
        }

        public JsonResult LoadAppCaseRunDetail(int testrunid, int applicationid, int osid)
        {
            BaseTriageSearchEntity se = new BaseTriageSearchEntity("uv_testCaseRunDetail");
            se["TestRunID"] = testrunid;
            se["ApplicationID"] = applicationid;
            se["OSID"] = osid;
            var result = se.Load<TestCaseRunDetail>();
            return this.JsonNet(result.Data);
        }
    }
}
