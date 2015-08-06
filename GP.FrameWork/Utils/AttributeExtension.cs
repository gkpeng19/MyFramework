using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GP.FrameWork.Utils
{
    public static class AttributeExtension
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var attrs = type.GetCustomAttributes(typeof(T), false);
            if (attrs.Length <= 0)
            {
                return null;
            }
            return attrs[0] as T;
        }

        public static T GetAttribute<T>(this Type type, string propertyName) where T : Attribute
        {
            var pi = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (pi != null)
            {
                var attrs = pi.GetCustomAttributes(typeof(T), false);
                if (attrs.Length > 0)
                {
                    return attrs[0] as T;
                }
            }
            return null;
        }
    }
}
