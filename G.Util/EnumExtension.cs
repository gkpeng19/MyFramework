using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GP.FrameWork.Utils
{
    public class EnumItem
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; set; }
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd != null)
            {
                var attrs = fd.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return (attrs[0] as EnumDescriptionAttribute).Description;
                }
            }
            return null;
        }

        public static List<EnumItem> GetEnumCollection(this Type enumType)
        {
            List<EnumItem> list = new List<EnumItem>();
            foreach (var em in Enum.GetValues(enumType))
            {
                list.Add(new EnumItem() { Value = (int)em, Description = (em as Enum).GetDescription() });
            }
            return list;
        }

        public static int? GetValueByDescription(this Type enumType, string description)
        {
            if (description == null)
            {
                return null;
            }

            foreach (var em in Enum.GetValues(enumType))
            {
                var desc = (em as Enum).GetDescription();
                if (desc != null && description.Equals(desc))
                {
                    return (int)em;
                }
            }

            return null;
        }
    }
}
