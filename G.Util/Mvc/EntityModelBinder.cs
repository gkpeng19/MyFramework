using GOMFrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G.Util.Mvc
{
    public class EntityModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection ValueCollection = null;
            var request = controllerContext.RequestContext.HttpContext.Request;
            if (request.RequestType.Equals("GET"))
            {
                ValueCollection = request.QueryString;
            }
            else if (request.RequestType.Equals("POST"))
            {
                ValueCollection = request.Form;
            }
            else { }

            if (ValueCollection.Count <= 0)
            {
                return null;
            }

            Dictionary<string, object> instanceDic = null;

            var target = Activator.CreateInstance(bindingContext.ModelType);
            if (target is EntityBase)
            {
                var pkey = (target as EntityBase).PrimaryKey.ToLower();

                object eid = null;
                if (controllerContext.RouteData.Values.ContainsKey("id"))
                {
                    eid = controllerContext.RouteData.Values["id"];
                }

                if (eid != null && eid.ToString().Length > 0)
                {
                    (target as EntityBase).SetUIValue(pkey, eid);
                }
            }

            object preInstance = null;
            object thisInstance = null;

            foreach (string key in ValueCollection.Keys)
            {
                if (key.EndsWith("_g", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (key.IndexOf('.') <= 0 && key.IndexOf('[') <= 0)
                {
                    (target as DataBase).SetUIValue(key, ValueCollection[key]);
                }
                else
                {
                    if (instanceDic == null)
                    {
                        instanceDic = new Dictionary<string, object>();
                    }

                    string[] strs = key.Split('.', '[', ']');
                    var keyList = new List<string>();
                    foreach (var str in strs)
                    {
                        if (str.Length > 0)
                        {
                            keyList.Add(str);
                        }
                    }

                    if (!instanceDic.ContainsKey(keyList[0]))
                    {
                        thisInstance = SetPropertyObject(target, keyList[0]);
                        instanceDic[keyList[0]] = thisInstance;
                    }

                    int index = 1;
                    for (index = 1; index <= keyList.Count - 2; ++index)
                    {
                        if (!instanceDic.ContainsKey(keyList[index]))
                        {
                            preInstance = instanceDic[keyList[index - 1]];
                            if (preInstance == null)
                            {
                                throw new Exception("对象构建失败：Name属性设置有误！");
                            }
                            thisInstance = SetPropertyObject(preInstance, keyList[index]);
                            instanceDic[keyList[index]] = thisInstance;
                        }
                    }

                    preInstance = instanceDic[keyList[index - 1]];
                    if (preInstance == null)
                    {
                        throw new Exception("对象构建失败：Name属性设置有误！");
                    }
                    (preInstance as DataBase).SetUIValue(keyList[index], ValueCollection[key]);
                }
            }

            return target;
        }

        internal static object SetPropertyObject(object instance, string propertyName)
        {
            object value = null;
            if (instance is IList)
            {
                Type etype = instance.GetType().GetGenericArguments()[0];
                value = Activator.CreateInstance(etype);
                if (value != null)
                {
                    (instance as IList).Add(value);
                }
            }
            else
            {
                PropertyInfo pi = instance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (pi != null)
                {
                    value = Activator.CreateInstance(pi.PropertyType);
                    if (value != null)
                    {
                        pi.SetValue(instance, value, null);
                    }
                }
            }
            return value;
        }
    }
}
