using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HZ_Catagory : EntityBase
    {
        public HZ_Catagory()
        {
            base.TableName = "HZ_Catagory";
        }
        [JsonIgnore]
        public String Code
        {
            get
            {
                return GetString("Code");
            }
            set
            {
                SetValue("Code", value);
            }
        }
        [JsonIgnore]
        public String Name
        {
            get
            {
                return GetString("Name");
            }
            set
            {
                SetValue("Name", value);
            }
        }
        [JsonIgnore]
        public Int32 CType
        {
            get
            {
                return GetInt32("CType");
            }
            set
            {
                SetValue("CType", value);
            }
        }
        [JsonIgnore]
        public Int32 ParentID
        {
            get
            {
                return GetInt32("ParentID");
            }
            set
            {
                SetValue("ParentID", value);
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
        [JsonIgnore]
        public String CreateBy
        {
            get
            {
                return GetString("CreateBy");
            }
            set
            {
                SetValue("CreateBy", value);
            }
        }
        [JsonIgnore]
        public DateTime EditOn
        {
            get
            {
                return GetDateTime("EditOn");
            }
            set
            {
                SetValue("EditOn", value);
            }
        }
        public string EditOn_G
        {
            get
            {
                if (EditOn == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return EditOn.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        [JsonIgnore]
        public String EditBy
        {
            get
            {
                return GetString("EditBy");
            }
            set
            {
                SetValue("EditBy", value);
            }
        }
    }
}
