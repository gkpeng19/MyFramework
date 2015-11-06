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
            trees = new List<GTree>();
            tables = new List<GTable>();
            jsons = new List<GJson>();
            forms = new List<GForm>();
            binds = new List<GBind>();
        }

        public List<GTree> trees { get; set; }
        public List<GTable> tables { get; set; }
        public List<GJson> jsons { get; set; }
        public List<GForm> forms { get; set; }
        public List<GBind> binds { get; set; }

        public GAuthority authorities { get; set; }
    }

    #region ForTree

    public class GTree
    {
        public GTree()
        {
            defaultbtns = new List<string>();
            custombtns = new List<GCustomBtn>();
        }

        public string id { get; set; }
        public string nodeField { get; set; }
        public string parentIdField { get; set; }
        public string loadurl { get; set; }
        public string saveurl { get; set; }
        public string delurl { get; set; }
        public string reftableid { get; set; }
        public string reftablefk { get; set; }

        public List<string> defaultbtns { get; set; }

        public List<GCustomBtn> custombtns { get; set; }
    }

    #endregion

    #region ForTable

    public class GTable
    {
        public GTable()
        {
            data = new GTableData();
            defaultbtns = new List<string>();
            searchitems = new List<GTableSearchItem>();
            custombtns = new List<GCustomBtn>();
        }
        public string id { get; set; }
        public string searchid { get; set; }

        public List<string> defaultbtns { get; set; }
        public List<GTableSearchItem> searchitems { get; set; }

        public string transferurl { get; set; }

        public List<GCustomBtn> custombtns { get; set; }
        public GTableData data { get; set; }
    }

    public class GCustomBtn
    {
        public string id { get; set; }
        public string text { get; set; }
        public string action { get; set; }
    }

    public class GTableSearchItem
    {
        public string id { get; set; }
        public string title { get; set; }
        public string property { get; set; }

        /// <summary>
        /// 1：文本，2：下拉，3：日期
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 下拉框绑定的数据源ID,在全局对象中
        /// </summary>
        public string bindjsonid { get; set; }
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
            parameters = new List<string>();
        }
        public string id { get; set; }
        public string url { get; set; }

        public List<string> parameters { get; set; }

        public string paramsjson
        {
            get
            {
                string str = "ran:Math.random()";
                foreach (var p in parameters)
                {
                    str += "," + p + ":$.getUrlParam('" + p + "')";
                }
                return str;
            }
        }

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
            btns = new List<GFormBtn>();
        }
        public string id { get; set; }

        public string isshow { get; set; }

        public string disabledurl { get; set; }

        public int size { get; set; }

        public List<GFormC> data { get; set; }

        public GFormExData extdata { get; set; }

        public List<GFormBtn> btns { get; set; }
    }

    public class GFormBtn
    {
        public string id { get; set; }
        public string txt { get; set; }
        public string action { get; set; }
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

        public GFormUploader uploader { get; set; }
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

    public class GFormUploader
    {
        public string filter { get; set; }
        public string maxSize { get; set; }
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

    #region ForAuthority

    public class GAuthority
    {
        public GAuthority()
        {
            btns = new List<GAuthorityBtn>();
        }
        public string menushtml { get; set; }
        public List<GAuthorityBtn> btns { get; set; }
    }

    public class GAuthorityBtn
    {
        public string menuid { get; set; }
        public string btnid { get; set; }
        public string btnrole { get; set; }
    }

    #endregion

    #region 生成cs文件实体

    public class GCsModel
    {
        public GCsModel()
        {
            Controllers = new Dictionary<string, GCsControllerModel>(StringComparer.CurrentCultureIgnoreCase) { };
            AreaControllers = new Dictionary<string, Dictionary<string, GCsControllerModel>>(StringComparer.CurrentCultureIgnoreCase);
        }

        public Dictionary<string, GCsControllerModel> Controllers { get; set; }
        public Dictionary<string, Dictionary<string, GCsControllerModel>> AreaControllers { get; set; }

        private static void GetACAByUrl(string url, out string area, out string controller, out string action)
        {
            string[] strs = url.Split('/');
            if (strs.Length == 4)
            {
                area = strs[1];
            }
            else
            {
                area = string.Empty;
            }
            action = strs[strs.Length - 1];
            controller = strs[strs.Length - 2];
        }

        private static GCsControllerModel GetControllerByACA(GCsModel model, string projectName, string url, out string action)
        {
            string area, controller;
            GetACAByUrl(url, out area, out controller, out action);

            GCsControllerModel ctr = null;
            if (area.Length == 0)
            {
                if (model.Controllers.ContainsKey(controller))
                {
                    ctr = model.Controllers[controller];
                }
                else
                {
                    ctr = new GCsControllerModel() { ProjectName = projectName, Name = controller };
                    model.Controllers.Add(controller, ctr);
                }
            }
            else
            {
                Dictionary<string, GCsControllerModel> ctrs = null;
                if (model.AreaControllers.ContainsKey(area))
                {
                    ctrs = model.AreaControllers[area];
                }
                else
                {
                    ctrs = new Dictionary<string, GCsControllerModel>(StringComparer.CurrentCultureIgnoreCase);
                    model.AreaControllers.Add(area, ctrs);
                }

                if (ctrs.ContainsKey(controller))
                {
                    ctr = ctrs[controller];
                }
                else
                {
                    ctr = new GCsControllerModel() { ProjectName = projectName, Name = controller };
                    ctrs.Add(controller, ctr);
                }
            }
            return ctr;
        }

        public static GCsModel GetModelFromContent(string projectName, GContent con)
        {
            GCsModel model = new GCsModel();

            string action = string.Empty;
            GCsControllerModel ctr = null;

            foreach (var tree in con.trees)
            {
                if (tree.loadurl.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, tree.loadurl, out action);
                    ctr.LoadTreeActions.Add(action);
                }

                if (tree.saveurl.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, tree.saveurl, out action);
                    ctr.SaveActions.Add(action);
                }

                if (tree.delurl.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, tree.delurl, out action);
                    ctr.DeleteActions.Add(action);
                }
            }

            foreach (var table in con.tables)
            {
                if (table.transferurl != null && table.transferurl.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, table.transferurl, out action);
                    ctr.SaveActions.Add(action);
                }

                if (table.data.url.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, table.data.url, out action);
                    ctr.LoadTableActions.Add(action);
                }

                foreach (var header in table.data.headers)
                {
                    if (header.actionbtns.Count > 0)
                    {
                        foreach (var btn in header.actionbtns)
                        {
                            if (btn.type.Equals("edit"))
                            {
                                if (btn.action.Length > 0)
                                {
                                    ctr = GetControllerByACA(model, projectName, btn.action, out action);
                                    ctr.SaveActions.Add(action);
                                }
                            }
                            else if (btn.type.Equals("delete"))
                            {
                                if (btn.action.Length > 0)
                                {
                                    ctr = GetControllerByACA(model, projectName, btn.action, out action);
                                    ctr.DeleteActions.Add(action);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var json in con.jsons)
            {
                List<string> jFields = new List<string>();
                foreach (var d in json.data)
                {
                    if (d.c[0].fld.Length > 0)
                    {
                        jFields.Add(d.c[0].fld);
                    }
                }
                if (json.url.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, json.url, out action);
                    ctr.LoadJsonActions.Add(action, jFields);
                }
            }

            foreach (var fm in con.forms)
            {
                if (fm.disabledurl.Length > 0)
                {
                    ctr = GetControllerByACA(model, projectName, fm.disabledurl, out action);
                    ctr.LoadIntAction.Add(action);
                }
                foreach (var btn in fm.btns)
                {
                    if (btn.action.Length > 0)
                    {
                        ctr = GetControllerByACA(model, projectName, btn.action, out action);
                        ctr.SaveActions.Add(action);
                    }
                }
            }

            return model;
        }
    }

    public class GCsControllerModel
    {
        public GCsControllerModel()
        {
            LoadIntAction = new List<string>();
            LoadTreeActions = new List<string>();
            LoadTableActions = new List<string>();
            LoadJsonActions = new Dictionary<string, List<string>>();

            SaveActions = new List<string>();
            DeleteActions = new List<string>();
        }

        public string ProjectName { get; set; }
        public string Name { get; set; }

        public List<string> LoadIntAction { get; set; }
        public List<string> LoadTreeActions { get; set; }
        public List<string> LoadTableActions { get; set; }
        public Dictionary<string, List<string>> LoadJsonActions { get; set; }
        public List<string> SaveActions { get; set; }
        public List<string> DeleteActions { get; set; }
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
