using EntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebSite
{
    public static class CommonUtil
    {
        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null; for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;
        }

        /// <summary>
        /// 根据树节点集合，初始化树
        /// </summary>
        /// <param name="orderNodes">有序树节点集合(按parentid正序)</param>
        /// <returns></returns>
        public static List<HtmlTreeNode> InitTree(List<HtmlTreeNode> orderNodes)
        {
            for (var i = orderNodes.Count - 1; i >= 0; --i)
            {
                var node = orderNodes[i];
                if (node.parentid == 0)
                {
                    break;
                }
                for (var j = i - 1; j >= 0; --j)
                {
                    if (node.parentid == orderNodes[j].id)
                    {
                        orderNodes[j].children.Add(node);
                        orderNodes.RemoveAt(i);
                        break;
                    }
                }
            }
            return orderNodes;
        }
    }
}