using EntityLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using System.Web.Http.Cors;
using G.Util.Tool;
using Newtonsoft.Json;
using GOMFrameWork.DataEntity;

namespace WebSite.Controllers
{
    public class PhoneController : Controller
    {
        public void LoadAllValliages(int page = 1, int psize = 10)
        {
            SearchModel sm = new SearchModel("uv_DisplayContent");
            sm["DPanelType"] = (int)EnumDPanelType.Distination;
            sm.OrderBy("id");
            sm.PageIndex = page;
            sm.PageSize = psize;
            sm.AddSearch("id", "DPanel_G", "Name", "ImgName_G");
            var result = sm.Load<HQ_DisplayContent>();
            OutResult(result);
        }

        public void LoadTraveGuides(int page = 1, int psize = 10)
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.TravelGuide;
            sm.OrderBy("id", GOMFrameWork.DataEntity.EnumOrderBy.Desc);
            sm.PageIndex = page;
            sm.PageSize = psize;
            var result = sm.Load<HQ_Article>();
            foreach (var d in result.Data)
            {
                var title = d.Title;
                if (title.Length > 25)
                {
                    title = title.Substring(0, 25) + "...";
                    d.Title = title;
                }
                var content = System.Text.RegularExpressions.Regex.Replace(d.AContent, "<[^>]*>", "");
                if (content.Length > 30)
                {
                    content = content.Substring(0, 30);
                }
                var cindex = content.LastIndexOf('<');
                if (cindex >= 0)
                {
                    content = content.Substring(0, cindex);
                }
                d.AContent = content;
            }
            OutResult(result);
        }

        public void LoadArticleDetail(int ptype, int vid)
        {
            SearchModel sm = null;
            switch (ptype)
            {
                case 1:
                    sm = new SearchModel("HQ_DisplayContent");
                    sm["ID"] = vid;
                    sm.AddSearch("Name", "DContent");
                    var dcon = sm.LoadEntity<HQ_DisplayContent>();
                    if (dcon != null)
                    {
                        OutResult(new { Name = dcon.Name, Content = dcon.DContent });
                        return;
                    }
                    break;
                case 2:
                    sm = new SearchModel("HQ_Article");
                    sm["ID"] = vid;
                    sm.AddSearch("Title", "AContent");
                    var article = sm.LoadEntity<HQ_Article>();
                    if (article != null)
                    {
                        OutResult(new { Name = article.Title, Content = article.AContent });
                        return;
                    }
                    break;
            }

            OutResult(new { Name = "...", Content = "您要查找的内容不存在！" });
        }

        void OutResult(object result)
        {
            HttpResponseBase response = this.HttpContext.Response;
            response.AddHeader("Access-Control-Allow-Origin", "http://127.0.0.1:8020");
            response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");
            response.AddHeader("Cache-Control", "no-cache");
            response.Write(JSON.Stringify(result));
            response.End();
        }
    }
}