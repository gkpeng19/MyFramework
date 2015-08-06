using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace G.Util
{
    public static class HttpRequester
    {
        public static T GetModel<T>(this HttpRequest request) where T : DataBase, new()
        {
            NameValueCollection ValueCollection = null;
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

            T target = new T();

            object preInstance = null;
            object thisInstance = null;

            foreach (string key in ValueCollection.Keys)
            {
                if (key.Length == 0 || key.EndsWith("_c", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (key.IndexOf('.') <= 0)
                {
                    (target as DataBase).SetUIValue(key, ValueCollection[key]);
                }
                else
                {
                    if (instanceDic == null)
                    {
                        instanceDic = new Dictionary<string, object>();
                    }

                    string[] strs = key.Split('.');
                    if (!instanceDic.ContainsKey(strs[0]))
                    {
                        thisInstance = EntityModelBinder.SetPropertyObject(target, strs[0]);
                        instanceDic[strs[0]] = thisInstance;
                    }

                    int index = 1;
                    for (index = 1; index <= strs.Length - 2; ++index)
                    {
                        if (!instanceDic.ContainsKey(strs[index]))
                        {
                            preInstance = instanceDic[strs[index - 1]];
                            if (preInstance == null)
                            {
                                throw new Exception("对象构建失败：Name属性设置有误！");
                            }
                            thisInstance = EntityModelBinder.SetPropertyObject(preInstance, strs[index]);
                            instanceDic[strs[index]] = thisInstance;
                        }
                    }

                    preInstance = instanceDic[strs[index - 1]];
                    if (preInstance == null)
                    {
                        throw new Exception("对象构建失败：Name属性设置有误！");
                    }
                    (preInstance as DataBase).SetUIValue(strs[index], ValueCollection[key]);
                }
            }

            return target;
        }
    }
}
