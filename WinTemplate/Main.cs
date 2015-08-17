using ComponentFactory.Krypton.Toolkit;
using G.Util.Extension;
using G.Util.Tool;
using mshtml;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinTemplate.UControl;

namespace WinTemplate
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Main : KryptonForm
    {
        public Main()
        {
            InitializeComponent();
            this.Load += Main_Load;
            this.FormClosed += Main_FormClosed;
        }

        void Main_Load(object sender, EventArgs e)
        {
            fileTree.ImageList = treeImgList;

            #region 组合键保存

            this.KeyPreview = true;
            this.KeyDown += (ss, ee) =>
                {
                    if (ee.Control && ee.KeyCode == Keys.S)
                    {
                        sbtnSave_Click(null, null);
                    }
                };

            #endregion

            #region 树右键菜单

            fileTree.TreeView.MouseClick += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Right)
                {
                    Point ClickPoint = new Point(ee.X, ee.Y);
                    TreeNode currentNode = fileTree.GetNodeAt(ClickPoint);
                    if (currentNode != null)//判断你点的是不是一个节点
                    {
                        fileTree.SelectedNode = currentNode;
                        if (currentNode.Tag is GSolution)
                        {
                            ShowTreeRightMenu(currentNode.TreeView, solutionCMenu);
                        }
                        else if (currentNode.Tag is GProject)
                        {
                            ShowTreeRightMenu(currentNode.TreeView, projectCMenu);
                        }
                        else if (currentNode.Tag is GFolder)
                        {
                            ShowTreeRightMenu(currentNode.TreeView, folderCMenu);
                        }
                        else if (currentNode.Tag is GFile)
                        {
                            ShowTreeRightMenu(currentNode.TreeView, fileCMenu);
                        }
                    }
                }
            };

            #endregion

            #region 树双击选中节点

            fileTree.TreeView.MouseDoubleClick += (ss, ee) =>
                {
                    if (ee.Button == MouseButtons.Left)
                    {
                        var currNode = fileTree.GetNodeAt(ee.X, ee.Y);
                        if (currNode != null && currNode.Tag is GFile)
                        {
                            fileTree.SelectedNode = currNode;

                            var gFile = currNode.Tag as GFile;
                            var tab = gFile.Tab;
                            if (tab != null)
                            {
                                tabControl.SelectTab(tab);
                            }
                            else
                            {
                                tab = tabControl.AddNewTab(gFile.Text, gFile.SPath);
                                WebBrowser browser = new WebBrowser();
                                browser.Url = new Uri("http://localhost/Default.html");
                                browser.ObjectForScripting = this;
                                browser.Dock = DockStyle.Fill;
                                tab.Container.Controls.Add(browser);
                                browser.Document.InvokeScript("InitUI", new string[] { File.ReadAllText(gFile.SPath + ".json") });
                            }
                        }
                    }
                };

            #endregion

            #region 右键菜单命令

            cMItemAddProject.Click += (ss, ee) =>
                {
                    NewFileWin win = new NewFileWin() { Text = "新建项目" };
                    win.OnSubmit += (name) =>
                        {
                            AddProject(fileTree.SelectedNode, name);
                            return true;
                        };
                    win.ShowDialog(this);
                };

            cMItemAddFolder.Click += (ss, ee) =>
                {
                    NewFileWin win = new NewFileWin() { Text = "新建文件夹" };
                    win.OnSubmit += (name) =>
                    {
                        AddFolder(fileTree.SelectedNode, name);
                        return true;
                    };
                    win.ShowDialog(this);
                };

            cMItemAddFile.Click += (ss, ee) =>
                {
                    NewFileWin win = new NewFileWin() { Text = "新建文件" };
                    win.OnSubmit += (name) =>
                    {
                        AddFile(fileTree.SelectedNode, name);
                        return true;
                    };
                    win.ShowDialog(this);
                };

            cMItemAddFile2.Click += (ss, ee) =>
            {
                NewFileWin win = new NewFileWin() { Text = "新建文件" };
                win.OnSubmit += (name) =>
                {
                    AddFile(fileTree.SelectedNode, name);
                    return true;
                };
                win.ShowDialog(this);
            };

            cMItemGenerateHtml.Click += (ss, ee) =>
                {
                    GenerateHtml((fileTree.SelectedNode.Tag as GFile).SPath + ".json");
                };

            #endregion
        }

        void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_solution != null)
            {
                XmlExtension.SaveToXml(_solution, _solution.SPath + "\\" + _solution.Text + ".xml");
            }
        }

        GSolution _solution = null;
        void AddSolution(string name, string path)
        {
            path = path + "\\" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _solution = new GSolution(path) { Text = name };
            var node = new TreeNode(name);
            node.ImageIndex = (int)ImageListEnum.Solution;
            node.SelectedImageIndex = node.ImageIndex;
            node.Tag = _solution;
            fileTree.Nodes.Add(node);
        }

        void AddProject(TreeNode snode, string name)
        {
            var path = (snode.Tag as GSolution).SPath + "\\" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var project = new GProject(path) { Text = name };
            _solution.Projects.Add(project);

            var node = new TreeNode(name) { Tag = project };
            node.ImageIndex = (int)ImageListEnum.Project;
            node.SelectedImageIndex = node.ImageIndex;
            snode.Nodes.Add(node);
            snode.Toggle();
            fileTree.SelectedNode = node;
        }

        void AddFolder(TreeNode snode, string name)
        {
            var path = (snode.Tag as GProject).SPath + "\\" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var folder = new GFolder(path) { Text = name };
            (snode.Tag as GProject).Folders.Add(folder);

            var node = new TreeNode(name) { Tag = folder };
            node.ImageIndex = (int)ImageListEnum.Folder;
            node.SelectedImageIndex = node.ImageIndex;
            snode.Nodes.Add(node);
            snode.Toggle();
            fileTree.SelectedNode = node;
        }

        void AddFile(TreeNode snode, string name)
        {
            var path = string.Empty;
            if (snode.Tag is GProject)
            {
                path = (snode.Tag as GProject).SPath + "\\" + name;
            }
            else
            {
                path = (snode.Tag as GFolder).SPath + "\\" + name;
            }

            var tab = tabControl.AddNewTab(name, path);
            WebBrowser browser = new WebBrowser();
            browser.Url = new Uri("http://localhost/Default.html");
            browser.ObjectForScripting = this;
            browser.Dock = DockStyle.Fill;
            tab.Container.Controls.Add(browser);

            var file = new GFile(path) { Text = name, Tab = tab };
            if (snode.Tag is GProject)
            {
                (snode.Tag as GProject).Files.Add(file);
            }
            else
            {
                (snode.Tag as GFolder).Files.Add(file);
            }

            var node = new TreeNode(name) { Tag = file };
            node.ImageIndex = (int)ImageListEnum.File;
            node.SelectedImageIndex = node.ImageIndex;
            snode.Nodes.Add(node);
            snode.Toggle();
            fileTree.SelectedNode = node;
        }

        KryptonTab _toSaveTab = null;
        public void SaveToFile(string json)
        {
            if (_toSaveTab == null)
            {
                _toSaveTab = tabControl.SelectedTab;
            }
            var sw = File.CreateText(_toSaveTab.FPath + ".json");

            _toSaveTab = null;

            sw.Write(json);
            sw.Close();
        }

        void GenerateHtml(string filePath)
        {
            string htmlpath = System.Environment.CurrentDirectory + @"\template\content.html";
            string tablepath = System.Environment.CurrentDirectory + @"\template\table.html";
            var html = System.IO.File.ReadAllText(htmlpath, Encoding.GetEncoding("GBK"));
            var table = System.IO.File.ReadAllText(tablepath, Encoding.GetEncoding("GBK"));

            Razor.Compile(table, "tables");

            var data = JSON.JsonBack<GContent>(File.ReadAllText(filePath));
            var result = Razor.Parse(html, data);
        }

        private void ShowTreeRightMenu(Control c, KryptonContextMenu kcm)
        {
            kcm.Show(c.RectangleToScreen(c.ClientRectangle), KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below);
        }

        private TreeNode AddTreeNode(TreeNode pNode, GTreeNode entity)
        {
            var node = new TreeNode(entity.Text) { Tag = entity };
            var nType = entity.Type;
            ImageListEnum imgIndex = ImageListEnum.File;
            switch (nType)
            {
                case TreeNodeEnum.Folder:
                    imgIndex = ImageListEnum.Folder;
                    break;
                case TreeNodeEnum.Project:
                    imgIndex = ImageListEnum.Project;
                    break;
                case TreeNodeEnum.Solution:
                    imgIndex = ImageListEnum.Solution;
                    break;
            }

            node.ImageIndex = (int)imgIndex;
            node.SelectedImageIndex = node.ImageIndex;

            if (pNode != null)
            {
                pNode.Nodes.Add(node);
            }
            else
            {
                fileTree.Nodes.Add(node);
            }
            return node;
        }

        #region 工具栏按钮事件

        private void sbtnAddSulution_Click(object sender, EventArgs e)
        {
            NewSluWin win = new NewSluWin();
            win.OnSubmit += (name, path) =>
            {
                AddSolution(name, path);
                return true;
            };
            win.ShowDialog(this);
        }

        private void sbtnOpen_Click(object sender, EventArgs e)
        {
            openSolutionDialog.Filter = "Xml文件|*.xml";
            if (openSolutionDialog.ShowDialog() == DialogResult.OK)
            {
                _solution = XmlExtension.ReadToEntity<GSolution>(openSolutionDialog.FileName);
                var snode = AddTreeNode(null, _solution);
                foreach (var p in _solution.Projects)
                {
                    var pnode = AddTreeNode(snode, p);
                    foreach (var f in p.Folders)
                    {
                        var fnode = AddTreeNode(pnode, f);
                        foreach (var fn in f.Files)
                        {
                            AddTreeNode(fnode, fn);
                        }
                    }
                    foreach (var fn in p.Files)
                    {
                        AddTreeNode(pnode, fn);
                    }
                }
            }
        }

        protected void sbtnSave_Click(object sender, System.EventArgs e)
        {
            if (tabControl.SelectedTab != null)
            {
                var browser = tabControl.SelectedTab.Container.Controls[0] as WebBrowser;
                browser.Document.InvokeScript("Save");
            }
        }

        private void sbtnSaveAll_Click(object sender, EventArgs e)
        {
            var index = 0;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (ss, ee) =>
            {
                if (index + 1 == tabControl.Tabs.Count)
                {
                    timer.Stop();
                }

                _toSaveTab = tabControl.Tabs[index];
                var browser = _toSaveTab.Container.Controls[0] as WebBrowser;
                ++index;

                browser.Document.InvokeScript("Save");
            };

            timer.Start();
        }

        #endregion
    }
}
