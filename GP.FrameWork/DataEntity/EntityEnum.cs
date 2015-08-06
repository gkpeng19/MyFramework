using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.FrameWork.DataEntity
{
    /// <summary>
    /// 实体的状态
    /// </summary>
    public enum EntityStatus : int
    {
        Normal,
        New,
        Modify,
        Delete
    }

    public enum SearchOperator : int
    {
        Equal = 0,
        Greater = 1,
        Less = 2,
        GreaterEqual = 3,
        LessEqual = 4,
        Like = 5,
        NotEqual = 6,
        IsNull = 7,
        IsNotNull = 8,
        IsNullZeroEqual = 9,
        In = 10
    }

    public enum EnumOrderBy : int
    {
        Asc = 0,
        Desc = 1
    }
}
