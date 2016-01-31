using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using EntityLibrary.Entities;
using G.Util.Account;
using WebSite.Controllers;
using G.Util.Extension;
using System.Net.Http;
using System.Data.SqlClient;
using System.Transactions;

namespace HQWZ.Controllers
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class Search_AMember : SearchEntity
    {
        #region 按会员用户名查询会员

        [Search(Operator = SearchOperator.Like)]
        public string UserName
        {
            set { SetValue("UserName", value); }
        }

        #endregion
    }

    public class AdminMemberController : Controller
    {
        public JsonResult LoadMemberList(Search_AMember se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "uv_MemberWithAmount";
            //.........................
            se.OrderBy("isdelete", EnumOrderBy.Asc);
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<HQ_Member>();
            return ExController.JsonNet(result);
        }

        public long ImportMember(string fpath)
        {
            fpath = Server.MapPath("~/" + fpath);
            if (!System.IO.File.Exists(fpath))
            {
                return 0;
            }

            var list = G.Util.Tool.ExcelHelper.Read<HQ_Member>(fpath, new string[] {
                "UserName","PhoneNum","MemberMedical"
                }, 1, (e) =>
                {
                    e.PhoneNum = e.PhoneNum;
                    e.UserPsw = e.PhoneNum.Substring(5);
                    e.UserType = (int)EnumUserType.Normal;
                    e.CreateBy = LoginInfo.Current.UserName;
                    e.CreateOn = DateTime.Now;
                });

            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://mall.chinalvju.com/");

            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var l in list)
                    {
                        SearchModel se = new SearchModel("uv_MemberWithAmount");
                        se["UserName"] = l.UserName;
                        se.AddSearch("ID", "PhoneNum", "ShopUserID_G");
                        var member = se.LoadEntity<HQ_Member>();
                        if (member == null)
                        {
                            se = new SearchModel("HQ_Member");
                            se["PhoneNum"] = l.PhoneNum;
                            se.AddSearch("count(id)");
                            var memCount = se.LoadValue<int>();
                            if (memCount > 0)
                            {
                                continue;
                            }

                            var dic = new Dictionary<string, string>();
                            dic.Add("UserName", l.PhoneNum);
                            dic.Add("NickName", l.UserName);
                            dic.Add("Password", l.UserPsw);
                            dic.Add("ConfirmPassword", l.UserPsw);

                            _httpClient.PostAsync("Account/Register", new FormUrlEncodedContent(dic));

                            l.UserPsw = MD5.EncryptString(l.UserPsw);
                            l.ShopPsw = l.UserPsw;
                            l.Save();
                        }
                        else
                        {
                            if (!l.PhoneNum.Equals(member.PhoneNum))
                            {
                                //手机号码变化时，修改商城用户手机号码
                                Shop_Member s_mem = new Shop_Member();
                                s_mem["UserID"] = member.ShopUserID_G;
                                s_mem["UserName"] = l.PhoneNum;
                                s_mem.Save();
                            }

                            var mem = new HQ_Member();
                            mem["id"] = member.ID;
                            mem.PhoneNum = l.PhoneNum;
                            mem.MemberMedical = l.MemberMedical;
                            mem.Save();
                        }
                    }

                    scope.Complete();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public JsonResult SaveMember(HQ_Member entity)
        {
            #region 用户名、手机号码是否重复

            SearchModel sm = SearchEntity.FormSql<SearchModel>("select count(id) from hq_member where username=@uname and isnull(isdelete,0)=0 and id<>@id",
                new SqlParameter("uname", entity.UserName), new SqlParameter("id", entity.ID));
            int count = sm.LoadValue<int>();
            if (count > 0)
            {
                return ExController.JsonNet(new { ID = -1 });
            }
            sm = SearchEntity.FormSql<SearchModel>("select count(id) from hq_member where phonenum=@pnum and isnull(isdelete,0)=0 and id<>@id",
                new SqlParameter("pnum", entity.PhoneNum), new SqlParameter("id", entity.ID));
            count = sm.LoadValue<int>();
            if (count > 0)
            {
                return ExController.JsonNet(new { ID = -2 });
            }

            #endregion

            try
            {
                using (var scope = new TransactionScope())
                {
                    if (entity.ID == 0)
                    {
                        var password = "123456";

                        #region 维护新增数据

                        entity.UserPsw = MD5.EncryptString(password);
                        entity.ShopPsw = entity.UserPsw;
                        entity.CreateBy = LoginInfo.Current.UserName;
                        entity.CreateOn = DateTime.Now;
                        if (entity.UserType == 2)
                        {
                            entity.OpenVipDate = DateTime.Now.Date;
                        }

                        #endregion

                        entity.Save();

                        HttpClient _httpClient = new HttpClient();
                        _httpClient.BaseAddress = new Uri("http://mall.chinalvju.com/");
                        var dic = new Dictionary<string, string>();
                        dic.Add("UserName", entity.PhoneNum);
                        dic.Add("NickName", entity.UserName);
                        dic.Add("Password", password);
                        dic.Add("ConfirmPassword", password);

                        _httpClient.PostAsync("Account/Register", new FormUrlEncodedContent(dic));
                    }
                    else
                    {
                        #region 维护修改数据

                        entity.EditBy = LoginInfo.Current.UserName;
                        entity.EditOn = DateTime.Now;
                        if (entity.UserType == 2)
                        {
                            entity.OpenVipDate = DateTime.Now.Date;
                        }

                        #endregion

                        sm = new SearchModel("uv_MemberWithAmount");
                        sm["id"] = entity.ID;
                        var oldMem = sm.LoadEntity<HQ_Member>();
                        if (oldMem.PhoneNum != entity.PhoneNum)
                        {
                            Shop_Member s_mem = new Shop_Member();
                            s_mem["UserID"] = oldMem.ShopUserID_G;
                            s_mem["UserName"] = entity.PhoneNum;
                            s_mem.Save();
                        }

                        entity.Save();
                    }

                    scope.Complete();
                }

                return ExController.JsonNet(entity);
            }
            catch
            {
                return ExController.JsonNet(new { ID = 0 });
            }
        }

        public int UseNMember(HQ_Member entity)
        {
            try
            {
                entity.Save();
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int AddBlance(int shopUserId, decimal amount)
        {
            if (amount <= 0)
            {
                return 0;
            }

            try
            {
                ShopSearchEntity sse = new ShopSearchEntity("Accounts_UsersExp");
                sse["UserID"] = shopUserId;
                sse.AddSearch("Balance");
                var uexp = sse.LoadEntity<Shop_Accounts_UsersExp>();

                using (var scope = new TransactionScope())
                {
                    #region 更新余额

                    var balance = uexp.Balance + amount;
                    uexp["UserID"] = shopUserId;
                    uexp.Balance = balance;
                    uexp.Save();

                    #endregion

                    #region 添加充值记录

                    Shop_Pay_BalanceDetails detail = new Shop_Pay_BalanceDetails();
                    detail.UserId = shopUserId;
                    detail.TradeDate = DateTime.Now;
                    detail.TradeType = 1;
                    detail.Income = amount;
                    detail.Balance = balance;
                    detail.Remark = "线下充值";
                    detail.Save();

                    #endregion

                    scope.Complete();
                }
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        public int HelpMemBook(int memid, int roomid, DateTime sdate, DateTime edate, string remark)
        {
            try
            {
                var r = MemberController.IsRoomEnough(roomid, sdate, edate);
                if (r == 0)
                {
                    return -1;
                }
                HQ_BookRoom broom = new HQ_BookRoom();
                broom.MemberID = memid;
                broom.RoomID = roomid;
                broom.BookStartTime = sdate;
                broom.BookEndTime = edate;
                broom.CreateOn = DateTime.Now;
                broom.Remark = remark;
                broom.OStatus = 0;
                broom.LastOperateTime = DateTime.Now;
                broom.Save();
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}