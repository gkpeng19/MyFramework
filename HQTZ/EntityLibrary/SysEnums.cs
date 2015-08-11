using G.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public enum ContextTypeEnum : int
    {
        Context = 1,
        Info = 2,
        Advert = 3
    }

    public enum CategoryTypeEnum : int
    {
        Product = 1,
        Info = 2,
        ImageBook = 3
    }

    public enum MessageTypeEnum : int
    {
        AskPrice = 1,
        AskPriceRes = 2,
        Message = 3,
        MessageRes = 4
    }

    //====业务相关===============
    public enum UserRoleEnum : int
    {
        [EnumDescription(Description = "管理员")]
        Admin = 1
    }
}
