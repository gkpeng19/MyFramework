using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Html
{
    public class HtmlTree
    {
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

    public class HtmlTreeNode
    {
        public HtmlTreeNode()
        {
            children = new List<HtmlTreeNode>();
        }
        public int id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }

        public int parentid { get; set; }

        public List<HtmlTreeNode> children { get; set; }
    }
}
