using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace GP.FrameWork.DBHelper
{
    public interface IDBOperator
    {
        CommonResult ExcuteInsert(EntityBase entity);

        void ExcuteUpdate(EntityBase entity);

        void ExcuteDelete(EntityBase entity);

        CommonResult<T> ExcuteSearch<T>(SearchEntity entity) where T : new();

        CommonResult ExcuteProcCommandResult(ProcEntity entity);

        CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity) where T : new();

        CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity) where T : new();

        void ExcuteTranscation(TranscationAction actions);
    }
}
