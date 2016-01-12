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
                return 1;
            }
            catch
            {
                return 0;
            }
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
                var isMoneyEnough = WebSite.Controllers.MemberController.IsMoneyEnouth(bookRoom.RoomID, bookRoom.MemberID, bookRoom.BookStartTime, bookRoom.BookEndTime, out balance, out shopUserId);

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

                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}