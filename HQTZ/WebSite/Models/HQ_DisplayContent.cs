﻿using G.Util.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_DisplayContent : CommonModel
    {
        public HQ_DisplayContent()
        {
            base.TableName = "HQ_DisplayContent";
        }

        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }

        [JsonIgnore]
        public string DContent
        {
            get { return GetString("DContent"); }
            set { SetValue("DContent", value); }
        }

        [JsonIgnore]
        public int DPanelID
        {
            get { return GetInt32("DPanelID"); }
            set { SetValue("DPanelID", value); }
        }

        [JsonIgnore]
        public string DPanel_G
        {
            get { return GetString("DPanel_G"); }
            set { SetValue("DPanel_G", value); }
        }
    }
}