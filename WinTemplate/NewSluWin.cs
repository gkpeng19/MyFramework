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
    public partial class NewSluWin : KryptonForm
    {
        public event Func<string, string, bool> OnSubmit = null;
        public NewSluWin()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (sluFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = sluFolderBrowserDialog.SelectedPath;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (OnSubmit != null)
            {
                var r = OnSubmit(txtName.Text.Trim(), txtPath.Text);
                if (r)
                {
                    this.Close();
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
