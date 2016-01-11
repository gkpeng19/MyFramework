using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary.Entities
{
    public class Shop_Member : ShopEntityBase
    {
        public Shop_Member()
        {
            base.TableName = "Accounts_Users";
            base.PrimaryKey = "UserID";
        }
    }

    public class Shop_Accounts_UsersExp : ShopEntityBase
    {
        public Shop_Accounts_UsersExp()
        {
            base.TableName = "Accounts_UsersExp";
            base.PrimaryKey = "UserID";
        }

        public decimal Balance
        {
            get { return GetDecimal("Balance"); }
            set { SetValue("Balance", value); }
        }
    }
}
