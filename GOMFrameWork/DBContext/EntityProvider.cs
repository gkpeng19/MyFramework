using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using GOMFrameWork.Utils;
using System.Transactions;
using GOMFrameWork.DataEntity;
using System.Collections;

namespace GOMFrameWork.DBContext
{
    internal delegate void OnProviderCompleted(string key, EntityProvider provider);
    internal abstract class EntityProvider : IInsert, IUpdate, ISearch, IExecProc
    {
        internal string ContextKey { get; set; }

        protected DBProvider _dbProvider;

        internal event OnProviderCompleted OnCompleted;

        internal virtual int ExcuteInsert(EntityBase entity)
        {
            return _dbProvider.ExecuteScalar(GetInsertSql(entity), GetInsertParameters(entity));
        }

        internal virtual void ExcuteUpdate(EntityBase entity)
        {
            _dbProvider.ExecuteNonQuery(GetUpdateSql(entity), GetUpdateParameters(entity));
        }

        internal virtual CommonResult<T> ExcuteSearch<T>(SearchEntity entity) where T : EntityBase, new()
        {
            CommonResult<T> result = new CommonResult<T>();

            DbParameter[] parameters = GetSearchParameters(entity);
            List<T> data = null;
            using (DbDataReader reader = _dbProvider.ExcuteReader(GetSearchSql(entity), parameters))
            {
                data = reader.ConvertToList<T>();
            }

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
            return result;
        }

        internal virtual T ExcuteSearchEntity<T>(SearchEntity entity) where T : EntityBase, new()
        {
            DbParameter[] parameters = GetSearchParameters(entity);
            T data = null;
            using (DbDataReader reader = _dbProvider.ExcuteReader(GetSearchSql(entity), parameters))
            {
                data = reader.ConvertToEntity<T>();
            }

            return data;
        }

        internal virtual CommonResult ExcuteProcResult(ProcEntity entity)
        {
            CommonResult result = new CommonResult();

            using (var ds = _dbProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
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

            return result;
        }

        internal virtual CommonResult<T> ExcuteProcResult<T>(ProcEntity entity) where T : EntityBase, new()
        {
            CommonResult<T> result = new CommonResult<T>();

            using (DataSet ds = _dbProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
            {
                if (ds.Tables.Count > 0)
                {
                    result.Data = DataConverter.ConvertToList<T>(ds.Tables[0]);
                    if (ds.Tables.Count > 1)
                    {
                        result.PageCount = (int)ds.Tables[1].Rows[0][0];
                    }
                }
            }

            return result;
        }

        internal virtual T ExcuteProcData<T>(ProcEntity entity) where T : new()
        {
            T obj = new T();

            using (DataSet ds = _dbProvider.ExcuteProc(entity.ProcName, GetProcParameters(entity)))
            {
                if (ds.Tables.Count > 1)
                {
                    DataTable propertytable = ds.Tables[ds.Tables.Count - 1];
                    for (int i = 0; i < propertytable.Columns.Count; ++i)
                    {
                        PropertyInfo pi = typeof(T).GetProperty(propertytable.Rows[0][i].ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                        if (pi != null)
                        {
                            if (typeof(EntityBase).IsAssignableFrom(pi.PropertyType))
                            {
                                pi.SetValue(obj, DataConverter.ConvertToEntity(ds.Tables[i], pi.PropertyType), null);
                            }
                            else if (typeof(IList).IsAssignableFrom(pi.PropertyType))
                            {
                                pi.SetValue(obj, DataConverter.ConvertToEntityList(ds.Tables[i], pi.PropertyType), null);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("存储过程错误，未在最后指定要查询数据的属性列表！");
                }
            }

            return obj;
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

        public void Close()
        {
            _dbProvider.Close();
            OnCompleted(this.ContextKey, this);
        }
    }

    internal class SqlEntityProvider : EntityProvider
    {
        public SqlEntityProvider(string key, string connStr)
        {
            this.ContextKey = key;
            _dbProvider = new SqlProvider(connStr);
        }

        #region 接口成员

        public override string GetInsertSql(EntityBase entity)
        {
            string sql = string.Empty;
            using (var sbk = new ExStringBuilder())
            {
                sbk.Append("insert into " + entity.TableName + "(");
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
                sb.Append("update " + entity.TableName + " set ");

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
                    sbsql.Append(",row_number() over(" + order + ") row_g from " + entity.SearchID + " where 1=1");
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
                    sbsql.Append(")r where r.row_g>" + (entity.PageIndex - 1) * entity.PageSize + " and r.row_g<=" + entity.PageIndex * entity.PageSize);
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

    internal class OracleEntityProvider : EntityProvider
    {
        public OracleEntityProvider(string key, string connStr)
        {
            this.ContextKey = key;
            _dbProvider = new OracleProvider(connStr);
        }

        internal override int ExcuteInsert(EntityBase entity)
        {
            var sql = GetInsertSql(entity);
            bool isSaveOk = true;
            try
            {
                _dbProvider.ExecuteNonQuery(sql, GetInsertParameters(entity));
            }
            catch
            {
                isSaveOk = false;
                throw;
            }

            if (isSaveOk)
            {
                return _dbProvider.ExecuteScalar("select seq_" + entity.TableName + ".currval from dual", null);
            }
            return 0;
        }

        internal override CommonResult<T> ExcuteSearch<T>(SearchEntity entity)
        {
            var sqls = GetSearchSql(entity).Split(';');

            CommonResult<T> result = new CommonResult<T>();
            DbParameter[] parameters = GetSearchParameters(entity);
            List<T> data = null;
            using (DbDataReader reader = _dbProvider.ExcuteReader(sqls[0], parameters))
            {
                data = reader.ConvertToList<T>();
            }

            if (entity.PageIndex > 0)
            {
                int op = _dbProvider.ExecuteScalar(sqls[1], parameters);
                result.PageIndex = entity.PageIndex;
                result.PageCount = Convert.ToInt32(Math.Ceiling(op / double.Parse(entity.PageSize.ToString())));
            }

            result.Data = data;
            return result;
        }

        #region 接口成员

        public override string GetInsertSql(EntityBase entity)
        {
            string sql = string.Empty;
            using (var sbk = new ExStringBuilder())
            {
                sbk.Append("insert into " + entity.TableName + "(" + entity.PrimaryKey);
                using (var sbv = new ExStringBuilder())
                {
                    sbv.Append("seq_" + entity.TableName + ".nextval");
                    foreach (string key in entity.Collection.Keys)
                    {
                        sbk.Append("," + key);
                        sbv.Append(",:" + key);
                    }
                    sbk.Append(") values(");
                    sbk.Append(sbv.ToString());
                    sbk.Append(")");
                }
                sql = sbk.ToString();
            }
            return sql;
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
                return string.Format("update {0} set {1} where " + entity.PrimaryKey + "=:id", entity.TableName, str.Substring(0, str.Length - 1));
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

        public override string GetSearchSql(SearchEntity entity)
        {
            string order = string.Empty;
            if (entity.Orders != null && entity.Orders.Count > 0)
            {
                order = "order by ";
                foreach (var o in entity.Orders)
                {
                    order += string.Format("s.{0} {1},", o[0], o[1]);
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
                    search += ("s." + str + ",");
                }
                search = search.Substring(0, search.Length - 1);
            }
            else
            {
                search = "s.*";
            }

            using (var sbwhere = new ExStringBuilder())
            {
                foreach (SearchItem item in entity.Collection)
                {
                    sbwhere.Append(" and s." + item.ToString(":"));
                }

                if (entity.ExtensionCondition != null && entity.ExtensionCondition.Length > 0)
                {
                    sbwhere.Append(" and s." + entity.ExtensionCondition);
                }

                string sql = string.Empty;
                if (entity.PageIndex <= 0)
                {
                    sql = string.Format("select {0} from {1} s where 1=1 {2} {3}", search, entity.SearchID, sbwhere.ToString(), order);
                }
                else
                {
                    sql = string.Format("select * from (select {0},row_number() over({1}) row_g from {2} s where 1=1 {3})r where r.row_g>{4} and r.row_g<={5};select count(1) from {2} s where 1=1 {3}",
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
