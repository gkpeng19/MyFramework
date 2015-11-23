using G.Util.Extension;
using G.Util.Net.Http;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace G.Simple.Entity
{
    public enum EnumSType
    {
        [EnumDescription(Description = "配套建筑总体方案")]
        Type1 = 1,
        [EnumDescription(Description = "总体设计方案")]
        Type2 = 2,
        [EnumDescription(Description = "其他方案")]
        Type3 = 3
    }

    public enum EnumGLState
    {
        [EnumDescription(Description = "未提交")]
        Type1 = 1,
        [EnumDescription(Description = "审定中")]
        Type2 = 2,
        [EnumDescription(Description = "以审定")]
        Type3 = 3
    }

    //---------------------------------------------------------------

    public class SimpleBase : EntityBase
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
                return CreateOn.ToString("yyyy-MM-dd");
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
                return EditOn.ToString("yyyy-MM-dd");
            }
        }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class GreenLand : SimpleBase
    {
        public GreenLand()
        {
            base.TableName = "GreenLand";
        }

        [JsonIgnore]
        public string Code
        {
            get { return GetString("Code"); }
            set { SetValue("Code", value); }
        }
        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        [JsonIgnore]
        public int SType
        {
            get { return GetInt32("SType"); }
            set { SetValue("SType", value); }
        }
        [JsonIgnore]
        public decimal Area
        {
            get { return GetDecimal("Area"); }
            set { SetValue("Area", value); }
        }

        [JsonIgnore]
        public int State
        {
            get { return GetInt32("State"); }
            set { SetValue("State", value); }
        }

        [JsonIgnore]
        public string Image
        {
            get { return GetString("Image"); }
            set { SetValue("Image", value); }
        }
        [JsonIgnore]
        public string Description
        {
            get { return GetString("Description"); }
            set { SetValue("Description", value); }
        }

        public string SType_G
        {
            get { return ((EnumSType)SType).GetDescription(); }
        }

        public string State_G
        {
            get { return ((EnumGLState)State).GetDescription(); }
        }
    }

    [EntityChildren("Items", "Descs")]
    [ModelBinder(typeof(EntityModelBinder))]
    public class S_Order : SimpleBase
    {
        public S_Order()
        {
            this.TableName = "S_Order";
        }

        [FKey(FKey = "ParentID")]
        public List<S_OrderItem> Items { get; set; }

        [FKey(FKey = "ParentID")]
        public List<S_OrderDescs> Descs { get; set; }

        [JsonIgnore]
        public string Code
        {
            get { return GetString("Code"); }
            set { SetValue("Code", value); }
        }
        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class S_OrderItem : SimpleBase
    {
        public S_OrderItem()
        {
            this.TableName = "S_OrderItem";
        }

        [JsonIgnore]
        public string ItemName
        {
            get { return GetString("ItemName"); }
            set { SetValue("ItemName", value); }
        }

        [JsonIgnore]
        public decimal Price
        {
            get { return GetDecimal("Price"); }
            set { SetValue("Price", value); }
        }

        [JsonIgnore]
        public int ParentID
        {
            get { return GetInt32("ParentID"); }
            set { SetValue("ParentID", value); }
        }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class S_OrderDescs : SimpleBase
    {
        public S_OrderDescs()
        {
            this.TableName = "S_OrderDescs";
        }

        [JsonIgnore]
        public int ParentID
        {
            get { return GetInt32("ParentID"); }
            set { SetValue("ParentID", value); }
        }
    }

    //----------------------------------------------------------

    [ModelBinder(typeof(EntityModelBinder))]
    public class SimpleSearchModel : SearchEntity
    {
        public SimpleSearchModel()
        {
            IsDelete = 0;
        }
        public SimpleSearchModel(string searchId)
            : base(searchId)
        {
            IsDelete = 0;
        }

        [Search(Operator = SearchOperator.Like)]
        public string Code
        {
            set { SetValue("Code", value); }
        }

        [Search(Field = "CreateOn", Operator = SearchOperator.GreaterEqual)]
        public DateTime StartDate
        {
            set { SetValue("StartDate", value); }
        }

        [Search(Field = "CreateOn", Operator = SearchOperator.LessEqual)]
        public DateTime EndDate
        {
            set { SetValue("EndDate", value); }
        }

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }
    }
}