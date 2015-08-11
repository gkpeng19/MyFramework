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
        Context,
        Info,
        Advert
    }

    public enum CategoryTypeEnum : int
    {
        Product,
        Info,
        ImageBook
    }

    public enum MessageTypeEnum : int
    {
        AskPrice,
        AskPriceRes,
        Message,
        MessageRes
    }

    //====业务相关===============
    public enum UserRoleEnum : int
    {
        [EnumDescription(Description = "管理员")]
        Admin
    }
}
