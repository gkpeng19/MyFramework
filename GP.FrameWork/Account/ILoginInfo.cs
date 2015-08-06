using System;
using System.Collections.Generic;
using System.Text;

namespace GP.FrameWork.Account
{
    public interface ILoginInfo
    {
        long UserID { get; set; }

        string UserName { get; set; }

        int UserType { get; set; }

        int UserRole { get; set; }

        object this[string key] { get; set; }
    }
}
