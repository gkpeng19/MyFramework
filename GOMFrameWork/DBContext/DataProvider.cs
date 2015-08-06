using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using GP.FrameWork.Extension;

namespace GP.FrameWork.DBHelper
{
    public class DataProvider : IDBOperator
    {
        internal event Action<DataProvider> OnUsed;

        DBProvider _db = null;
        internal DataProvider()
        {
            if (GPContext.Current.DbType == DbType.SqlServer)
            {
                _db = new SqlProvider();
            }
            else
            {
                _db = new OracleProvider();
            }
        }

        #region IDBOperator接口成员

        public CommonResult ExcuteInsert(EntityBase entity)
        {
            CommonResult result = new CommonResult();
            try
            {
                result.ResultID = _db.ExcuteInsert(entity.ToInsertSql(), entity.InitInsertParameterCollection());
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            entity.ID = result.ResultID;
            return result;
        }

        public void ExcuteUpdate(EntityBase entity)
        {
            try
            {
                _db.ExecuteNonQuery(entity.ToUpdateSql(), entity.InitUpdateParameterCollection());
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }
        }

        public void ExcuteDelete(EntityBase entity)
        {
            try
            {
                _db.ExecuteNonQuery(entity.ToDeleteSql(), entity.InitDeleteParameterCollection());
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }
        }

        public CommonResult<T> ExcuteSearch<T>(SearchEntity entity) where T : new()
        {
            CommonResult<T> result = new CommonResult<T>();
            try
            {
                DbParameter[] parameters = entity.InitSearchParameterCollection();
                List<T> data = _db.ExcuteSearch<T>(entity.ToSearchSql(), parameters);

                if (entity.PageIndex > 0)
                {
                    DbParameter op = parameters[parameters.Length - 1];
                    if (op.Direction == ParameterDirection.Output)
                    {
                        result.PageIndex = entity.PageIndex;
                        result.PageCount = Convert.ToInt32(Math.Ceiling(int.Parse(op.Value.ToString()) / double.Parse(entity.PageSize.ToString())));
                    }
                }
                result.Data = data;
                result.ResultID = 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            return result;
        }

        public CommonResult ExcuteProcCommandResult(ProcEntity entity)
        {
            CommonResult result = new CommonResult();
            try
            {
                using (var ds = _db.ExcuteProc(entity.ProcName, entity.InitSearchParameterCollection()))
                {
                    if (ds.Tables.Count <= 0)
                    {
                        result.ResultID = 1;
                    }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        result.ResultID = long.Parse(dt.Rows[0][0].ToString());
                        result.Message = dt.Rows[0][1].ToString();
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }
            return result;
        }

        public CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity) where T : new()
        {
            CommonResult<T> result = new CommonResult<T>() { ResultID = 1 };
            try
            {
                using (DataSet ds = _db.ExcuteProc(entity.ProcName, entity.InitSearchParameterCollection()))
                {
                    if (ds.Tables.Count > 0)
                    {
                        result.Data = DataConverter.ConvertToList<T>(ds.Tables[0]);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            return result;
        }

        public CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity) where T : new()
        {
            T obj = new T();
            CommonData<T> result = new CommonData<T>() { ResultID = 1, Data = obj };

            try
            {
                using (DataSet ds = _db.ExcuteProc(entity.ProcName, entity.InitSearchParameterCollection()))
                {
                    if (ds.Tables.Count > 1)
                    {
                        DataTable propertytable = ds.Tables[ds.Tables.Count - 1];
                        for (int i = 0; i < propertytable.Columns.Count; ++i)
                        {
                            PropertyInfo pi = typeof(T).GetPropertyCache(propertytable.Rows[0][i].ToString());
                            if (pi != null)
                            {
                                pi.SetValue(obj, DataConverter.ConvertToObject(ds.Tables[i], pi.PropertyType), null);
                            }
                        }
                    }
                    else
                    {
                        result.ResultID = 0;
                        result.Message = "存储过程错误，未在最后指定要查询数据的属性列表！";
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_db.IsInTransaction && OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            return result;
        }

        public void ExcuteTranscation(TranscationAction actions)
        {
            if (actions != null)
            {
                try
                {
                    if (_db.BeginTran())
                    {
                        actions();
                        _db.CommitTran();
                    }
                }
                catch
                {
                    _db.RollBackTran();
                    throw;
                }
                finally
                {
                    if (OnUsed != null)
                    {
                        OnUsed(this);
                    }
                }
            }
        }

        #endregion
    }
}
