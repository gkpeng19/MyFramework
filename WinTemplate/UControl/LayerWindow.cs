using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTemplate.UControl
{
    public partial class LayerWindow : UserControl
    {
        string _title = string.Empty;
        public LayerWindow()
        {
            InitializeComponent();
            this.Load += LayerWindow_Load;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                layerHeader.ValuesPrimary.Heading = _title;
            }
        }

        public Action OnSubmit = null;
        public Action OnCancle = null;

        void LayerWindow_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += (ss, ee) =>
                {
                    if (OnSubmit != null)
                    {
                        OnSubmit();
                    }
                };
            btnCancle.Click += (ss, ee) =>
                {
                    if (OnCancle != null)
                    {
                        OnCancle();
                    }
                };
        }

        public Panel Panel
        {
            get { return this.layerContainer; }
        }
    }
}
