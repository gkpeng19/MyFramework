using ComponentFactory.Krypton.Toolkit;
using G.Util;
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

namespace WinTemplate
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Main : KryptonForm
    {
        public Main()
        {
            //GenerateHtml();
            //return;
            InitializeComponent();
            this.Load += Main_Load;
        }

        void Main_Load(object sender, EventArgs e)
        {
            fileTree.ImageList = treeImgList;

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

                            var tab = (currNode.Tag as GFile).Tab;
                            tabControl.SelectTab(tab);
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

            #endregion
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

        public void SaveToFile(string json)
        {
            var sw = File.CreateText(tabControl.SelectedTab.FPath + ".json");
            sw.Write(json);
            sw.Close();
        }

        void GenerateHtml()
        {
            string htmlpath = System.Environment.CurrentDirectory + @"\template\content.html";
            string tablepath = System.Environment.CurrentDirectory + @"\template\table.html";
            var html = System.IO.File.ReadAllText(htmlpath, Encoding.GetEncoding("GBK"));
            var table = System.IO.File.ReadAllText(tablepath, Encoding.GetEncoding("GBK"));

            Razor.Compile(table, "tables");

            var data = JSON.JsonBack<GContent>(File.ReadAllText(@"D:\GTemplateDemo\SSS\PPP\info.json"));
            var result = Razor.Parse(html, data);
        }

        private void ShowTreeRightMenu(Control c, KryptonContextMenu kcm)
        {
            kcm.Show(c.RectangleToScreen(c.ClientRectangle), KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below);
        }

        private void sbtnAddSulution_Click(object sender, EventArgs e)
        {
            NewFileWin win = new NewFileWin() { Text = "新建解决方案" };
            win.OnSubmit += (name) =>
                {
                    AddSolution(name, @"D:\GTemplateDemo");
                    return true;
                };
            win.ShowDialog(this);
        }
    }
}
