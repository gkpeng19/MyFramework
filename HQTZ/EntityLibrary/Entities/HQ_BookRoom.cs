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
    public class HQ_BookRoom : EntityBase
    {
        public HQ_BookRoom()
        {
            base.TableName = "HQ_BookRoom";
        }

        [JsonIgnore]
        public int MemberID
        {
            get { return GetInt32("MemberID"); }
            set { SetValue("MemberID", value); }
        }
        [JsonIgnore]
        public int RoomID
        {
            get { return GetInt32("RoomID"); }
            set { SetValue("RoomID", value); }
        }
        [JsonIgnore]
        public DateTime BookStartTime
        {
            get { return GetDateTime("BookStartTime"); }
            set { SetValue("BookStartTime", value); }
        }
        [JsonIgnore]
        public DateTime BookEndTime
        {
            get { return GetDateTime("BookEndTime"); }
            set { SetValue("BookEndTime", value); }
        }
        [JsonIgnore]
        public DateTime CreateOn
        {
            get { return GetDateTime("CreateOn"); }
            set { SetValue("CreateOn", value); }
        }
    }
}
