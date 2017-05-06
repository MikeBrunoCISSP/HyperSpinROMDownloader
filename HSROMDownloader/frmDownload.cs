using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using System.IO.Compression;

namespace HSROMDownloader
{
    enum ExtractResult
    {
        EXTRACT_SUCCESS = 0,
        FILE_IS_ROM = 1,
        EXTRACT_FAIL = 2
    }

    public partial class frmDownload : Form
    {
        string hyperList,
               ROMDir,
               URL;

        static string _7zip = frmSelectDB._7zip;
        static bool use7zip;
        static string archiveExt = ".zip";

        BindingList<string> games = new BindingList<string>();

        public frmDownload(string hList, string Rdir)
        {
            InitializeComponent();
            System.Drawing.Icon ico = HSROMDownloader.Properties.Resources.bdrh;
            this.Icon = ico;

            hyperList = hList;

            use7zip = !string.IsNullOrEmpty(_7zip);
            if (use7zip)
                archiveExt = ".7z";

            if (Rdir[Rdir.Length - 1] != '\\')
                Rdir = Rdir + '\\';
            ROMDir = Rdir;

            populateGamesList();
        }

        void checkForm()
        {
            bool validURL;

            if (txtURL.Text == string.Empty)
            {
                btnDownload.Enabled = false;
                return;
            }
            if (txtExt.Text == string.Empty)
            {
                btnDownload.Enabled = false;
                return;
            }

            Uri uriResult;
            URL = txtURL.Text.ToUpper();
            if (URL.Length >= 7)
            {
                if (URL.Substring(0, 7) != "HTTP://")
                    URL = "http://" + URL;
            }
            validURL = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!validURL)
            {
                btnDownload.Enabled = false;
                return;
            }

            if (lstGames.SelectedIndex == -1)
                btnDownload.Enabled = false;
            else
                btnDownload.Enabled = true;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            List<string> gamesToDisplay = new List<string>();
            Regex r = new Regex(txtFilter.Text, RegexOptions.IgnoreCase);
            Match m;

            foreach (string game in games)
            {
                m = r.Match(game);
                if (m.Success)
                    gamesToDisplay.Add(game);
            }

            lstGames.DataSource = gamesToDisplay;
            lstGames.Update();
            lstGames.SelectedIndex = -1;
        }

        void populateGamesList()
        {
            string currentRecord,
                   game;
            TextReader inStream = new StreamReader(hyperList);
            while (inStream.Peek() != -1)
            {
                currentRecord = inStream.ReadLine();
                if (currentRecord.IndexOf("<game") != -1)
                {
                    game = currentRecord.Split(new string[] { "name=" }, StringSplitOptions.None)[1].Split('\"')[1].Replace("&apos;", "\'").Replace("&amp;", "&");
                    games.Add(game);
                }
            }

            lstGames.DataSource = games;
            lstGames.Update();
        }

        ExtractResponse extractArchive(string archive)
        {
            if (use7zip)
                return extractArchive7zip(archive);
            else
                return extractArchiveWin(archive);
        }

        ExtractResponse extractArchive7zip(string archiveFile)
        {
            string[] lines;
            //int possibleROMs = 0;
            string command = _7zip + " -y x \"" + archiveFile + "\"";
            string output = execCmd(command);
            string ROM = string.Empty;
            string URLextension;
            //string extractedROM = string.Empty;
            List<string> ROMsInArchive = new List<string>();

            if (output.IndexOf("Error") != -1)
            {
                URLextension = URL.Substring(URL.Length - txtExt.Text.Length, txtExt.Text.Length);
                if (string.Equals(txtExt.Text, URLextension, StringComparison.OrdinalIgnoreCase))
                    return new ExtractResponse(ExtractResult.FILE_IS_ROM, string.Empty);
                else
                    return new ExtractResponse();
            }
            else
            {
                lines = output.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.IndexOf("Extracting") != -1)
                    {
                        ROM = line.Replace("Extracting  ", string.Empty);
                        if ((ROM.IndexOf(".txt") != -1) || (ROM.IndexOf(".htm") != -1) || (ROM.IndexOf(".html") != -1))
                            File.Delete(ROM);
                        else
                            ROMsInArchive.Add(ROM);
                    }
                }

                switch (ROMsInArchive.Count)
                {
                    case 0:
                        return new ExtractResponse();
                    case 1:
                        return new ExtractResponse(ExtractResult.EXTRACT_SUCCESS, ROM);
                    default:
                        using (MultipleFiles mf = new MultipleFiles(ROMsInArchive))
                        {
                            mf.ShowDialog();
                            if (mf.DialogResult == DialogResult.OK)
                            {
                                ROM = mf.selectedROM;
                                foreach (string file in ROMsInArchive)
                                {
                                    if (!string.Equals(file, ROM))
                                        File.Delete(file);
                                }
                                return new ExtractResponse(ExtractResult.EXTRACT_SUCCESS, ROM);
                            }
                            else
                                return new ExtractResponse();
                        }
                }
            }
        }

        ExtractResponse extractArchiveWin(string archiveFile)
        {
            string absoluteFilePath = string.Empty;
            string ROM = string.Empty;
            string URLextension;
            List<string> ROMsInArchive = new List<string>();

            try
            {
                using (ZipArchive oArchive = ZipFile.OpenRead(archiveFile))
                {
                    foreach (ZipArchiveEntry entry in oArchive.Entries)
                    {
                        if (entry.FullName.EndsWith(txtExt.Text))
                        {
                            absoluteFilePath = Path.Combine(ROMDir, entry.FullName);
                            entry.ExtractToFile(absoluteFilePath);
                            ROMsInArchive.Add(entry.FullName);
                            ROM = entry.FullName;
                        }
                    }
                }

                switch (ROMsInArchive.Count)
                {
                    case 0:
                        return new ExtractResponse();
                    case 1:
                        return new ExtractResponse(ExtractResult.FILE_IS_ROM, ROM);
                    default:
                        using (MultipleFiles mf = new MultipleFiles(ROMsInArchive))
                        {
                            mf.ShowDialog();
                            if (mf.DialogResult == DialogResult.OK)
                            {
                                ROM = mf.selectedROM;
                                foreach (string file in ROMsInArchive)
                                {
                                    if (!string.Equals(file, ROM))
                                        File.Delete(file);
                                }
                                return new ExtractResponse(ExtractResult.EXTRACT_SUCCESS, ROM);
                            }
                            else
                                return new ExtractResponse();
                        }
                }
            }
            catch (Exception e)
            {
                URLextension = URL.Substring(URL.Length - txtExt.Text.Length, txtExt.Text.Length);
                if (string.Equals(URLextension, txtExt.Text, StringComparison.OrdinalIgnoreCase))
                    return new ExtractResponse(ExtractResult.FILE_IS_ROM, string.Empty);
                else
                {
                    MessageBox.Show("An exception occurred when attempting to extract the ROM:\r\n\r\n" + e.ToString() + "\r\n\r\nIf the specified URL points to a file of any format other than \".zip\", try using 7zip.  For more information, visit http://www.7-zip.org/", "Brudog's ROM Hunter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new ExtractResponse();
                }
            }
        }

        bool compressROM(string tmpROMfile, string destROMFile, string destArchive)
        {
            if (use7zip)
                return compressROM7zip(tmpROMfile, destROMFile, destArchive);
            else
                return compressROMWin(tmpROMfile, destROMFile, destArchive);
        }

        bool compressROM7zip(string tmpROMfile, string destROMFile, string destArchive)
        {
            File.Move(tmpROMfile, destROMFile);
            if (File.Exists(destROMFile))
            {
                string command = _7zip + " a \"" + destArchive + "\" \"" + destROMFile + "\"";
                string output = execCmd(command);

                if (output.IndexOf("Error") != -1)
                    return false;

                return File.Exists(destArchive);
            }
            else
                return false;
        }

        bool compressROMWin(string tmpROMfile, string destROMPath, string destArchive)
        {
            string destROMFile;
            string[] parts;

            File.Move(tmpROMfile, destROMPath);
            if (File.Exists(destROMPath))
            {
                parts = destROMPath.Split('\\');
                destROMFile = parts[parts.Length - 1];
                using (FileStream fs = new FileStream(destArchive, FileMode.Create))
                using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    arch.CreateEntryFromFile(destROMPath, destROMFile);
                }
                if (File.Exists(destArchive))
                {
                    bool found = false;
                    using (ZipArchive oArchive = ZipFile.OpenRead(destArchive))
                    {
                        foreach (ZipArchiveEntry entry in oArchive.Entries)
                            found = (entry.FullName == destROMFile);
                    }
                    return found;
                }
                else
                    return false;
            }
            else
                return false;
        }

        string execCmd(string command)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                return output;
            }

            catch (Exception e)
            {
                MessageBox.Show("An exception occurred when attempting to execute the command:\r\n\r\n" + e.ToString(), "HSROMDownloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bool tmpArchiveCreated = false;
            string tmpROMfile = string.Empty;
            string tmpArchive = ROMDir + "tmpArchive";

            string ROMExt = txtExt.Text;
            if (ROMExt[0] != '.')
                ROMExt = "." + ROMExt;
            string destROMFile = ROMDir + lstGames.GetItemText(lstGames.SelectedItem) + ROMExt;
            string destArchive = ROMDir + lstGames.GetItemText(lstGames.SelectedItem) + archiveExt;
            ExtractResponse response;

            lblStatus.Text = "Downloading ROM...";
            if (downloadROM(txtURL.Text, tmpArchive))
            {
                lblStatus.Text = "Attempting to extract ROM...";

                response = extractArchive(tmpArchive);
                switch (response.result)
                {
                    case ExtractResult.EXTRACT_SUCCESS:
                        tmpROMfile = response.fileName;
                        break;
                    case ExtractResult.FILE_IS_ROM:
                        tmpROMfile = tmpArchive;
                        break;
                    case ExtractResult.EXTRACT_FAIL:
                        lblStatus.Text = "Failed to extract ROM from archive.";
                        return;
                }

                if (File.Exists(destArchive))
                    lblStatus.Text = "You already have this ROM.";
                else
                {
                    lblStatus.Text = "Re-compressing ROM with new file name...";
                    if (compressROM(tmpROMfile, destROMFile, destArchive))
                    {
                        lblStatus.Text = "Success!";
                        txtURL.Text = "";
                    }
                    else
                        lblStatus.Text = "Failed to compress ROM.";
                }

                try
                {
                    File.Delete(tmpROMfile);
                    File.Delete(destROMFile);
                    if (tmpArchiveCreated)
                        File.Delete(tmpArchive);
                }
                catch (Exception) { }
            }
            else
                lblStatus.Text = "Failed to download ROM.";
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            checkForm();
        }

        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkForm();
        }

        private void txtExt_TextChanged(object sender, EventArgs e)
        {
            checkForm();
        }

        bool downloadROM(string URL, string fileName)
        {
            if (URL.Length <= 7 || URL.Substring(0, 7).ToUpper() != "HTTP://")
                URL = "http://" + URL;

            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(URL, fileName);
                return File.Exists(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occurred when attempting to download the ROM:\r\n\r\n" + e.ToString(), "HSROMDownloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

    class ExtractResponse
    {
        public ExtractResult result;
        public string fileName;

        public ExtractResponse(ExtractResult er, string name)
        {
            result = er;
            fileName = name;
        }

        public ExtractResponse()
        {
            result = ExtractResult.EXTRACT_FAIL;
            fileName = string.Empty;
        }
    }

}
