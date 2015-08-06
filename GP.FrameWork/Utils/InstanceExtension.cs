using GP.FrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GP.FrameWork.Utils
{
    public static class InstanceExtension
    {
        public static void SetPropertyValue(this object instance, string propertyName, object value, bool isDbValue = true)
        {
            try
            {
                if (instance is SearchEntity || instance is ProcEntity)
                {
                    (instance as DataBase).SetUIValue(propertyName, value);
                }
                else if (instance is EntityBase)
                {
                    EntityBase e = instance as EntityBase;
                    if (propertyName.Equals(e.PrimaryKey,StringComparison.OrdinalIgnoreCase))
                    {
                        string strv = value.ToString();
                        if (strv.Length > 0)
                        {
                            e.ID = long.Parse(strv);
                        }
                    }
                    else
                    {
                        if (isDbValue)
                        {
                            e.SetDbValue(propertyName, value);
                        }
                        else
                        {
                            e.SetUIValue(propertyName, value);
                        }
                    }
                }
                else
                {
                    if (value != null && value != DBNull.Value)
                    {
                        PropertyInfo pi = instance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                        if (pi != null)
                        {
                            if (!isDbValue)
                            {
                                Type totype = pi.PropertyType;
                                try
                                {
                                    if (totype.IsGenericType && totype.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                    {
                                        totype = totype.GetGenericArguments()[0];
                                    }
                                    value = Convert.ChangeType(value, totype);
                                }
                                catch
                                {
                                    return;
                                }
                            }
                            pi.SetValue(instance, value, null);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("属性[{0}]赋值为[{1}]失败：{2}", propertyName, value, ex.Message));
            }
        }

        public static object SetPropertyObject(this object instance, string propertyName, object value = null)
        {
            if (instance is IList)
            {
                if (value == null)
                {
                    Type etype = instance.GetType().GetGenericArguments()[0];
                    value = Activator.CreateInstance(etype);
                }
                (instance as IList).Add(value);
            }
            else
            {
                PropertyInfo pi = instance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (pi != null)
                {
                    if (value == null)
                    {
                        value = Activator.CreateInstance(pi.PropertyType);
                    }
                    pi.SetValue(instance, value, null);
                }
            }
            return value;
        }
    }
}
