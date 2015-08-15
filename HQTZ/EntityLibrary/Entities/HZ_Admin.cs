using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using G.Util.Extension;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HZ_Admin : EntityBase
    {
        public HZ_Admin()
        {
            base.TableName = "HZ_Admin";
        }
        [JsonIgnore]
        public String UserName
        {
            get
            {
                return GetString("UserName");
            }
            set
            {
                SetValue("UserName", value);
            }
        }
        [JsonIgnore]
        public String UPassword
        {
            get
            {
                return GetString("UPassword");
            }
            set
            {
                SetValue("UPassword", value);
            }
        }
        [JsonIgnore]
        public Int32 UserRole
        {
            get
            {
                return GetInt32("UserRole");
            }
            set
            {
                SetValue("UserRole", value);
            }
        }

        public string UserRole_G
        {
            get { return EnumExtension.GetDescription((UserRoleEnum)UserRole); }
        }
        [JsonIgnore]
        public String Email
        {
            get
            {
                return GetString("Email");
            }
            set
            {
                SetValue("Email", value);
            }
        }
        [JsonIgnore]
        public String Phone
        {
            get
            {
                return GetString("Phone");
            }
            set
            {
                SetValue("Phone", value);
            }
        }
        [JsonIgnore]
        public Int32 IsDelete
        {
            get
            {
                return GetInt32("IsDelete");
            }
            set
            {
                SetValue("IsDelete", value);
            }
        }
        [JsonIgnore]
        public String Remark
        {
            get
            {
                return GetString("Remark");
            }
            set
            {
                SetValue("Remark", value);
            }
        }
        [JsonIgnore]
        public DateTime CreateOn
        {
            get
            {
                return GetDateTime("CreateOn");
            }
            set
            {
                SetValue("CreateOn", value);
            }
        }
        public string CreateOn_G
        {
            get
            {
                if (CreateOn == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return CreateOn.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
