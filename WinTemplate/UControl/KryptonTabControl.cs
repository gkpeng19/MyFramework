using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace WinTemplate.UControl
{
    public partial class KryptonTabControl : UserControl
    {
        public KryptonTab SelectedTab { get; private set; }

        private List<KryptonTab> _allTabs = null;
        public KryptonTabControl()
        {
            InitializeComponent();
            _allTabs = new List<KryptonTab>();
        }

        public KryptonTab AddNewTab(string name,string fpath)
        {
            if (SelectedTab != null)
            {
                SelectedTab.Hide();
            }

            KryptonTab tab = new KryptonTab(name, fpath);
            tab.OnSelecting += () =>
                {
                    SelectedTab.Hide();

                    SelectedTab = tab;
                    SelectedTab.Show();
                };

            SelectedTab = tab;
            SelectedTab.Show();

            tabHeader.ButtonSpecs.Add(tab.TabTag);
            tabBody.Controls.Add(tab.Container);

            return tab;
        }

        public void SelectTab(KryptonTab tab)
        {
            if (tab != SelectedTab)
            {
                SelectedTab.Hide();

                SelectedTab = tab;
                SelectedTab.Show();
            }
        }

        public void CloseSelectedTab()
        {
            if (SelectedTab != null)
            {
                _allTabs.Remove(SelectedTab);
                tabHeader.ButtonSpecs.Remove(SelectedTab.TabTag);
                tabBody.Controls.Remove(SelectedTab.Container);
            }

            if (_allTabs.Count > 0)
            {
                SelectedTab = _allTabs[0];
            }
            SelectedTab.Show();
        }
    }

    public class KryptonTab
    {
        public event Action OnSelecting;
        public KryptonTab(string name,string fpath)
        {
            this.FPath = fpath;

            TabTag = new ButtonSpecAny()
            {
                Text = name,
                Edge = PaletteRelativeEdgeAlign.Near,
                Checked = ButtonCheckState.Checked,
                ExtraText = " *"
            };
            Container = new KryptonPanel();
            Container.Dock = DockStyle.Fill;

            TabTag.Click += (ss, ee) =>
                {
                    if (TabTag.Checked == ButtonCheckState.Unchecked)
                    {
                        TabTag.Checked = ButtonCheckState.Checked;
                    }
                    else
                    {
                        if (OnSelecting != null)
                        {
                            OnSelecting();
                        }
                    }
                };
        }

        public string FPath { get; private set; }

        public ButtonSpecAny TabTag { get; private set; }
        public KryptonPanel Container { get; private set; }

        public void Hide()
        {
            TabTag.Checked = ButtonCheckState.Unchecked;
            Container.Hide();
        }

        public void Show()
        {
            TabTag.Checked = ButtonCheckState.Checked;
            Container.Show();
        }
    }
}
