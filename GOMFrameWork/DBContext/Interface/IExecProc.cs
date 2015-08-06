using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace GOMFrameWork.DBContext
{
    public interface IExecProc
    {
        DbParameter[] GetProcParameters(ProcEntity entity);
    }
}
