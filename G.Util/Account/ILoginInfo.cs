using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace G.Util.Account
{
    internal interface ILoginInfo : IIdentity
    {
        long UserID { get; set; }

        int UserType { get; set; }

        int UserRole { get; set; }

        object this[string key] { get; set; }
    }
}
