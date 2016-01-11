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
            _httpClient.BaseAddress = new Uri("http://123.57.153.47:8099/");

            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var l in list)
                    {
                        SearchModel se = new SearchModel("HQ_Member");
                        se["UserName"] = l.UserName;
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
                                ShopSearchEntity sse = new ShopSearchEntity("Accounts_Users");
                                sse["UserName"] = member.PhoneNum;
                                sse.AddSearch("UserId");
                                var smem = sse.LoadEntity<Shop_Member>();
                                if (smem != null)
                                {
                                    smem["UserName"] = l.PhoneNum;
                                    smem.Save();
                                }
                            }

                            var mem = new HQ_Member();
                            mem["id"] = mem.ID;
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
            if (entity.ID == 0)
            {
                #region 维护新增数据

                entity.UserPsw = MD5.EncryptString("123456");
                entity.CreateBy = LoginInfo.Current.UserName;
                entity.CreateOn = DateTime.Now;

                #endregion
            }
            else
            {
                #region 维护修改数据

                entity.EditBy = LoginInfo.Current.UserName;
                entity.EditOn = DateTime.Now;

                #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }

        public int AddBlance(int shopUserId, decimal amount)
        {
            if (amount <= 0)
            {
                return 0;
            }
            ShopSearchEntity sse = new ShopSearchEntity("Accounts_UsersExp");
            sse["UserID"] = shopUserId;
            sse.AddSearch("Balance");
            var uexp = sse.LoadEntity<Shop_Accounts_UsersExp>();
            uexp["UserID"] = shopUserId;
            uexp.Balance = uexp.Balance + amount;
            uexp.Save();
            return 1;
        }
    }
}