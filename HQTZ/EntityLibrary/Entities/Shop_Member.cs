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
}
