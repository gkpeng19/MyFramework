using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_MemberAsk : CommonModel
    {
        public HQ_MemberAsk()
        {
            base.TableName = "hq_memberask";
        }

        [JsonIgnore]
        public int MemberID
        {
            get { return GetInt32("MemberID"); }
            set { SetValue("MemberID", value); }
        }

        [JsonIgnore]
        public string AskContent
        {
            get { return GetString("AskContent"); }
            set { SetValue("AskContent", value); }
        }

        [JsonIgnore]
        public int AnswerID_G
        {
            get { return GetInt32("AnswerID_G"); }
            set { SetValue("AnswerID_G", value); }
        }

        [JsonIgnore]
        public int IsView_G
        {
            get { return GetInt32("IsView_G"); }
            set { SetValue("IsView_G", value); }
        }
        [JsonIgnore]
        public int MemberName_G
        {
            get { return GetInt32("MemberName_G"); }
            set { SetValue("MemberName_G", value); }
        }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_MemberAnswer : CommonModel
    {
        public HQ_MemberAnswer()
        {
            base.TableName = "HQ_MemberAnswer";
        }

        [JsonIgnore]
        public int AskID
        {
            get { return GetInt32("AskID"); }
            set { SetValue("AskID", value); }
        }

        [JsonIgnore]
        public string Answer
        {
            get { return GetString("Answer"); }
            set { SetValue("Answer", value); }
        }

        [UIValueZeroNotEqualNull]
        [JsonIgnore]
        public int IsView
        {
            get { return GetInt32("IsView"); }
            set { SetValue("IsView", value); }
        }
    }
}
