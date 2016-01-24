using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Entities
{
    public class Shop_Member : EntityBase
    {
        public Shop_Member()
        {
            base.TableName = "DrShop.dbo.Accounts_Users";
            base.PrimaryKey = "UserID";
        }
    }

    public class Shop_Accounts_UsersExp : EntityBase
    {
        public Shop_Accounts_UsersExp()
        {
            base.TableName = "DrShop.dbo.Accounts_UsersExp";
            base.PrimaryKey = "UserID";
        }

        public decimal Balance
        {
            get { return GetDecimal("Balance"); }
            set { SetValue("Balance", value); }
        }
    }

    public class Shop_Pay_BalanceDetails : EntityBase
    {
        public Shop_Pay_BalanceDetails()
        {
            base.TableName = "DrShop.dbo.Pay_BalanceDetails";
            base.PrimaryKey = "JournalNumber";
        }
        public int UserId
        {
            get { return GetInt32("UserId"); }
            set { SetValue("UserId", value); }
        }

        public DateTime TradeDate
        {
            get { return GetDateTime("TradeDate"); }
            set { SetValue("TradeDate", value); }
        }
        public int TradeType
        {
            get { return GetInt32("TradeType"); }
            set { SetValue("TradeType", value); }
        }
        public decimal Income
        {
            get { return GetDecimal("Income"); }
            set { SetValue("Income", value); }
        }
        public decimal Expenses
        {
            get { return GetDecimal("Expenses"); }
            set { SetValue("Expenses", value); }
        }
        public decimal Balance
        {
            get { return GetDecimal("Balance"); }
            set { SetValue("Balance", value); }
        }
        public string Remark
        {
            get { return GetString("Remark"); }
            set { SetValue("Remark", value); }
        }
    }
}
