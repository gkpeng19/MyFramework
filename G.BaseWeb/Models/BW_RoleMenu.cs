using G.Util.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G.BaseWeb.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class BW_RoleMenu : BaseModel
    {
        public BW_RoleMenu()
        {
            base.TableName = "BW_RoleMenu";
        }
        [JsonIgnore]
        public int RoleID
        {
            get { return GetInt32("RoleID"); }
            set { SetValue("RoleID", value); }
        }
        [JsonIgnore]
        public int MenuID
        {
            get { return GetInt32("MenuID"); }
            set { SetValue("MenuID", value); }
        }
    }
}