using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G.BaseWeb.Models
{
    public class BaseModel : EntityBase
    {
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class BaseSearchModel : SearchEntity
    {
        public BaseSearchModel()
        {
            IsDelete = 0;
        }
        public BaseSearchModel(string searchID)
            : base(searchID)
        {
            IsDelete = 0;
        }

        #region 角色查询

        [Search(Operator = SearchOperator.Like)]
        public string RoleName
        {
            set { SetValue("RoleName", value); }
        }

        #endregion

        #region 用户查询

        /// <summary>
        /// 按角色查询用户
        /// </summary>
        [Search(Field = "UserRole", Operator = SearchOperator.In)]
        public string nodeids
        {
            set { SetValue("nodeids", value); }
        }

        [Search(Operator=SearchOperator.Like)]
        public string UserName
        {
            set { SetValue("UserName", value); }
        }

        #endregion

        #region 公共查询

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }

        #endregion
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class BaseProcModel : ProcEntity
    {
        public BaseProcModel() { }
        public BaseProcModel(string procName) : base(procName) { }
    }

    public class BaseCommonModel : BaseModel
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
}