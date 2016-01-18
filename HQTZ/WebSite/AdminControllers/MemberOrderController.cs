﻿using EntityLibrary.Entities;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace WebSite.AdminControllers
{
    public class MemberOrderController : Controller
    {
        public JsonResult LoadOrders(SearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "uv_memberorder";
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_BookRoom>();
            return ExController.JsonNet(result);
        }

        public int BookOk(int bid)
        {
            try
            {
                HQ_BookRoom book = new HQ_BookRoom();
                book["id"] = bid;
                book.OStatus = 1;
                book.LastOperateTime = DateTime.Now;
                book.Save();
            }
            catch
            {
                return 0;
            }

            try
            {
                SearchModel sm = new SearchModel("uv_bookroom");
                sm["id"] = bid;
                sm.AddSearch("RoomName_G", "PhoneNum_G");
                var broom = sm.LoadEntity<HQ_BookRoom>();
                if (broom != null)
                {
                    NM.Util.SendUserInfo _U = new NM.Util.SendUserInfo() { isLog = 1, orgid = 555, username = broom.PhoneNum_G };
                    NM.Util.MsgSend.DirectSend(string.Format("尊敬的会员您好，您已成功预定 {0} 。", broom.RoomName_G), broom.PhoneNum_G, _U);
                }
            }
            catch { }

            return 1;
        }

        public int BookNo(int bid, string remark)
        {
            try
            {
                HQ_BookRoom book = new HQ_BookRoom();
                book["id"] = bid;
                book.OStatus = 2;
                book.Remark = remark;
                book.LastOperateTime = DateTime.Now;
                book.Save();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int Pay(int bid)
        {
            try
            {
                SearchModel sm = new SearchModel("hq_bookroom");
                sm["ID"] = bid;
                var bookRoom = sm.LoadEntity<HQ_BookRoom>();
                if (bookRoom == null)
                {
                    return 0;
                }

                int shopUserId = 0;
                decimal balance = 0;
                string phoneNum = null;
                var isMoneyEnough = WebSite.Controllers.MemberController.IsMoneyEnouth(bookRoom.RoomID, bookRoom.MemberID, bookRoom.BookStartTime, bookRoom.BookEndTime, out balance, out shopUserId, out phoneNum);

                if (isMoneyEnough == -1)//余额不足
                {
                    return -1;
                }
                else if (isMoneyEnough == 0)//检查失败
                {
                    return 0;
                }

                using (var scope = new TransactionScope())
                {
                    var updateBRoom = new HQ_BookRoom();
                    updateBRoom["id"] = bid;
                    updateBRoom.OStatus = 3;
                    updateBRoom.LastOperateTime = DateTime.Now;
                    updateBRoom.Save();

                    Shop_Accounts_UsersExp uexp = new Shop_Accounts_UsersExp();
                    uexp["UserID"] = shopUserId;
                    uexp["Balance"] = balance;
                    uexp.Save();

                    scope.Complete();
                }

                try
                {
                    if (balance < 500)
                    {
                        NM.Util.SendUserInfo _U = new NM.Util.SendUserInfo() { isLog = 1, orgid = 555, username = phoneNum };
                        NM.Util.MsgSend.DirectSend("尊敬的会员您好，您的账户余额已低于500元，请尽快充值。", phoneNum, _U);
                    }
                }
                catch { }

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}