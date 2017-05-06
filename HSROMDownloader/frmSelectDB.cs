using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace HSROMDownloader
{
    public partial class frmSelectDB : Form
    {
        public static string _7zip;
        public frmSelectDB()
        {
            InitializeComponent();
            System.Drawing.Icon ico = HSROMDownloader.Properties.Resources.bdrh;
            this.Icon = ico;


            if (!File.Exists(ConfigurationManager.AppSettings["7zip"]))
            {
                /*
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Please locate 7zip executable";
                ofd.Filter = "7zip|7z.exe";
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                    _7zip = ofd.FileName;
                else
                    Application.Exit();
                */
                _7zip = string.Empty;
            }
            else
                _7zip = ConfigurationManager.AppSettings["7zip"];
        }

        private void btnBroweDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dbSelector = new OpenFileDialog();
            dbSelector.Filter = "HyperSpin Database|*.xml";
            dbSelector.InitialDirectory = ConfigurationManager.AppSettings["HyperSpinDB_Path"];
            DialogResult result = dbSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDBPath.Text = dbSelector.FileName;
                checkForm();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ROMDirSelector = new FolderBrowserDialog();
            ROMDirSelector.SelectedPath = ConfigurationManager.AppSettings["ROM_Directory"];
            DialogResult result = ROMDirSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtROMDir.Text = ROMDirSelector.SelectedPath;
                checkForm();
            }
        }

        private void checkForm()
        {
            if (txtDBPath.Text == String.Empty || txtROMDir.Text == String.Empty)
                btnDBOK.Enabled = false;
            else
                btnDBOK.Enabled = true;
        }

        private void btnDBOK_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDownload app = new frmDownload(txtDBPath.Text, txtROMDir.Text);
            app.ShowDialog();
            this.Close();
        }
    }
}
