using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GP.FrameWork.Utils;

namespace GP.FrameWork.Mvc
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

            Hashtable instanceDic = null;

            var BindingInstance = Activator.CreateInstance(bindingContext.ModelType);
            var eid = controllerContext.RouteData.Values["id"];
            if (eid != null)
            {
                BindingInstance.SetPropertyValue("ID", eid, false);
            }

            object preInstance = null;
            object thisInstance = null;

            foreach (string key in ValueCollection.Keys)
            {
                if (key.Length == 0 || key.EndsWith("_c",StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (key.IndexOf('.') <= 0)
                {
                    BindingInstance.SetPropertyValue(key, ValueCollection[key], false);
                }
                else
                {
                    if (instanceDic == null)
                    {
                        instanceDic = new Hashtable();
                    }

                    string[] strs = key.Split('.');
                    if (instanceDic[strs[0]] == null)
                    {
                        thisInstance = BindingInstance.SetPropertyObject(strs[0]);
                        instanceDic[strs[0]] = thisInstance;
                    }

                    int index = 1;
                    for (index = 1; index <= strs.Length - 2; ++index)
                    {
                        if (instanceDic[strs[index]] == null)
                        {
                            preInstance = instanceDic[strs[index - 1]];
                            if (preInstance == null)
                            {
                                throw new Exception("对象构建失败：Name属性设置有误！");
                            }
                            thisInstance = preInstance.SetPropertyObject(strs[index]);
                            instanceDic[strs[index]] = thisInstance;
                        }
                    }

                    preInstance = instanceDic[strs[index - 1]];
                    if (preInstance == null)
                    {
                        throw new Exception("对象构建失败：Name属性设置有误！");
                    }
                    preInstance.SetPropertyValue(strs[index], ValueCollection[key], false);
                }
            }

            return BindingInstance;
        }
    }
}
