using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Text;
using GP.FrameWork.Extension;

namespace GP.FrameWork.DBHelper
{
    public abstract class EntityProvider : IInsertSql, IUpdateSql, IDeleteSql, ISearchSql, IExecProc
    {
        protected DBProvider _DBProvider;

        internal event Action<EntityProvider> OnUsed;

        public virtual CommonResult ExcuteInsert(EntityBase entity)
        {
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
                if (OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            entity.ID = result.ResultID;
            return result;
        }

        public virtual void ExcuteUpdate(EntityBase entity)
        {
            try
            {
                _DBProvider.ExecuteNonQuery(GetUpdateSql(entity),GetUpdateParameters(entity));
            }
            catch
            {
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

        public virtual void ExcuteDelete(EntityBase entity)
        {
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
                if (OnUsed != null)
                {
                    OnUsed(this);
                }
            }
        }

        public virtual CommonResult<T> ExcuteSearch<T>(SearchEntity entity) where T : new()
        {
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
                if (OnUsed != null)
                {
                    OnUsed(this);
                }
            }

            return result;
        }

        public virtual CommonResult ExcuteProcCommandResult(ProcEntity entity)
        {

        }

        public abstract CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity) where T : new();

        public abstract CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity) where T : new();


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
        public SqlEntityProvider(string key)
        {
            _DBProvider = new SqlProvider(GPContext.Current.ConnectionConfig.Collection[key].GetConnectionString());
        }

        public override CommonResult ExcuteInsert(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void ExcuteUpdate(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void ExcuteDelete(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult<T> ExcuteSearch<T>(SearchEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult ExcuteProcCommandResult(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        #region 接口成员

        public override string GetInsertSql(EntityBase entity)
        {
            var sbk = StringBuilderProvider.Current;
            var sbv = StringBuilderProvider.Current;
            foreach (string key in entity.Collection.Keys)
            {
                sbk.Append(key + ",");
                sbv.Append("@" + key + ",");
            }
            string strk = sbk.ToString();
            string strv = sbv.ToString();
            sbk.Dispose();
            sbv.Dispose();
            return string.Format("insert into {0}({1}) values({2});select @@identity;", entity.TableInfo.TableName, strk.Substring(0, strk.Length - 1),
                strv.Substring(0, strv.Length - 1));
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
            var sb = StringBuilderProvider.Current;
            foreach (string key in entity.Collection.Keys)
            {
                sb.Append(key + "=@" + key + ",");
            }
            string str = sb.ToString();
            sb.Dispose();
            return string.Format("update {0} set {1} where " + entity.PrimaryKey + "=@id", entity.TableInfo.TableName, str.Substring(0, str.Length - 1));
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
            return string.Format("delete from {0} where " + entity.PrimaryKey + "=@id", entity.TableInfo.TableName);
        }

        public override DbParameter[] GetDeleteParameters(EntityBase entity)
        {
            return new SqlParameter[] { new SqlParameter("ID", entity.ID) };
        }

        public override string GetSearchSql(SearchEntity entity)
        {
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

            var sbwhere = StringBuilderProvider.Current;
            foreach (SearchItem item in entity.Collection)
            {
                sbwhere.Append(" and " + item.ToString("@"));
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
                if (order.Length == 0)
                {
                    throw new Exception("分页查询必须指定orderby部分！");
                }
                sql = string.Format("select * from (select {0},row_number() over({1}) row from {2} where 1=1 {3})r where r.row>{4} and r.row<={5};select @outcount=count(1) from {2} where 1=1 {3}",
                    search, order, entity.SearchID, sbwhere.ToString(), (entity.PageIndex - 1) * entity.PageSize, entity.PageIndex * entity.PageSize);
            }
            sbwhere.Dispose();

            return sql;
        }

        public override DbParameter[] GetSearchParameters(SearchEntity entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (SearchItem item in entity.Collection)
            {
                if (item.Operator != SearchOperator.Like && item.Operator != SearchOperator.IsNull && item.Operator != SearchOperator.IsNullOrZero && item.Operator != SearchOperator.IsNotNull && item.Operator != SearchOperator.In)
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
        public OracleEntityProvider(string key)
        {
            _DBProvider = new OracleProvider(GPContext.Current.ConnectionConfig.Collection[key].GetConnectionString());
        }

        public override CommonResult ExcuteInsert(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void ExcuteUpdate(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void ExcuteDelete(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult<T> ExcuteSearch<T>(SearchEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult ExcuteProcCommandResult(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonResult<T> ExcuteProcCommandResult<T>(ProcEntity entity)
        {
            throw new NotImplementedException();
        }

        public override CommonData<T> ExcuteProcCommandData<T>(ProcEntity entity)
        {
            throw new NotImplementedException();
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
            var sb = StringBuilderProvider.Current;
            foreach (string key in entity.Collection.Keys)
            {
                sb.Append(key + "=:" + key + ",");
            }
            string str = sb.ToString();
            sb.Dispose();
            return string.Format("update {0} set {1} where " + entity.PrimaryKey + "=@id", entity.TableInfo.TableName, str.Substring(0, str.Length - 1));
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

            var sbwhere = StringBuilderProvider.Current;
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
                if (order.Length == 0)
                {
                    throw new Exception("分页查询必须指定orderby部分！");
                }
                sql = string.Format("select * from (select {0},row_number() over({1}) row from {2} where 1=1 {3})r where r.row>{4} and r.row<={5};select :outcount=count(1) from {2} where 1=1 {3}",
                    search, order, entity.SearchID, sbwhere.ToString(), (entity.PageIndex - 1) * entity.PageSize, entity.PageIndex * entity.PageSize);
            }
            sbwhere.Dispose();

            return sql;
        }

        public override DbParameter[] GetSearchParameters(SearchEntity entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (SearchItem item in entity.Collection)
            {
                if (item.Operator != SearchOperator.Like && item.Operator != SearchOperator.IsNull && item.Operator != SearchOperator.IsNullOrZero && item.Operator != SearchOperator.IsNotNull && item.Operator != SearchOperator.In)
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
