using GOMFrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GOMFrameWork.DataEntity
{
    public class SearchEntity : DataBase
    {
        public List<SearchItem> Collection { get; private set; }
        public List<string> SearchItems { get; private set; }
        public List<string[]> Orders { get; private set; }

        public string SearchID { get; set; }

        //public string ExtensionCondition { get; set; }

        public SearchEntity()
            : this(false)
        {
        }

        public SearchEntity(string searchID)
            : this()
        {
            this.SearchID = searchID;
        }

        private SearchEntity(bool isFromSql)
        {
            this.IsFromSql = isFromSql;
            if (!isFromSql)
            {
                Collection = new List<SearchItem>();
            }
        }

        #region 用于执行sql语句

        internal bool IsFromSql { get; private set; }
        internal DbParameter[] SqlParameters { get; private set; }
        public static SearchEntity FormSql(string sql, params DbParameter[] parameters)
        {
            SearchEntity se = new SearchEntity(true);
            se.SearchID = sql;
            se.SqlParameters = parameters;
            return se;
        }

        #endregion

        public void AddSearch(params string[] value)
        {
            if (SearchItems == null)
            {
                SearchItems = new List<string>();
            }
            SearchItems.AddRange(value);
        }

        public void OrderBy(string key, EnumOrderBy by = EnumOrderBy.Asc)
        {
            if (Orders == null)
            {
                Orders = new List<string[]>();
            }
            Orders.Add(new[] { key, by == EnumOrderBy.Desc ? "desc" : "asc" });
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        protected override EntityItem GetItem(string key)
        {
            foreach (SearchItem item in Collection)
            {
                if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        public override void SetUIValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullUIValue(key, value))
                {
                    Collection.Remove(item as SearchItem);
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                SearchAttribute sa = this.GetType().GetPropertyAttribute<SearchAttribute>(key);
                if (sa != null)
                {
                    if (sa.Operator == SearchOperator.IsNull || sa.Operator == SearchOperator.IsNotNull)
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                            Operator = sa.Operator,
                            Key = key
                        });
                    }
                    else
                    {
                        if (!IsNullUIValue(key, value))
                        {
                            Collection.Add(new SearchItem()
                            {
                                Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                                Operator = sa.Operator,
                                Key = key,
                                Value = value
                            });
                        }
                    }
                }
                else
                {
                    if (!IsNullUIValue(key, value))
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = key,
                            Operator = SearchOperator.Equal,
                            Key = key,
                            Value = value
                        });
                    }
                }
            }
        }

        protected override void SetValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullValue(key, value))
                {
                    Collection.Remove(item as SearchItem);
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                SearchAttribute sa = this.GetType().GetPropertyAttribute<SearchAttribute>(key);
                if (sa != null)
                {
                    if (sa.Operator == SearchOperator.IsNull || sa.Operator == SearchOperator.IsNotNull)
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                            Operator = sa.Operator,
                            Key = key
                        });
                    }
                    else
                    {
                        if (!IsNullValue(key, value))
                        {
                            Collection.Add(new SearchItem()
                            {
                                Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                                Operator = sa.Operator,
                                Key = key,
                                Value = value
                            });
                        }
                    }
                }
                else
                {
                    if (!IsNullValue(key, value))
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = key,
                            Operator = SearchOperator.Equal,
                            Key = key,
                            Value = value
                        });
                    }
                }
            }
        }

        #region 数据库操作

        public CommonResult<T> Load<T>() where T : EntityBase, new()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteSearch<T>(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        public T LoadEntity<T>() where T : EntityBase, new()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteSearchEntity<T>(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        public T LoadValue<T>()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteValue<T>(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        #endregion
    }

    public class ProcEntity : DataBase
    {
        public List<EntityItem> Collection { get; private set; }

        protected ProcEntity()
        {
            Collection = new List<EntityItem>();
        }

        protected ProcEntity(string procName)
        {
            ProcName = procName;
            Collection = new List<EntityItem>();
        }

        public string ProcName { get; set; }

        protected override EntityItem GetItem(string key)
        {
            foreach (EntityItem item in Collection)
            {
                if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        protected override bool IsNullUIValue(string key, object value)
        {
            if (value.ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        public override void SetUIValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullUIValue(key, value))
                {
                    item.Value = DBNull.Value;
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                if (IsNullUIValue(key, value))
                {
                    value = DBNull.Value;
                }
                Collection.Add(new EntityItem() { Key = key, Value = value });
            }
        }

        protected override void SetValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullValue(key, value))
                {
                    item.Value = DBNull.Value;
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                if (IsNullValue(key, value))
                {
                    value = DBNull.Value;
                }
                Collection.Add(new EntityItem() { Key = key, Value = value });
            }
        }

        #region 数据库操作

        public CommonResult Execute()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteProcResult(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        public CommonResult<T> Execute<T>() where T : EntityBase, new()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteProcResult<T>(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        public T ExecuteData<T>() where T : new()
        {
            var eProvider = GetProvider();
            try
            {
                return eProvider.ExcuteProcData<T>(this);
            }
            finally
            {
                eProvider.Close();
            }
        }

        #endregion
    }

    #region ForOracle

    public class OracleOutParamAttribute : Attribute
    {
        public OracleOutParamAttribute(params string[] paramName)
        {
            OutParamNames = paramName;
        }

        public string[] OutParamNames { get; private set; }
    }

    #endregion
}
