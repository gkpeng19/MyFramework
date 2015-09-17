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
    public class BW_Role : BaseCommonModel
    {
        public BW_Role()
        {
            base.TableName = "BW_Role";
        }

        [JsonIgnore]
        public string RoleCode
        {
            get { return GetString("RoleCode"); }
            set { SetValue("RoleCode", value); }
        }
        [JsonIgnore]
        public string RoleName
        {
            get { return GetString("RoleName"); }
            set { SetValue("RoleName", value); }
        }
        [JsonIgnore]
        public int ParentID
        {
            get { return GetInt32("ParentID"); }
            set { SetValue("ParentID", value); }
        }
    }
}