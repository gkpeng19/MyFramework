using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_Member : CommonModel
    {
        public HQ_Member()
        {
            base.TableName = "HQ_Member";
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

        [JsonIgnore]
        public string HeaderImg
        {
            get { return GetString("HeaderImg"); }
            set { SetValue("HeaderImg", value); }
        }

        public string HeaderImg_G
        {
            get
            {
                var img = HeaderImg;
                if (img == null||img.Length==0)
                {
                    return "Images/header.jpg";
                }
                return img;
            }
        }

        [JsonIgnore]
        [UIValueZeroNotEqualNull]
        public int IsDelete
        {
            get { return GetInt32("IsDelete"); }
            set { SetValue("IsDelete", value); }
        }
    }
}