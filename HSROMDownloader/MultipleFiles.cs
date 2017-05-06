using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSROMDownloader
{
    public partial class MultipleFiles : Form
    {
        public string selectedROM;

        public MultipleFiles(List<string> ROMs)
        {
            InitializeComponent();
            System.Drawing.Icon ico = HSROMDownloader.Properties.Resources.bdrh;
            this.Icon = ico;

            lstROMs.DataSource = ROMs;
        }

        private void lstROMs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnKeep.Enabled = true;
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            selectedROM = lstROMs.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}