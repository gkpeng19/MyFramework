using G.Util.Extension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinTemplate.UControl;

namespace WinTemplate
{
    public class GContent
    {
        public GContent()
        {
            tables = new List<GTable>();
            jsons = new List<GJson>();
            forms = new List<GForm>();
            binds = new List<GBind>();
        }

        public List<GTable> tables { get; set; }
        public List<GJson> jsons { get; set; }
        public List<GForm> forms { get; set; }
        public List<GBind> binds { get; set; }
    }

    #region ForTable

    public class GTable
    {
        public GTable()
        {
            data = new GTableData();
        }
        public string id { get; set; }
        public GTableData data { get; set; }
    }

    public class GTableData
    {
        public GTableData()
        {
            headers = new List<GTableHeader>();
        }

        public string url { get; set; }
        public string pkey { get; set; }
        public string pagesize { get; set; }

        public List<GTableHeader> headers { get; set; }
    }

    public class GTableHeader
    {
        public GTableHeader()
        {
            actionbtns = new List<GTableActionBtn>();
        }
        public string type { get; set; }
        public string header { get; set; }
        public string bindproperty { get; set; }
        public List<GTableActionBtn> actionbtns { get; set; }
    }

    public class GTableActionBtn
    {
        public string type { get; set; }
        public string formdataid { get; set; }
        public string action { get; set; }
    }

    #endregion

    #region ForJson

    public class GJson
    {
        public GJson()
        {
            data = new List<GJsonC>();
        }
        public string id { get; set; }
        public string url { get; set; }

        public List<GJsonC> data { get; set; }
    }

    public class GJsonC
    {
        public GJsonC()
        {
            c = new List<GJsonItem>();
        }
        public List<GJsonItem> c { get; set; }
    }

    public class GJsonItem : GNode { }

    #endregion

    #region ForForm

    public class GForm
    {
        public GForm()
        {
            data = new List<GFormC>();
        }
        public string id { get; set; }

        public string isshow { get; set; }

        public int size { get; set; }

        public List<GFormC> data { get; set; }

        public GFormExData extdata { get; set; }
    }

    public class GFormC
    {
        public GFormC()
        {
            c = new List<GFormItem>();
        }
        public List<GFormItem> c { get; set; }
    }

    public class GFormItem : GNode { }

    public class GFormExData
    {
        public GFormExData()
        {
            extdatas = new List<GFormExDataItem>();
        }
        public List<GFormExDataItem> extdatas { get; set; }
    }

    public class GFormExDataItem
    {
        public GFormExDataItem()
        {
            validates = new List<GFormValidate>();
        }
        public string dataid { get; set; }
        public string showname { get; set; }
        public string size { get; set; }
        public List<GFormValidate> validates { get; set; }
    }

    public class GFormValidate
    {
        string _minlen = string.Empty;
        string _maxlen = string.Empty;

        public string type { get; set; }
        public string minlen
        {
            get
            {
                if (_minlen.Length == 0)
                {
                    _minlen = "--";
                }
                return _minlen;
            }
            set { _minlen = value; }
        }
        public string maxlen
        {
            get
            {
                if (_maxlen.Length == 0)
                {
                    _maxlen = "++";
                }
                return _maxlen;
            }
            set { _maxlen = value; }
        }
        public string regstr { get; set; }
    }

    public enum GFormValidateEnum
    {
        required,
        number,
        numint,
        email,
        ipaddress,
        postcode,
        telnum,
        idcard,
        lenvalidate,
        regvalidate
    }

    #endregion

    #region ForBind

    public class GBind
    {
        public GBind()
        {
            data = new List<GBindC>();
        }
        public string id { get; set; }

        /// <summary>
        /// 1：对象绑定到div，2：数组绑定到select控件
        /// </summary>
        public string type { get; set; }
        public List<GBindC> data { get; set; }
    }

    public class GBindC
    {
        public GBindC()
        {
            c = new List<GBindItem>();
        }
        public List<GBindItem> c { get; set; }
    }

    public class GBindItem
    {
        public string v { get; set; }
        public string f { get; set; }
    }

    #endregion


    public class GNode
    {
        private string _v;
        private string _f;
        private string _t = string.Empty;
        private string _fld = string.Empty;

        public string v
        {
            get { return _v; }
            set
            {
                _v = value;
                var index = value.LastIndexOf('_');
                if (index > 0)
                {
                    _t = value.Substring(index + 1);
                }
            }
        }
        public string t
        {
            get { return _t; }
        }

        public string f
        {
            get { return _f; }
            set
            {
                _f = value;
                if (value != null && value.Length > 0)
                {
                    var index = value.IndexOf('{');
                    index = index <= 0 ? value.IndexOf('[') : index;
                    if (index > 0)
                    {
                        _fld = value.Substring(0, index - 1);
                    }
                }
            }
        }
        public string fld
        {
            get { return _fld; }
        }
    }

    #region For主界面

    public enum TreeNodeEnum : int
    {
        None = 0,
        Solution = 1,
        Project = 2,
        Folder = 3,
        File = 4
    }

    [XmlEntity]
    public class GTreeNode : GOMFrameWork.DataEntity.EntityBase
    {
        [XmlKey]
        public string Text
        {
            get { return GetString("Text"); }
            set { SetValue("Text", value); }
        }

        public TreeNodeEnum Type { get; protected set; }

        [XmlKey]
        public string SPath
        {
            get { return GetString("SPath"); }
            set { SetValue("SPath", value); }
        }
    }

    [XmlEntity]
    public class GFile : GTreeNode
    {
        public GFile()
        {
            this.Type = TreeNodeEnum.File;
        }

        public GFile(string spath)
            : this()
        {
            base.SPath = spath;
        }

        public KryptonTab Tab { get; set; }
    }

    [XmlEntity]
    public class GFolder : GTreeNode
    {
        public GFolder()
        {
            this.Type = TreeNodeEnum.Folder;
            Files = new List<GFile>();
        }
        public GFolder(string spath)
            : this()
        {
            base.SPath = spath;
        }

        [XmlList]
        public List<GFile> Files { get; set; }
    }

    [XmlEntity]
    public class GProject : GTreeNode
    {
        public GProject()
        {
            this.Type = TreeNodeEnum.Project;
            Files = new List<GFile>();
            Folders = new List<GFolder>();
        }
        public GProject(string spath)
            : this()
        {
            base.SPath = spath;
        }

        [XmlList]
        public List<GFile> Files { get; set; }
        [XmlList]
        public List<GFolder> Folders { get; set; }
    }

    [XmlEntity]
    public class GSolution : GTreeNode
    {
        public GSolution()
        {
            this.Type = TreeNodeEnum.Solution;
            this.Projects = new List<GProject>();
        }
        public GSolution(string spath)
            : this()
        {
            base.SPath = spath;
        }

        [XmlList]
        public List<GProject> Projects { get; set; }
    }

    #endregion

    public enum ImageListEnum : int
    {
        Solution,
        Project,
        Folder,
        File
    }
}
