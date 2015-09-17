namespace WinTemplate.UControl
{
    partial class KryptonTabControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tabBody = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tabHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.btnClose = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.btnTabs = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabBody)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tabBody);
            this.kryptonPanel1.Controls.Add(this.tabHeader);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(346, 150);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tabBody
            // 
            this.tabBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBody.Location = new System.Drawing.Point(0, 21);
            this.tabBody.Name = "tabBody";
            this.tabBody.Size = new System.Drawing.Size(346, 129);
            this.tabBody.TabIndex = 2;
            // 
            // tabHeader
            // 
            this.tabHeader.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.btnClose,
            this.btnTabs});
            this.tabHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabHeader.Location = new System.Drawing.Point(0, 0);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Size = new System.Drawing.Size(346, 21);
            this.tabHeader.TabIndex = 1;
            this.tabHeader.Values.Description = "";
            this.tabHeader.Values.Heading = "";
            this.tabHeader.Values.Image = null;
            // 
            // btnClose
            // 
            this.btnClose.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Close;
            this.btnClose.UniqueName = "F1BD0082D9AF43A9B98A79549B0D385A";
            // 
            // btnTabs
            // 
            this.btnTabs.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.DropDown;
            this.btnTabs.UniqueName = "0E9D19015D7F44A1D2BA571CD307D0DC";
            this.btnTabs.Visible = false;
            // 
            // KryptonTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "KryptonTabControl";
            this.Size = new System.Drawing.Size(346, 150);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabBody)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader tabHeader;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny btnClose;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny btnTabs;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel tabBody;
    }
}
