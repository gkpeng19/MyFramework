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
    public class BW_User : BaseCommonModel
    {
        public BW_User()
        {
            base.TableName = "BW_User";
        }

        [JsonIgnore]
        public string UserName
        {
            get { return GetString("UserName"); }
            set { SetValue("UserName", value); }
        }
        [JsonIgnore]
        public string UserPsw
        {
            get { return GetString("UserPsw"); }
            set { SetValue("UserPsw", value); }
        }
        [JsonIgnore]
        public int UserType
        {
            get { return GetInt32("UserType"); }
            set { SetValue("UserType", value); }
        }
        [JsonIgnore]
        public int UserRole
        {
            get { return GetInt32("UserRole"); }
            set { SetValue("UserRole", value); }
        }
        [JsonIgnore]
        public string UserRole_G
        {
            get { return GetString("UserRole_G"); }
            set { SetValue("UserRole_G", value); }
        }
    }
}