using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace GP.FrameWork.Utils
{
    public static class EntityTypeExtension
    {
        static object _obj = null;
        static Hashtable _propertiesCollection = null;
        static EntityTypeExtension()
        {
            _obj = new object();
            _propertiesCollection = new Hashtable();
        }

        public static PropertyInfo GetPropertyCache(this Type type, string propertyName)
        {
            Hashtable hsvalue = _propertiesCollection[type.GUID] as Hashtable;
            if (hsvalue == null)
            {
                lock (_obj)
                {
                    hsvalue = _propertiesCollection[type.GUID] as Hashtable;
                    if (hsvalue == null)
                    {
                        hsvalue = new Hashtable();
                        foreach (var p in type.GetProperties())
                        {
                            hsvalue.Add(p.Name.ToLower(), p);
                        }
                        _propertiesCollection.Add(type.GUID, hsvalue);
                    }
                }
            }

            var pi = hsvalue[propertyName.ToLower()];
            if (pi != null)
            {
                return pi as PropertyInfo;
            }
            return null;
        }

        public static Hashtable GetPropertiesCache(this Type type)
        {
            Hashtable hsvalue = _propertiesCollection[type.GUID] as Hashtable;
            if (hsvalue == null)
            {
                lock (_obj)
                {
                    hsvalue = _propertiesCollection[type.GUID] as Hashtable;
                    if (hsvalue == null)
                    {
                        hsvalue = new Hashtable();
                        foreach (var pi in type.GetProperties())
                        {
                            hsvalue.Add(pi.Name.ToLower(), pi);
                        }
                        _propertiesCollection.Add(type.GUID, hsvalue);
                    }
                }
            }

            return hsvalue;
        }
    }
}
