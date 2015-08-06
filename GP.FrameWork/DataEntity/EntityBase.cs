using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using GP.FrameWork.Utils;

namespace GP.FrameWork.DataEntity
{
    public abstract class DataBase : INotifyPropertyChanged
    {
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
                    var attr = this.GetType().GetAttribute<UIValueZeroNotEqualNullAttribute>(key);
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

        public abstract void Remove_G();
    }

    /// <summary>
    /// 代码生成时，指定TableName、PrimaryKey、主键的生成为（get{return base.ID;} set{base.ID=value;}）
    /// </summary>
    public class EntityBase : DataBase
    {
        private string _primaryKey = null;
        public string PrimaryKey
        {
            get { return _primaryKey == null ? "ID" : _primaryKey; }
            set { _primaryKey = value; }
        }

        public long ID { get; set; }

        protected EntityStatus _S = EntityStatus.Normal;
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

        public EntityStatus GetEntityStatus()
        {
            return _S;
        }

        public void ToDelete()
        {
            _S = EntityStatus.Delete;
        }

        #region 实体对应表信息

        protected string TableName { get; set; }
        private TableInfo _TableInfo;
        public TableInfo TableInfo
        {
            get
            {
                if (_TableInfo != null)
                {
                    return _TableInfo;
                }
                _TableInfo = new TableInfo(TableName);

                PropertyInfo property = this.GetType().GetProperty("Details", BindingFlags.Instance | BindingFlags.Public);
                if (property != null)
                {
                    object[] objs = property.GetCustomAttributes(typeof(EntityDetailsAttribute), true);
                    if (objs.Length > 0)
                    {
                        EntityDetailsAttribute attribute = objs[0] as EntityDetailsAttribute;
                        _TableInfo.FKey = attribute.FKey;
                    }
                }
                return _TableInfo;
            }
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
                if (IsNullValue(key, value))
                {
                    return;
                }
                Collection[key] = new EntityItem() { Key = key, Value = value };
            }
            else
            {
                if (IsNullValue(key, value))
                {
                    if (ID > 0)
                    {
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
            if (!IsNullUIValue(key, value))
            {
                Collection[key] = new EntityItem() { Key = key, Value = value };
            }
            else
            {
                if (this.ID > 0)
                {
                    Collection[key] = new EntityItem() { Key = key, Value = DBNull.Value };
                }
            }
        }

        public virtual void SetDbValue(string key, object value)
        {
            if (value != DBNull.Value)
            {
                Collection[key] = new EntityItem() { Key = key, Value = value };
            }
        }

        public object GetEntityDetails()
        {
            PropertyInfo property = this.GetType().GetProperty("Details", BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                object[] AObjs = property.GetCustomAttributes(typeof(EntityDetailsAttribute), true);
                if (AObjs != null && AObjs.Length > 0)
                {
                    return property.GetValue(this, null);
                }
            }
            return null;
        }

        public override void Remove_G()
        {
            foreach (string key in Collection.Keys)
            {
                if (key.EndsWith("_g", StringComparison.OrdinalIgnoreCase))
                {
                    Collection.Remove(key);
                }
            }
        }
    }

    public class SltEntityBase : EntityBase
    {
        public SltEntityBase(EntityStatus s = EntityStatus.New)
            : base()
        {
            _S = s;
        }

        [JsonIgnore]
        public EntityStatus PreS { get; set; }

        public EntityStatus S
        {
            get { return _S; }
            set
            {
                if (_S != value)
                {
                    PreS = _S;
                    _S = value;
                }
            }
        }

        protected override void SetValue(string key, object value)
        {
            var item = Collection[key];
            if (item == null)
            {
                if (IsNullValue(key, value))
                {
                    return;
                }
                Collection[key] = new EntityItem() { Key = key, Value = value };
                if (this._S == EntityStatus.Normal)
                {
                    S = EntityStatus.Modify;
                }
            }
            else
            {
                EntityItem eitem = item as EntityItem;
                if (IsNullValue(key, value))
                {
                    if (this._S == EntityStatus.New)
                    {
                        Collection.Remove(key);
                    }
                    else
                    {
                        eitem.Value = DBNull.Value;
                        S = EntityStatus.Modify;
                    }
                }
                else
                {
                    if (!value.Equals(eitem.Value))
                    {
                        eitem.Value = value;
                        S = EntityStatus.Modify;
                    }
                }
            }

            OnPropertyChanged(key);
        }
    }

    public class TableInfo
    {
        public TableInfo(string tableName)
        {
            this.TableName = tableName;
        }

        public string TableName { get; set; }
        public string FKey { get; set; }
    }
}
