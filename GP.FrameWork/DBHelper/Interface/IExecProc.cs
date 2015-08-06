using GP.FrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace GP.FrameWork.DBHelper
{
    public interface IExecProc
    {
        DbParameter[] GetProcParameters(ProcEntity entity);
    }
}
