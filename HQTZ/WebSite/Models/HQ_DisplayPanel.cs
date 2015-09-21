using G.Util.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_DisplayPanel : CommonModel
    {
        public HQ_DisplayPanel()
        {
            base.TableName = "HQ_DisplayPanel";
        }

        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }

        [JsonIgnore]
        public string ImgName
        {
            get { return GetString("ImgName"); }
            set { SetValue("ImgName", value); }
        }

        [JsonIgnore]
        public int DPanelType
        {
            get { return GetInt32("DPanelType"); }
            set { SetValue("DPanelType", value); }
        }
    }
}