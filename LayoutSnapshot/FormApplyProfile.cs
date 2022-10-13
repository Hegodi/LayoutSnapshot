using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutSnapshot
{
    public partial class FormApplyProfile : Form
    {
        List<LayoutSnapshot> mProfiles;
        FormEditProfiles mFormEditProfiles = null;
        public FormApplyProfile()
        {
            InitializeComponent();
            ReloadData();
        }

        private void ReloadData()
        {
            mProfiles = LayoutSnapshot.LoadData();
            comboBoxProfiles.Items.Clear(); 
            foreach (LayoutSnapshot profile in mProfiles)
            {
                comboBoxProfiles.Items.Add(profile.name);
            }
            if (comboBoxProfiles.Items.Count > 0)
            {
                comboBoxProfiles.SelectedIndex = 0;
            }
        }

        private void buttonEditProfiles_Click(object sender, EventArgs e)
        {
            if (mFormEditProfiles == null)
            {
                mFormEditProfiles = new FormEditProfiles();
                mFormEditProfiles.FormClosed += OnEditWindowClosed;
            }
            mFormEditProfiles.Show();
            mFormEditProfiles.BringToFront();
            this.Enabled = false;

        }

        void OnEditWindowClosed(object sender, EventArgs args)
        {
            mFormEditProfiles = null;
            ReloadData();
            this.Enabled = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex >= 0 && comboBoxProfiles.SelectedIndex < mProfiles.Count)
            {
                mProfiles[comboBoxProfiles.SelectedIndex].Apply();
            }
        }
    }
}
