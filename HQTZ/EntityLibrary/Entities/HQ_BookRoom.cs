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
        [JsonIgnore]
        public string Remark
        {
            get { return GetString("Remark"); }
            set { SetValue("Remark", value); }
        }

        [JsonIgnore]
        public DateTime LastOperateTime
        {
            get { return GetDateTime("LastOperateTime"); }
            set { SetValue("LastOperateTime", value); }
        }

        [JsonIgnore]
        public int DaysBeforePay_G
        {
            get { return GetInt32("DaysBeforePay_G"); }
            set { SetValue("DaysBeforePay_G", value); }
        }

        public int ShowPay_G
        {
            get
            {
                if (DateTime.Now.AddDays(DaysBeforePay_G) >= BookStartTime)
                {
                    if (OStatus == 1)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// 0:待处理(是否通过预订);1:通过预定;2:未通过预定;3:已付款;4:已退订;5:已删除
        /// </summary>
        [JsonIgnore]
        public int OStatus
        {
            get { return GetInt32("OStatus"); }
            set { SetValue("OStatus", value); }
        }

        public string BookStartTime_G
        {
            get { return BookStartTime.ToString("yyyy-MM-dd"); }
        }

        public string BookEndTime_G
        {
            get { return BookEndTime.ToString("yyyy-MM-dd"); }
        }

        public string CreateOn_G
        {
            get { return CreateOn.ToString("yyyy-MM-dd"); }
        }

        public string LongCreateOn_G
        {
            get { return CreateOn.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        [JsonIgnore]
        public string RoomName_G
        {
            get { return GetString("RoomName_G"); }
            set { SetValue("RoomName_G", value); }
        }
        [JsonIgnore]
        public int RoomPrice_G
        {
            get { return Convert.ToInt32(GetDecimal("RoomPrice_G")); }
        }

        int _days = 0;
        public int BookDays_G
        {
            get
            {
                if (_days == 0)
                {
                    _days = BookEndTime.Subtract(BookStartTime).Days;
                }
                return _days;
            }
        }

        public int AllPrice_G
        {
            get { return RoomPrice_G * BookDays_G; }
        }

        public int CanCancelBook_G
        {
            get
            {
                if (OStatus == 1)
                {
                    if (DateTime.Now.AddDays(DaysBeforePay_G) < BookStartTime)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        [JsonIgnore]
        public string MemberName_G
        {
            get { return GetString("MemberName_G"); }
            set { SetValue("MemberName_G", value); }
        }
        [JsonIgnore]
        public string PhoneNum_G
        {
            get { return GetString("PhoneNum_G"); }
            set { SetValue("PhoneNum_G", value); }
        }

        [JsonIgnore]
        public string VillageName_G
        {
            get { return GetString("VillageName_G"); }
            set { SetValue("VillageName_G", value); }
        }
        [JsonIgnore]
        public string DestinationName_G
        {
            get { return GetString("DestinationName_G"); }
            set { SetValue("DestinationName_G", value); }
        }
    }
}
