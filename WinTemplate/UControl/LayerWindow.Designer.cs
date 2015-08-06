namespace WinTemplate.UControl
{
    partial class LayerWindow
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
            this.layerHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.layerContainer = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnCancle = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.layerHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerHeader.Panel)).BeginInit();
            this.layerHeader.Panel.SuspendLayout();
            this.layerHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerContainer)).BeginInit();
            this.layerContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layerHeader
            // 
            this.layerHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerHeader.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.layerHeader.HeaderVisibleSecondary = false;
            this.layerHeader.Location = new System.Drawing.Point(0, 0);
            this.layerHeader.Name = "layerHeader";
            // 
            // layerHeader.Panel
            // 
            this.layerHeader.Panel.Controls.Add(this.layerContainer);
            this.layerHeader.Panel.Controls.Add(this.kryptonPanel2);
            this.layerHeader.Size = new System.Drawing.Size(342, 150);
            this.layerHeader.TabIndex = 0;
            this.layerHeader.ValuesPrimary.Heading = "Title";
            // 
            // layerContainer
            // 
            this.layerContainer.Controls.Add(this.txtName);
            this.layerContainer.Controls.Add(this.kryptonLabel1);
            this.layerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerContainer.Location = new System.Drawing.Point(0, 0);
            this.layerContainer.Name = "layerContainer";
            this.layerContainer.Size = new System.Drawing.Size(340, 97);
            this.layerContainer.TabIndex = 3;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.btnCancle);
            this.kryptonPanel2.Controls.Add(this.btnSubmit);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 97);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.kryptonPanel2.Size = new System.Drawing.Size(340, 30);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.Location = new System.Drawing.Point(266, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(70, 25);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Values.Text = "取消";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(193, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Values.Text = "确定";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(36, 43);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(90, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(207, 20);
            this.txtName.TabIndex = 1;
            this.txtName.WordWrap = false;
            // 
            // LayerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layerHeader);
            this.Name = "LayerWindow";
            this.Size = new System.Drawing.Size(342, 150);
            ((System.ComponentModel.ISupportInitialize)(this.layerHeader.Panel)).EndInit();
            this.layerHeader.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layerHeader)).EndInit();
            this.layerHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layerContainer)).EndInit();
            this.layerContainer.ResumeLayout(false);
            this.layerContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup layerHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancle;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        public ComponentFactory.Krypton.Toolkit.KryptonPanel layerContainer;

    }
}
