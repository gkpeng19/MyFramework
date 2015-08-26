using G.Util.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Html
{
    public class HtmlSelect
    {
        /// <summary>
        /// 从枚举中获取Select
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<HtmlSelectItem> GetHtmlSelectByEnum(Type enumType)
        {
            List<HtmlSelectItem> list = new List<HtmlSelectItem>();
            foreach (var item in EnumExtension.GetEnumCollection(enumType))
            {
                list.Add(new HtmlSelectItem() { k = item.Description, v = item.Value });
            }
            return list;
        }

        /// <summary>
        /// 从集合中获取Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">可枚举类型</param>
        /// <param name="setValueFunc">赋值委托</param>
        /// <returns></returns>
        public static List<HtmlSelectItem> GetHtmlSelectByCollection<T>(IEnumerable<T> collection, Func<T, HtmlSelectItem> setValueFunc)
        {
            List<HtmlSelectItem> list = new List<HtmlSelectItem>();
            foreach (T item in collection)
            {
                list.Add(setValueFunc(item));
            }
            return list;
        }
    }

    public class HtmlSelectItem
    {
        public string k { get; set; }
        public int v { get; set; }
    }
}
