using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_Admin:CommonModel
    {
        public HQ_Admin()
        {
            base.TableName = "HQ_Admin";
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
        public string TrueName
        {
            get { return GetString("TrueName"); }
            set { SetValue("TrueName", value); }
        }
        [JsonIgnore]
        public string PhoneNum
        {
            get { return GetString("PhoneNum"); }
            set { SetValue("PhoneNum", value); }
        }
        [JsonIgnore]
        public string Email
        {
            get { return GetString("Email"); }
            set { SetValue("Email", value); }
        }
    }
}