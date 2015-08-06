using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using GP.FrameWork.Utils;
using System.Transactions;
using GP.FrameWork.DataEntity;

namespace GP.FrameWork.DBHelper
{
    internal delegate void OnProviderUsed(string key, EntityProvider provider);
    public abstract class EntityProvider : IInsertSql, IUpdateSql, IDeleteSql, ISearchSql, IExecProc
    {
        //标识是否存在与队列中（false:存在队列中，未被取出使用；true:已被取出，可以使用）
        protected bool _isEnabled = false;
        private bool _isInTran = false;
        protected string _key = string.Empty;
        protected DBProvider _DBProvider;

        internal event OnProviderUsed OnUsed;

        internal void BeginExecute()
        {
            _isEnabled = true;
        }

        internal void EndExecute()
        {
            _isEnabled = false;
        }

        public void BeginTransaction()
        {
            var currTran = Transaction.Current;
            if (currTran != null)
            {
                _isInTran = true;
                currTran.TransactionCompleted += (ss, ee) =>
                {
                    _isInTran = false;
                    OnUsed(_key, this);
                };
            }
        }

        public virtual CommonResult ExcuteInsert(EntityBase entity)
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            entity.Remove_G();

            CommonResult result = new CommonResult();
            try
            {
                result.ResultID = _DBProvider.ExcuteInsert(GetInsertSql(entity), GetInsertParameters(entity));
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }

            entity.ID = result.ResultID;
            return result;
        }

        public virtual void ExcuteUpdate(EntityBase entity)
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            entity.Remove_G();

            try
            {
                _DBProvider.ExecuteNonQuery(GetUpdateSql(entity), GetUpdateParameters(entity));
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }
        }

        public virtual void ExcuteDelete(EntityBase entity)
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            try
            {
                _DBProvider.ExecuteNonQuery(GetDeleteSql(entity), GetDeleteParameters(entity));
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }
        }

        public virtual CommonResult<T> ExcuteSearch<T>(SearchEntity entity) where T : new()
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            CommonResult<T> result = new CommonResult<T>();
            try
            {
                DbParameter[] parameters = GetSearchParameters(entity);
                List<T> data = _DBProvider.ExcuteSearch<T>(GetSearchSql(entity), parameters);

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
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }

            return result;
        }

        public virtual CommonResult ExcuteProcCommandResult(ProcEntity entity)
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            CommonResult result = new CommonResult();
            try
            {
                using (var ds = _DBProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
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
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }
            return result;
        }

        public virtual CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity) where T : new()
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            CommonResult<T> result = new CommonResult<T>() { ResultID = 1 };
            try
            {
                using (DataSet ds = _DBProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
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
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }

            return result;
        }

        public virtual CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity) where T : new()
        {
            if (!_isEnabled)
            {
                throw new Exception("Please make sure to use Method 'BeginTransaction' in TransactionScope!");
            }

            T obj = new T();
            CommonData<T> result = new CommonData<T>() { ResultID = 1, Data = obj };

            try
            {
                using (DataSet ds = _DBProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
                {
                    if (ds.Tables.Count > 1)
                    {
                        DataTable propertytable = ds.Tables[ds.Tables.Count - 1];
                        for (int i = 0; i < propertytable.Columns.Count; ++i)
                        {
                            PropertyInfo pi = typeof(T).GetProperty(propertytable.Rows[0][i].ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
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
                if (!_isInTran)
                {
                    OnUsed(_key, this);
                }
            }

            return result;
        }

        #region 接口成员

        public virtual string GetInsertSql(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbParameter[] GetInsertParameters(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual string GetUpdateSql(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbParameter[] GetUpdateParameters(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual string GetDeleteSql(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbParameter[] GetDeleteParameters(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public virtual string GetSearchSql(SearchEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbParameter[] GetSearchParameters(SearchEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbParameter[] GetProcParameters(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SqlEntityProvider : EntityProvider
    {
        public SqlEntityProvider(string key, string connStr)
        {
            _key = key;
            _DBProvider = new SqlProvider(connStr);
            _isEnabled = true;
        }

        #region 接口成员

        public override string GetInsertSql(EntityBase entity)
        {
            string sql = string.Empty;
            using (var sbk = new ExStringBuilder())
            {
                sbk.Append("insert into " + entity.TableInfo.TableName + "(");
                using (var sbv = new ExStringBuilder())
                {
                    int i = 0;
                    foreach (string key in entity.Collection.Keys)
                    {
                        ++i;
                        if (i == entity.Collection.Count)
                        {
                            sbk.Append(key);
                            sbv.Append("@" + key);
                        }
                        else
                        {
                            sbk.Append(key + ",");
                            sbv.Append("@" + key + ",");
                        }
                    }
                    sbk.Append(") values(");
                    sbk.Append(sbv.ToString());
                    sbk.Append(");select @@identity;");
                }
                sql = sbk.ToString();
            }
            return sql;
        }

        public override DbParameter[] GetInsertParameters(EntityBase entity)
        {
            DbParameter[] parameters = new SqlParameter[entity.Collection.Count];
            int index = 0;
            foreach (EntityItem item in entity.Collection.Values)
            {
                parameters[index] = new SqlParameter(item.Key, item.Value);
                ++index;
            }

            return parameters;
        }

        public override string GetUpdateSql(EntityBase entity)
        {
            using (var sb = new ExStringBuilder())
            {
                sb.Append("update " + entity.TableInfo.TableName + " set ");

                int i = 0;
                foreach (string key in entity.Collection.Keys)
                {
                    ++i;
                    if (i == entity.Collection.Count)
                    {
                        sb.Append(key + "=@" + key);
                    }
                    else
                    {
                        sb.Append(key + "=@" + key + ",");
                    }
                }

                sb.Append(" where " + entity.PrimaryKey + "=@id");
                return sb.ToString();
            }
        }

        public override DbParameter[] GetUpdateParameters(EntityBase entity)
        {
            DbParameter[] parameters = new SqlParameter[entity.Collection.Count + 1];
            int index = 0;
            foreach (EntityItem item in entity.Collection.Values)
            {
                parameters[index] = new SqlParameter(item.Key, item.Value);
                ++index;
            }
            parameters[index] = new SqlParameter("ID", entity.ID);

            return parameters;
        }

        public override string GetDeleteSql(EntityBase entity)
        {
            return "delete from " + entity.TableInfo.TableName + " where " + entity.PrimaryKey + "=@id";
        }

        public override DbParameter[] GetDeleteParameters(EntityBase entity)
        {
            return new SqlParameter[] { new SqlParameter("ID", entity.ID) };
        }

        public override string GetSearchSql(SearchEntity entity)
        {
            string order = string.Empty;
            if (entity.Orders != null && entity.Orders.Count > 0)
            {
                order = " order by ";
                var length = entity.Orders.Count;
                for (var i = 0; i < length; ++i)
                {
                    if (i + 1 == length)
                    {
                        order += (entity.Orders[i][0] + " " + entity.Orders[i][1]);
                    }
                    else
                    {
                        order += (entity.Orders[i][0] + " " + entity.Orders[i][1] + ",");
                    }
                }
            }

            if (entity.PageIndex > 0 && order.Length == 0)
            {
                throw new Exception("分页查询必须指定orderby部分！");
            }

            using (var sbsql = new ExStringBuilder())
            {
                if (entity.PageIndex <= 0)
                {
                    sbsql.Append("select ");
                }
                else
                {
                    sbsql.Append("select * from (select ");
                }

                #region 初始化Search

                if (entity.SearchItems != null && entity.SearchItems.Count > 0)
                {
                    var length = entity.SearchItems.Count;
                    for (int i = 0; i < length; ++i)
                    {
                        if (i + 1 == length)
                        {
                            sbsql.Append(entity.SearchItems[i]);
                        }
                        else
                        {
                            sbsql.Append(entity.SearchItems[i] + ",");
                        }
                    }
                }
                else
                {
                    sbsql.Append("*");
                }

                #endregion

                if (entity.PageIndex <= 0)
                {
                    sbsql.Append(" from " + entity.SearchID + " where 1=1");
                }
                else
                {
                    sbsql.Append(",row_number() over(" + order + ") row from " + entity.SearchID + " where 1=1");
                }

                #region 初始化Where

                string where = string.Empty;
                using (var sbwhere = new ExStringBuilder())
                {
                    foreach (SearchItem item in entity.Collection)
                    {
                        sbwhere.Append(" and " + item.ToString("@"));
                    }

                    if (entity.ExtensionCondition != null && entity.ExtensionCondition.Length > 0)
                    {
                        sbwhere.Append(" and " + entity.ExtensionCondition);
                    }

                    where = sbwhere.ToString();
                }

                sbsql.Append(where);

                #endregion

                if (entity.PageIndex <= 0)
                {
                    sbsql.Append(order);
                }
                else
                {
                    sbsql.Append(")r where r.row>" + (entity.PageIndex - 1) * entity.PageSize + " and r.row<=" + entity.PageIndex * entity.PageSize);
                    sbsql.Append(";select @outcount=count(1) from " + entity.SearchID + " where 1=1");
                    sbsql.Append(where);
                }

                return sbsql.ToString();
            }
        }

        public override DbParameter[] GetSearchParameters(SearchEntity entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (SearchItem item in entity.Collection)
            {
                if (item.Operator != SearchOperator.Like && item.Operator != SearchOperator.IsNull && item.Operator != SearchOperator.IsNullZeroEqual && item.Operator != SearchOperator.IsNotNull && item.Operator != SearchOperator.In)
                {
                    parameters.Add(new SqlParameter(item.Key, item.Value));
                }
            }

            if (entity.PageIndex > 0)
            {
                parameters.Add(new SqlParameter("outcount", 0) { Direction = ParameterDirection.Output });
            }

            return parameters.ToArray();
        }

        public override DbParameter[] GetProcParameters(ProcEntity entity)
        {
            DbParameter[] parameters = new SqlParameter[entity.Collection.Count];
            int index = 0;
            foreach (EntityItem item in entity.Collection)
            {
                parameters[index] = new SqlParameter(item.Key, item.Value);
                ++index;
            }

            return parameters;
        }

        #endregion
    }

    public class OracleEntityProvider : EntityProvider
    {
        public OracleEntityProvider(string key, string connStr)
        {
            _key = key;
            _DBProvider = new OracleProvider(connStr);
            _isEnabled = true;
        }

        #region 接口成员

        public override string GetInsertSql(EntityBase entity)
        {
            return base.GetInsertSql(entity);
        }

        public override DbParameter[] GetInsertParameters(EntityBase entity)
        {
            DbParameter[] parameters = new OracleParameter[entity.Collection.Count];
            int index = 0;
            foreach (EntityItem item in entity.Collection.Values)
            {
                parameters[index] = new OracleParameter(item.Key, item.Value);
                ++index;
            }

            return parameters;
        }

        public override string GetUpdateSql(EntityBase entity)
        {
            using (var sb = new ExStringBuilder())
            {
                foreach (string key in entity.Collection.Keys)
                {
                    sb.Append(key + "=:" + key + ",");
                }
                string str = sb.ToString();
                return string.Format("update {0} set {1} where " + entity.PrimaryKey + "=@id", entity.TableInfo.TableName, str.Substring(0, str.Length - 1));
            }
        }

        public override DbParameter[] GetUpdateParameters(EntityBase entity)
        {
            DbParameter[] parameters = new OracleParameter[entity.Collection.Count + 1];
            int index = 0;
            foreach (EntityItem item in entity.Collection.Values)
            {
                parameters[index] = new OracleParameter(item.Key, item.Value);
                ++index;
            }
            parameters[index] = new OracleParameter("ID", entity.ID);

            return parameters;
        }

        public override string GetDeleteSql(EntityBase entity)
        {
            return string.Format("delete from {0} where " + entity.PrimaryKey + "=:id", entity.TableInfo.TableName);
        }

        public override DbParameter[] GetDeleteParameters(EntityBase entity)
        {
            return new OracleParameter[] { new OracleParameter("ID", entity.ID) };
        }

        public override string GetSearchSql(SearchEntity entity)
        {
            string order = string.Empty;
            if (entity.Orders != null && entity.Orders.Count > 0)
            {
                order = "order by ";
                foreach (var o in entity.Orders)
                {
                    order += string.Format("{0} {1},", o[0], o[1]);
                }
                order = order.Substring(0, order.Length - 1);
            }
            if (entity.PageIndex > 0 && order.Length == 0)
            {
                throw new Exception("分页查询必须指定orderby部分！");
            }

            string search = string.Empty;
            if (entity.SearchItems != null && entity.SearchItems.Count > 0)
            {
                foreach (var str in entity.SearchItems)
                {
                    search += (str + ",");
                }
                search = search.Substring(0, search.Length - 1);
            }
            else
            {
                search = "*";
            }

            using (var sbwhere = new ExStringBuilder())
            {
                foreach (SearchItem item in entity.Collection)
                {
                    sbwhere.Append(" and " + item.ToString(":"));
                }

                if (entity.ExtensionCondition != null && entity.ExtensionCondition.Length > 0)
                {
                    sbwhere.Append(" and " + entity.ExtensionCondition);
                }

                string sql = string.Empty;
                if (entity.PageIndex <= 0)
                {
                    sql = string.Format("select {0} from {1} where 1=1 {2} {3}", search, entity.SearchID, sbwhere.ToString(), order);
                }
                else
                {
                    sql = string.Format("select * from (select {0},row_number() over({1}) row from {2} where 1=1 {3})r where r.row>{4} and r.row<={5};select :outcount=count(1) from {2} where 1=1 {3}",
                        search, order, entity.SearchID, sbwhere.ToString(), (entity.PageIndex - 1) * entity.PageSize, entity.PageIndex * entity.PageSize);
                }

                return sql;
            }
        }

        public override DbParameter[] GetSearchParameters(SearchEntity entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (SearchItem item in entity.Collection)
            {
                if (item.Operator != SearchOperator.Like && item.Operator != SearchOperator.IsNull && item.Operator != SearchOperator.IsNullZeroEqual && item.Operator != SearchOperator.IsNotNull && item.Operator != SearchOperator.In)
                {
                    parameters.Add(new OracleParameter(item.Key, item.Value));
                }
            }

            if (entity.PageIndex > 0)
            {
                parameters.Add(new OracleParameter("outcount", 0) { Direction = ParameterDirection.Output });
            }

            return parameters.ToArray();
        }

        public override DbParameter[] GetProcParameters(ProcEntity entity)
        {
            DbParameter[] parameters = new OracleParameter[entity.Collection.Count];
            int index = 0;
            foreach (EntityItem item in entity.Collection)
            {
                parameters[index] = new OracleParameter(item.Key, item.Value);
                ++index;
            }

            return parameters;
        }

        #endregion
    }
}
