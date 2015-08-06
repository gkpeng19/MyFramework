using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using GOMFrameWork.Utils;
using GOMFrameWork.DBContext;
using System.Transactions;

namespace GOMFrameWork.DataEntity
{
    public abstract class DataBase : INotifyPropertyChanged
    {
        internal EntityProvider GetProvider()
        {
            bool isNew = true;
            return GetProvider(null, ref isNew);
        }

        internal EntityProvider GetProvider(EntityProvider provider, ref bool isNew)
        {
            return DbContext.GetEntityProvider(provider, this.GetType(), ref isNew);
        }

        protected abstract void SetValue(string key, object value);
        public abstract void SetUIValue(string key, object value);

        protected abstract EntityItem GetItem(string key);

        protected virtual bool IsNullValue(string key, object value)
        {
            if (
                value == null ||
                (value is string && value.ToString().Length == 0) ||
                (value is int && (int)value == int.MinValue) ||
                (value is DateTime && (DateTime)value == DateTime.MinValue) ||
                (value is decimal && (decimal)value == decimal.MinValue) ||
                (value is long && (long)value == long.MinValue) ||
                (value is double && (double)value == double.MinValue) ||
                (value is short && (short)value == short.MinValue) ||
                (value is long && (long)value == long.MinValue) ||
                (value is float && (float)value == float.MinValue)
                )
            {
                return true;
            }
            return false;
        }

        protected virtual bool IsNullUIValue(string key, object value)
        {
            string v = value.ToString();
            if (v.Length == 0)
            {
                return true;
            }
            else
            {
                if (v.Equals("0"))
                {
                    var attr = this.GetType().GetPropertyAttribute<UIValueZeroNotEqualNullAttribute>(key);
                    if (attr == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
                }
                catch { }
            }
        }

        public virtual object this[string key]
        {
            get
            {
                var item = GetItem(key);
                if (item != null)
                {
                    return item.Value;
                }
                return null;
            }
            set
            {
                SetValue(key, value);
            }
        }
    }

    /// <summary>
    /// 代码生成时，指定TableName、PrimaryKey、主键的生成为（get{return base.ID;} set{base.ID=value;}）
    /// </summary>
    public class EntityBase : DataBase
    {
        public string TableName { get; protected set; }

        private string _primaryKey = null;
        public string PrimaryKey
        {
            get { return _primaryKey == null ? "ID" : _primaryKey; }
            protected set { _primaryKey = value; }
        }

        private long _id = 0;
        /// <summary>
        /// 如果主键字段不是ID,则主键字段写法：public long PID{get{return base.ID;}}
        /// </summary>
        public long ID
        {
            get
            {
                if (_id > 0)
                {
                    return _id;
                }

                string pkey = this.PrimaryKey;
                if (Collection.ContainsKey(pkey))
                {
                    Int64.TryParse((Collection[pkey] as EntityItem).Value.ToString(), out this._id);
                    Collection.Remove(pkey);
                }
                return _id;
            }
            internal set { _id = value; }
        }

        public Hashtable Collection { get; private set; }

        public EntityBase()
        {
            Collection = new Hashtable(StringComparer.OrdinalIgnoreCase);
        }

        #region 取值方法

        public short GetInt16(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Int16)
                {
                    return (short)item.Value;
                }
                else
                {
                    short result = 0;
                    short.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public int GetInt32(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Int32)
                {
                    return (int)item.Value;
                }
                else
                {
                    int result = 0;
                    int.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public long GetInt64(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Int64)
                {
                    return (long)item.Value;
                }
                else
                {
                    long result = 0;
                    long.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public float GetFloat(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is float)
                {
                    return (float)item.Value;
                }
                else
                {
                    float result = 0;
                    float.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public double GetDouble(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Double)
                {
                    return (double)item.Value;
                }
                else
                {
                    double result = 0;
                    double.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public bool GetBoolean(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Boolean)
                {
                    return (bool)item.Value;
                }
                else
                {
                    bool result = false;
                    bool.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return false;
        }

        public decimal GetDecimal(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is Decimal)
                {
                    return (decimal)item.Value;
                }
                else
                {
                    decimal result = 0;
                    decimal.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return 0;
        }

        public string GetString(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                return item.Value.ToString();
            }
            return string.Empty;
        }

        public DateTime GetDateTime(string key)
        {
            EntityItem item = GetItem(key);
            if (item != null && item.Value != null)
            {
                if (item.Value is DateTime)
                {
                    return (DateTime)item.Value;
                }
                else
                {
                    DateTime result = DateTime.MinValue;
                    DateTime.TryParse(item.Value.ToString(), out result);
                    return result;
                }
            }
            return DateTime.MinValue;
        }

        #endregion

        protected override EntityItem GetItem(string key)
        {
            var item = Collection[key];
            if (item != null)
            {
                return item as EntityItem;
            }
            return null;
        }

        protected override void SetValue(string key, object value)
        {
            var item = Collection[key];
            if (item == null)
            {
                if (!IsNullValue(key, value))
                {
                    Collection[key] = new EntityItem() { Key = key, Value = value };
                }
            }
            else
            {
                if (IsNullValue(key, value))
                {
                    if (ID > 0)
                    {
                        //清空数据库字段中的值
                        (item as EntityItem).Value = DBNull.Value;
                    }
                    else
                    {
                        Collection.Remove(key);
                    }
                }
                else
                {
                    (item as EntityItem).Value = value;
                }
            }
        }

        public override void SetUIValue(string key, object value)
        {
            var item = Collection[key];
            if (item == null)
            {
                if (!IsNullUIValue(key, value))
                {
                    Collection[key] = new EntityItem() { Key = key, Value = value };
                }
            }
            else
            {
                if (IsNullUIValue(key, value))
                {
                    if (ID > 0)
                    {
                        //清空数据库字段中的值
                        (item as EntityItem).Value = DBNull.Value;
                    }
                    else
                    {
                        Collection.Remove(key);
                    }
                }
                else
                {
                    (item as EntityItem).Value = value;
                }
            }
        }

        internal virtual void SetDbValue(string key, object value)
        {
            if (value != DBNull.Value)
            {
                Collection[key] = new EntityItem() { Key = key, Value = value };
            }
        }

        private void Remove_G()
        {
            foreach (string key in Collection.Keys)
            {
                if (key.EndsWith("_g", StringComparison.OrdinalIgnoreCase))
                {
                    Collection.Remove(key);
                }
            }
        }

        #region 数据库操作

        public long Save()
        {
            return Save(null);
        }

        internal long Save(EntityProvider provider)
        {
            this.Remove_G();

            bool isNew = true;
            var entityProvider = GetProvider(provider, ref isNew);

            long id = 0;
            try
            {
                Type thisType = this.GetType();
                var attrs = thisType.GetCustomAttributes(typeof(EntityChildAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    #region 使用事务去保存该实体与其他相关实体

                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (this.ID > 0)
                        {
                            id = this.ID;
                            entityProvider.ExcuteUpdate(this);
                        }
                        else
                        {
                            if (this.Collection.Count > 0)
                            {
                                id = entityProvider.ExcuteInsert(this);
                            }
                        }

                        if (attrs != null && attrs.Length > 0)
                        {
                            #region 保存外键关联表信息

                            foreach (EntityChildAttribute attr in attrs)
                            {
                                foreach (var pname in attr.PropertyNames)
                                {
                                    var pi = thisType.GetProperty(pname, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                                    if (pi == null)
                                    {
                                        continue;
                                    }

                                    var v = pi.GetValue(this);
                                    if (v == null)
                                    {
                                        continue;
                                    }

                                    var fkey = pi.GetCustomAttribute<FKeyAttribute>(false);
                                    if (attr is EntityChildrenAttribute)
                                    {
                                        #region Save EntityChildren

                                        var list = v as IList;
                                        foreach (EntityBase data in list)
                                        {
                                            if (data.ID == 0 && fkey != null && fkey.FKey != null && fkey.FKey.Length > 0)
                                            {
                                                data.SetDbValue(fkey.FKey, this.ID);
                                            }
                                            data.Save(entityProvider);
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Save EntityChild

                                        var entity = v as EntityBase;
                                        if (entity.ID == 0 && fkey != null && fkey.FKey != null && fkey.FKey.Length > 0)
                                        {
                                            entity.SetDbValue(fkey.FKey, this.ID);
                                        }
                                        entity.Save(entityProvider);

                                        #endregion
                                    }
                                }
                            }

                            #endregion
                        }

                        scope.Complete();
                    }

                    #endregion
                }
                else
                {
                    #region 保存当前实体

                    if (this.ID > 0)
                    {
                        id = this.ID;
                        entityProvider.ExcuteUpdate(this);
                    }
                    else
                    {
                        id = entityProvider.ExcuteInsert(this);
                    }

                    #endregion
                }
            }
            finally
            {
                if (isNew)
                {
                    entityProvider.Close();
                }
            }
            return id;
        }

        #endregion
    }
}
