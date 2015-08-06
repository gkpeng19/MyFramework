using GP.FrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GP.FrameWork.Utils
{
    public static class DataConverter
    {
        /// <summary>
        /// 转换数据集到集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">数据集</param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(this IDataReader reader) where T : new()
        {
            List<T> rlist = new List<T>();
            if (reader == null)
            {
                return rlist;
            }

            while (reader.Read())
            {
                T obj = new T();
                if (obj is SltEntityBase)
                {
                    (obj as SltEntityBase).S = EntityStatus.Normal;
                }
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj.SetPropertyValue(reader.GetName(i), reader.GetValue(i));
                }
                rlist.Add(obj);
            }

            return rlist;
        }

        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            List<T> rlist = new List<T>();
            if (table == null || table.Rows.Count <= 0)
            {
                return rlist;
            }

            foreach (DataRow dr in table.Rows)
            {
                T obj = new T();
                if (obj is SltEntityBase)
                {
                    (obj as SltEntityBase).S = EntityStatus.Normal;
                }
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    obj.SetPropertyValue(table.Columns[i].ColumnName, dr[i]);
                }

                rlist.Add(obj);
            }

            return rlist;
        }

        /// <summary>
        /// 转换DataTable到泛型
        /// </summary>
        /// <typeparam name="T">要转换到的泛型类型</typeparam>
        /// <param name="table">数据源DataTable</param>
        /// <returns>泛型对象</returns>
        public static T ConvertToObject<T>(this DataTable table) where T : new()
        {
            if (table == null || table.Rows.Count <= 0)
            {
                return default(T);
            }

            T instance = new T();
            if (instance is IList)
            {
                IList rlist = instance as IList;
                string header = string.Empty;
                foreach (DataRow dr in table.Rows)
                {
                    var entity = Activator.CreateInstance(typeof(T).GetGenericArguments()[0]);
                    if (entity is SltEntityBase)
                    {
                        (entity as SltEntityBase).S = EntityStatus.Normal;
                    }
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        entity.SetPropertyValue(table.Columns[i].ColumnName, dr[i]);
                    }
                    rlist.Add(entity);
                }
            }
            else
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    instance.SetPropertyValue(table.Columns[i].ColumnName, table.Rows[0][i]);
                }
            }

            return instance;
        }

        /// <summary>
        /// 转换DataTable到指定类型
        /// </summary>
        /// <param name="table">数据源DataTable</param>
        /// <param name="objType">指定要转换到的类型</param>
        /// <returns>指定objType类型的对象</returns>
        public static object ConvertToObject(this DataTable table, Type objType)
        {
            if (table == null)
            {
                return null;
            }

            var instance = Activator.CreateInstance(objType);
            if (table.Rows.Count <= 0)
            {
                return instance;
            }

            if (instance is IList)
            {
                IList rlist = instance as IList;
                string header = string.Empty;
                foreach (DataRow dr in table.Rows)
                {
                    var entity = Activator.CreateInstance(objType.GetGenericArguments()[0]);
                    if (entity is SltEntityBase)
                    {
                        (entity as SltEntityBase).S = EntityStatus.Normal;
                    }
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        entity.SetPropertyValue(table.Columns[i].ColumnName, dr[i]);
                    }
                    rlist.Add(entity);
                }
            }
            else
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    instance.SetPropertyValue(table.Columns[i].ColumnName, table.Rows[0][i]);
                }
            }

            return instance;
        }
    }
}
