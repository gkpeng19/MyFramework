using WinTemplate.UControl;
namespace WinTemplate
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tabControl = new WinTemplate.UControl.KryptonTabControl();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.fileTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.solutionCMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.cMItemAddProject = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.projectCMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.cMItemAddFolder = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.cMItemAddFile = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.folderCMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.cMItemAddFile2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.treeImgList = new System.Windows.Forms.ImageList(this.components);
            this.openSolutionDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileCMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems4 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.cMItemGenerateHtml = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.sluFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sbtnAddSulution = new System.Windows.Forms.ToolStripButton();
            this.sbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.sbtnSave = new System.Windows.Forms.ToolStripButton();
            this.sbtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kryptonSplitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 437);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(784, 462);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonPanel1);
            this.kryptonSplitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonSplitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(784, 437);
            this.kryptonSplitContainer1.SplitterDistance = 600;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tabControl);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 6);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 431);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(600, 431);
            this.tabControl.TabIndex = 0;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockInactive;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 6);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.fileTree);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(179, 431);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "解决方案资源管理器";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // fileTree
            // 
            this.fileTree.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlStandalone;
            this.fileTree.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.InputControlStandalone;
            this.fileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTree.ItemHeight = 21;
            this.fileTree.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.ListItem;
            this.fileTree.Location = new System.Drawing.Point(0, 0);
            this.fileTree.Name = "fileTree";
            this.fileTree.Size = new System.Drawing.Size(177, 407);
            this.fileTree.TabIndex = 0;
            // 
            // solutionCMenu
            // 
            this.solutionCMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.cMItemAddProject});
            // 
            // cMItemAddProject
            // 
            this.cMItemAddProject.Text = "新建项目";
            // 
            // projectCMenu
            // 
            this.projectCMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems2});
            // 
            // kryptonContextMenuItems2
            // 
            this.kryptonContextMenuItems2.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.cMItemAddFolder,
            this.cMItemAddFile});
            // 
            // cMItemAddFolder
            // 
            this.cMItemAddFolder.Text = "新建文件夹";
            // 
            // cMItemAddFile
            // 
            this.cMItemAddFile.Text = "新建文件";
            // 
            // folderCMenu
            // 
            this.folderCMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems3});
            // 
            // kryptonContextMenuItems3
            // 
            this.kryptonContextMenuItems3.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.cMItemAddFile2});
            // 
            // cMItemAddFile2
            // 
            this.cMItemAddFile2.Text = "新建文件";
            // 
            // treeImgList
            // 
            this.treeImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImgList.ImageStream")));
            this.treeImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImgList.Images.SetKeyName(0, "solution.png");
            this.treeImgList.Images.SetKeyName(1, "project.png");
            this.treeImgList.Images.SetKeyName(2, "folder.png");
            this.treeImgList.Images.SetKeyName(3, "document.png");
            // 
            // openSolutionDialog
            // 
            this.openSolutionDialog.FileName = "openSolutionDialog";
            // 
            // fileCMenu
            // 
            this.fileCMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems4});
            // 
            // kryptonContextMenuItems4
            // 
            this.kryptonContextMenuItems4.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.cMItemGenerateHtml});
            // 
            // cMItemGenerateHtml
            // 
            this.cMItemGenerateHtml.Text = "生成Html";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbtnAddSulution,
            this.sbtnOpen,
            this.sbtnSave,
            this.sbtnSaveAll});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(104, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // sbtnAddSulution
            // 
            this.sbtnAddSulution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbtnAddSulution.Image = global::WinTemplate.Properties.Resources.nnew;
            this.sbtnAddSulution.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnAddSulution.Name = "sbtnAddSulution";
            this.sbtnAddSulution.Size = new System.Drawing.Size(23, 22);
            this.sbtnAddSulution.Text = "新建解决方案";
            // 
            // sbtnOpen
            // 
            this.sbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbtnOpen.Image = global::WinTemplate.Properties.Resources.open;
            this.sbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnOpen.Name = "sbtnOpen";
            this.sbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.sbtnOpen.Text = "打开解决方案";
            // 
            // sbtnSave
            // 
            this.sbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbtnSave.Image = global::WinTemplate.Properties.Resources.save;
            this.sbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnSave.Name = "sbtnSave";
            this.sbtnSave.Size = new System.Drawing.Size(23, 22);
            this.sbtnSave.Text = "保存";
            // 
            // sbtnSaveAll
            // 
            this.sbtnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbtnSaveAll.Image = global::WinTemplate.Properties.Resources.saveall;
            this.sbtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnSaveAll.Name = "sbtnSaveAll";
            this.sbtnSaveAll.Size = new System.Drawing.Size(23, 22);
            this.sbtnSaveAll.Text = "保存全部";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "GTemplate";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView fileTree;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private KryptonTabControl tabControl;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu solutionCMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem cMItemAddProject;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu projectCMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem cMItemAddFolder;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem cMItemAddFile;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu folderCMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem cMItemAddFile2;
        private System.Windows.Forms.ImageList treeImgList;
        private System.Windows.Forms.OpenFileDialog openSolutionDialog;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu fileCMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems4;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem cMItemGenerateHtml;
        private System.Windows.Forms.FolderBrowserDialog sluFolderBrowserDialog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton sbtnAddSulution;
        private System.Windows.Forms.ToolStripButton sbtnOpen;
        private System.Windows.Forms.ToolStripButton sbtnSave;
        private System.Windows.Forms.ToolStripButton sbtnSaveAll;



    }
}

