using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GOMFrameWork.DBContext
{
    public interface ISearch
    {
        string GetSearchSql(SearchEntity entity);
        DbParameter[] GetSearchParameters(SearchEntity entity);
    }
}
