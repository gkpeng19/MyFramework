using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class HtmlSelectItem
    {
        public string k { get; set; }
        public int v { get; set; }
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

        public class HtmlTreeNodeState
        {
            public bool selected { get; set; }
            public bool disabled { get; set; }
            public bool opened { get; set; }
        }

        public HtmlTreeNodeState state { get; set; }
    }
}
