using G.Util.Extension;
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

    public enum UserLevelEnum : int
    {
        [EnumDescription(Description = "初学一级")]
        Primary = 1,
        [EnumDescription(Description = "菜鸟一级")]
        Medium = 2,
        [EnumDescription(Description = "老鸟一级")]
        High = 3
    }

    public enum UserTypeEnum : int
    {
        [EnumDescription(Description = "普通会员")]
        Normal = 1,
        [EnumDescription(Description = "Vip会员")]
        Vip = 2
    }
}
