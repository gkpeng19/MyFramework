using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GOMFrameWork.DBContext
{
    public interface IUpdate
    {
        string GetUpdateSql(EntityBase entity);
        DbParameter[] GetUpdateParameters(EntityBase entity);
    }
}
