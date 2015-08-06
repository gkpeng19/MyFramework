using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GOMFrameWork.DBContext
{
    public interface IInsert
    {
        string GetInsertSql(EntityBase entity);
        DbParameter[] GetInsertParameters(EntityBase entity);
    }
}
