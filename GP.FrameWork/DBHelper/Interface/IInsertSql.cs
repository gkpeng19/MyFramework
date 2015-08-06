using GP.FrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GP.FrameWork.DBHelper
{
    public interface IInsertSql
    {
        string GetInsertSql(EntityBase entity);
        DbParameter[] GetInsertParameters(EntityBase entity);
    }
}
