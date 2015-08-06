using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GOMFrameWork.Utils
{
    public static class AttributeExtension
    {
        public static T GetPropertyAttribute<T>(this Type type, string propertyName) where T : Attribute
        {
            var pi = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (pi != null)
            {
                var attrs = pi.GetCustomAttribute<T>(false);
                if (attrs != null)
                {
                    return attrs;
                }
            }
            return null;
        }
    }
}
