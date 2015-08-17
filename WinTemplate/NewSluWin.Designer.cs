namespace WinTemplate
{
    partial class NewSluWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSluWin));
            this.layerContainer = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnBrowser = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtPath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnCancle = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.sluFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.layerContainer)).BeginInit();
            this.layerContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layerContainer
            // 
            this.layerContainer.Controls.Add(this.btnBrowser);
            this.layerContainer.Controls.Add(this.txtPath);
            this.layerContainer.Controls.Add(this.kryptonLabel2);
            this.layerContainer.Controls.Add(this.txtName);
            this.layerContainer.Controls.Add(this.kryptonLabel1);
            this.layerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerContainer.Location = new System.Drawing.Point(0, 0);
            this.layerContainer.Name = "layerContainer";
            this.layerContainer.Size = new System.Drawing.Size(361, 84);
            this.layerContainer.TabIndex = 7;
            // 
            // btnBrowser
            // 
            this.btnBrowser.AutoSize = true;
            this.btnBrowser.Location = new System.Drawing.Point(272, 45);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(30, 24);
            this.btnBrowser.TabIndex = 4;
            this.btnBrowser.Values.Text = "...";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(85, 48);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(185, 20);
            this.txtPath.TabIndex = 3;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(33, 48);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "路径：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(85, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(217, 20);
            this.txtName.TabIndex = 1;
            this.txtName.WordWrap = false;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(33, 14);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "名称：";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.btnCancle);
            this.kryptonPanel2.Controls.Add(this.btnSubmit);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 84);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.kryptonPanel2.Size = new System.Drawing.Size(361, 30);
            this.kryptonPanel2.TabIndex = 6;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.Location = new System.Drawing.Point(287, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(70, 25);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Values.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(214, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Values.Text = "确定";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // NewSluWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 114);
            this.Controls.Add(this.layerContainer);
            this.Controls.Add(this.kryptonPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewSluWin";
            this.Text = "新建解决方案";
            ((System.ComponentModel.ISupportInitialize)(this.layerContainer)).EndInit();
            this.layerContainer.ResumeLayout(false);
            this.layerContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ComponentFactory.Krypton.Toolkit.KryptonPanel layerContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancle;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowser;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.FolderBrowserDialog sluFolderBrowserDialog;
    }
}