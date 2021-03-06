﻿using G.Util.Mvc;
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
        public string ShopPsw
        {
            get { return GetString("ShopPsw"); }
            set { SetValue("ShopPsw", value); }
        }
        [JsonIgnore]
        public int UserType
        {
            get { return GetInt32("UserType"); }
            set { SetValue("UserType", value); }
        }

        public string UserType_G
        {
            get
            {
                if (UserType == 1)
                {
                    return "普通会员";
                }
                else if (UserType == 2)
                {
                    return "VIP会员";
                }
                else
                {
                    return "未知";
                }
            }
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
        public string MemberMedical
        {
            get { return GetString("MemberMedical"); }
            set { SetValue("MemberMedical", value); }
        }

        //[JsonIgnore]
        //public decimal Money
        //{
        //    get { return GetDecimal("Money"); }
        //    set { SetValue("Money", value); }
        //}
        [JsonIgnore]
        public int ShopUserID_G
        {
            get { return GetInt32("ShopUserID_G"); }
            set { SetValue("ShopUserID_G", value); }
        }

        [JsonIgnore]
        public decimal Balance_G
        {
            get
            {
                var balance = GetDecimal("Balance_G");
                balance = decimal.Round(balance, 2);
                return balance;
            }
            set { SetValue("Balance_G", value); }
        }

        public decimal ShortBalance_C
        {
            get
            {
                return Balance_G;
            }
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
                if (img == null || img.Length == 0)
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

        [JsonIgnore]
        public DateTime OpenVipDate
        {
            get { return GetDateTime("OpenVipDate"); }
            set { SetValue("OpenVipDate", value); }
        }
    }
}