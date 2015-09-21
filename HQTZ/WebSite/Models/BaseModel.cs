using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class CommonModel:EntityBase
    {
        [JsonIgnore]
        public DateTime CreateOn
        {
            get { return GetDateTime("CreateOn"); }
            set { SetValue("CreateOn", value); }
        }
        [JsonIgnore]
        public string CreateBy
        {
            get { return GetString("CreateBy"); }
            set { SetValue("CreateBy", value); }
        }
        [JsonIgnore]
        public DateTime EditOn
        {
            get { return GetDateTime("EditOn"); }
            set { SetValue("EditOn", value); }
        }
        [JsonIgnore]
        public string EditBy
        {
            get { return GetString("EditBy"); }
            set { SetValue("EditBy", value); }
        }

        [JsonIgnore]
        public string Remark
        {
            get { return GetString("Remark"); }
            set { SetValue("Remark", value); }
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
    }

    public class SearchModel : SearchEntity
    {
        public SearchModel()
        {
            IsDelete = 1;
        }

        [Search(Operator=SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }
    }
}