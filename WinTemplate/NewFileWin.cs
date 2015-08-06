using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTemplate
{
    public partial class NewFileWin : KryptonForm
    {
        string _title = string.Empty;
        public NewFileWin()
        {
            InitializeComponent();
            this.Load += NewItemWin_Load;
        }

        public event Func<string, bool> OnSubmit = null;
        void NewItemWin_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += (ss, ee) =>
            {
                if (OnSubmit != null)
                {
                    var r = OnSubmit(txtName.Text.Trim());
                    if (r)
                    {
                        this.Close();
                    }
                }
            };
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                this.Text = value;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
