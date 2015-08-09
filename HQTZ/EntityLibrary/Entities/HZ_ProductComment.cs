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
    public class HZ_ProductComment : EntityBase
    {
        public HZ_ProductComment()
        {
            base.TableName = "HZ_ProductComment";
        }
        [JsonIgnore]
        public Int32 ProductID
        {
            get
            {
                return GetInt32("ProductID");
            }
            set
            {
                SetValue("ProductID", value);
            }
        }
        [JsonIgnore]
        public String Context
        {
            get
            {
                return GetString("Context");
            }
            set
            {
                SetValue("Context", value);
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
        public Int32 CommentUID
        {
            get
            {
                return GetInt32("CommentUID");
            }
            set
            {
                SetValue("CommentUID", value);
            }
        }
        [JsonIgnore]
        public String CommentUName
        {
            get
            {
                return GetString("CommentUName");
            }
            set
            {
                SetValue("CommentUName", value);
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
