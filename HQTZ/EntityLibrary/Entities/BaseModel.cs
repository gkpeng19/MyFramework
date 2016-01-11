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
    public class CommonModel : EntityBase
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

    [ModelBinder(typeof(EntityModelBinder))]
    public class SearchModel : SearchEntity
    {
        public SearchModel()
        {
            IsDelete = 0;
        }

        public SearchModel(string searchId)
            : this()
        {
            base.SearchID = searchId;
        }

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }

        #region 按展板名称查询展板明细

        [Search(Field = "DPanel_G", Operator = SearchOperator.Like)]
        public string DPanelName
        {
            set { SetValue("DPanelName", value); }
        }

        #endregion

        #region 度假村房间查询

        #region 按房间名称查询度假村房间

        [Search(Field = "Name", Operator = SearchOperator.Like)]
        public string RoomName
        {
            set { SetValue("RoomName", value); }
        }

        #endregion

        #region 按度假村名称查询度假村的房间

        [Search(Field = "Village_G", Operator = SearchOperator.Like)]
        public string InVillage
        {
            set { SetValue("InVillage", value); }
        }

        #endregion

        #endregion

        #region 会员登录

        [Search(Field = "UserName", Operator = SearchOperator.Equal)]
        public string MemberName
        {
            set { SetValue("MemberName", value); }
        }

        #endregion

        #region 文章按标题查询

        [Search(Field = "Title", Operator = SearchOperator.Like)]
        public string ArticleName
        {
            set { SetValue("ArticleName", value); }
        }

        #endregion

        #region 查询代理人

        [Search(Field = "Name")]
        public string AgenterName
        {
            set { SetValue("AgenterName", value); }
        }

        #endregion

        #region 会员订单查询

        [Search(Field = "MemberName_G", Operator = SearchOperator.Like)]
        public string MOrder_MemberName
        {
            set { SetValue("MOrder_MemberName", value); }
        }

        [Search(Operator = SearchOperator.GreaterEqual)]
        public DateTime BookStartTime
        {
            set { SetValue("BookStartTime", value); }
        }

        [Search(Operator = SearchOperator.LessEqual)]
        public DateTime BookEndTime
        {
            set { SetValue("BookEndTime", value); }
        }

        #endregion

        #region 是否有新消息回复查询


        [Search(Field = "AnswerID_G", Operator = SearchOperator.IsNotNull)]
        public int NewAnswerID
        {
            set { SetValue("NewAnswerID", value); }
        }
        [Search(Field = "IsView_G", Operator = SearchOperator.IsNull)]
        public int AnswerViewed
        {
            set { SetValue("AnswerViewed", value); }
        }

        #endregion

        #region 订单提醒，入住时间大于等于现在时间

        [Search(Field = "BookStartTime", Operator = SearchOperator.GreaterEqual)]
        public DateTime BeginInDate
        {
            set { SetValue("BeginInDate", value); }
        }

        #endregion
    }

    public class ShopSearchEntity : SearchEntity
    {
        public ShopSearchEntity() { }
        public ShopSearchEntity(string searchId)
            : base(searchId)
        {
        }
    }
}