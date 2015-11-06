using GOMFrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace G.Util.Mvc
{
    public class EntityModelBinder : IModelBinder
    {
        static Regex regger = new Regex("_g]?$", RegexOptions.IgnoreCase);
        static Regex reggerDate = new Regex(@"^\d{4}-\d{2}-\d{2}( \d{2}:\d{2}:\d{2})?$");
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
                if (regger.IsMatch(key))
                {
                    continue;
                }
                if (key.IndexOf('.') < 0 && key.IndexOf('[') < 0)
                {
                    string value = ValueCollection[key];
                    if (reggerDate.IsMatch(value))
                    {
                        (target as DataBase).SetUIValue(key, DateTime.Parse(value));
                    }
                    else
                    {
                        (target as DataBase).SetUIValue(key, value);
                    }
                }
                else
                {
                    if (instanceDic == null)
                    {
                        instanceDic = new Dictionary<string, object>();
                    }

                    string[] strs = key.Split('.', '[', ']');
                    var keyList = new List<string>();
                    for (var i = 0; i < strs.Length; ++i)
                    {
                        var str = strs[i];
                        if (str.Length == 0)
                        {
                            continue;
                        }
                        else if (str.Length == 1)
                        {
                            keyList.Add(strs[i - 1] + str);
                        }
                        else
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
                    string value = ValueCollection[key];
                    if (reggerDate.IsMatch(value))
                    {
                        (preInstance as DataBase).SetUIValue(keyList[index], DateTime.Parse(value));
                    }
                    else
                    {
                        (preInstance as DataBase).SetUIValue(keyList[index], value);
                    }
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

namespace G.Util.Net.Http
{
    public class EntityModelBinder : System.Web.Http.ModelBinding.IModelBinder
    {
        static Regex regger = new Regex("_g]?$", RegexOptions.IgnoreCase);
        static Regex reggerDate = new Regex(@"^\d{4}-\d{2}-\d{2}( \d{2}:\d{2}:\d{2})?$");

        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            NameValueCollection ValueCollection = null;
            var request = (actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase).Request;
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
                return true;
            }

            Dictionary<string, object> instanceDic = null;

            var target = Activator.CreateInstance(bindingContext.ModelType);
            if (target is EntityBase)
            {
                var pkey = (target as EntityBase).PrimaryKey.ToLower();

                object eid = null;
                if (actionContext.ControllerContext.RouteData.Values.ContainsKey("id"))
                {
                    eid = actionContext.ControllerContext.RouteData.Values["id"];
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
                if (regger.IsMatch(key))
                {
                    continue;
                }
                if (key.IndexOf('.') <= 0 && key.IndexOf('[') <= 0)
                {
                    string value = ValueCollection[key];
                    if (reggerDate.IsMatch(value))
                    {
                        (target as DataBase).SetUIValue(key, DateTime.Parse(value));
                    }
                    else
                    {
                        (target as DataBase).SetUIValue(key, value);
                    }
                }
                else
                {
                    if (instanceDic == null)
                    {
                        instanceDic = new Dictionary<string, object>();
                    }

                    string[] strs = key.Split('.', '[', ']');
                    var keyList = new List<string>();
                    for (var i = 0; i < strs.Length; ++i)
                    {
                        var str = strs[i];
                        if (str.Length == 0)
                        {
                            continue;
                        }
                        else if (str.Length == 1)
                        {
                            keyList.Add(strs[i - 1] + str);
                        }
                        else
                        {
                            keyList.Add(str);
                        }
                    }

                    if (!instanceDic.ContainsKey(keyList[0]))
                    {
                        thisInstance = G.Util.Mvc.EntityModelBinder.SetPropertyObject(target, keyList[0]);
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
                            thisInstance = G.Util.Mvc.EntityModelBinder.SetPropertyObject(preInstance, keyList[index]);
                            instanceDic[keyList[index]] = thisInstance;
                        }
                    }

                    preInstance = instanceDic[keyList[index - 1]];
                    if (preInstance == null)
                    {
                        throw new Exception("对象构建失败：Name属性设置有误！");
                    }
                    string value = ValueCollection[key];
                    if (reggerDate.IsMatch(value))
                    {
                        (preInstance as DataBase).SetUIValue(keyList[index], DateTime.Parse(value));
                    }
                    else
                    {
                        (preInstance as DataBase).SetUIValue(keyList[index], value);
                    }
                }
            }

            bindingContext.Model = target;

            return true;
        }
    }
}
