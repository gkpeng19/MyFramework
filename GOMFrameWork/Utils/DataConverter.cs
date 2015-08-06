using GOMFrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GOMFrameWork.Utils
{
    internal static class DataConverter
    {
        /// <summary>
        /// 转换数据集到集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">数据集</param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(this IDataReader reader) where T : EntityBase, new()
        {
            List<T> rlist = new List<T>();
            if (reader == null)
            {
                return rlist;
            }

            while (reader.Read())
            {
                T obj = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj.SetDbValue(reader.GetName(i), reader.GetValue(i));
                }
                rlist.Add(obj);
            }

            return rlist;
        }

        public static T ConvertToEntity<T>(this IDataReader reader) where T : EntityBase, new()
        {
            if (reader == null)
            {
                return null;
            }

            if (reader.Read())
            {
                T obj = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj.SetDbValue(reader.GetName(i), reader.GetValue(i));
                }
                return obj;
            }

            return null;
        }

        public static List<T> ConvertToList<T>(this DataTable table) where T : EntityBase, new()
        {
            List<T> rlist = new List<T>();
            if (table == null || table.Rows.Count <= 0)
            {
                return rlist;
            }

            foreach (DataRow dr in table.Rows)
            {
                T obj = new T();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    obj.SetDbValue(table.Columns[i].ColumnName, dr[i]);
                }

                rlist.Add(obj);
            }

            return rlist;
        }

        public static EntityBase ConvertToEntity(this DataTable table, Type entityType)
        {
            if (!typeof(EntityBase).IsAssignableFrom(entityType))
            {
                return null;
            }

            if (table == null || table.Rows.Count <= 0)
            {
                return null;
            }

            EntityBase obj = Activator.CreateInstance(entityType) as EntityBase;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                obj.SetDbValue(table.Columns[i].ColumnName, table.Rows[0][i]);
            }
            return obj;
        }

        public static IList ConvertToEntityList(this DataTable table, Type entityListType)
        {
            var args = entityListType.GetGenericArguments();
            if (args == null || args.Length == 0 || args.Length > 1)
            {
                return null;
            }

            if (!typeof(EntityBase).IsAssignableFrom(args[0]))
            {
                return null;
            }

            var entityList = Activator.CreateInstance(entityListType);
            if (entityList is IList)
            {
                IList rlist = entityList as IList;
                foreach (DataRow dr in table.Rows)
                {
                    EntityBase entity = Activator.CreateInstance(args[0]) as EntityBase;
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        entity.SetDbValue(table.Columns[i].ColumnName, dr[i]);
                    }

                    rlist.Add(entity);
                }

                return rlist;
            }

            return null;
        }

        #region NoUse

        ///// <summary>
        ///// 转换DataTable到泛型
        ///// </summary>
        ///// <typeparam name="T">要转换到的泛型类型</typeparam>
        ///// <param name="table">数据源DataTable</param>
        ///// <returns>泛型对象</returns>
        //public static T ConvertToObject<T>(this DataTable table) where T : EntityBase, new()
        //{
        //    if (table == null || table.Rows.Count <= 0)
        //    {
        //        return default(T);
        //    }

        //    T instance = new T();
        //    if (instance is IList)
        //    {
        //        IList rlist = instance as IList;
        //        string header = string.Empty;
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            var entity = Activator.CreateInstance(typeof(T).GetGenericArguments()[0]);
        //            for (int i = 0; i < table.Columns.Count; i++)
        //            {
        //                entity.SetPropertyValue(table.Columns[i].ColumnName, dr[i]);
        //            }
        //            rlist.Add(entity);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < table.Columns.Count; i++)
        //        {
        //            instance.SetPropertyValue(table.Columns[i].ColumnName, table.Rows[0][i]);
        //        }
        //    }

        //    return instance;
        //}

        ///// <summary>
        ///// 转换DataTable到指定类型
        ///// </summary>
        ///// <param name="table">数据源DataTable</param>
        ///// <param name="objType">指定要转换到的类型</param>
        ///// <returns>指定objType类型的对象</returns>
        //public static object ConvertToObject(this DataTable table, Type objType)
        //{
        //    if (table == null)
        //    {
        //        return null;
        //    }

        //    var instance = Activator.CreateInstance(objType);
        //    if (table.Rows.Count <= 0)
        //    {
        //        return instance;
        //    }

        //    if (instance is IList)
        //    {
        //        IList rlist = instance as IList;
        //        string header = string.Empty;
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            var entity = Activator.CreateInstance(objType.GetGenericArguments()[0]);
        //            for (int i = 0; i < table.Columns.Count; i++)
        //            {
        //                entity.SetPropertyValue(table.Columns[i].ColumnName, dr[i]);
        //            }
        //            rlist.Add(entity);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < table.Columns.Count; i++)
        //        {
        //            instance.SetPropertyValue(table.Columns[i].ColumnName, table.Rows[0][i]);
        //        }
        //    }

        //    return instance;
        //}

        #endregion

    }
}
