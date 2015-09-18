using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G.Util.Mvc;
using System.Web.Mvc;

namespace G.BaseWeb.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class BW_Menu:BaseCommonModel
    {
        public BW_Menu()
        {
            base.TableName = "BW_Menu";
        }

        [JsonIgnore]
        public string Code
        {
            get { return GetString("Code"); }
            set { SetValue("Code", value); }
        }
        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        [JsonIgnore]
        public int ParentID
        {
            get { return GetInt32("ParentID"); }
            set { SetValue("ParentID", value); }
        }

        [JsonIgnore]
        public int MenuType
        {
            get { return GetInt32("MenuType"); }
            set { SetValue("MenuType", value); }
        }
    }
}