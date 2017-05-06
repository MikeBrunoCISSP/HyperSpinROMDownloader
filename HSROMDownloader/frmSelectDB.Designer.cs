namespace HSROMDownloader
{
    partial class frmSelectDB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.btnBroweDB = new System.Windows.Forms.Button();
            this.btnDBOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnROMDir = new System.Windows.Forms.Button();
            this.txtROMDir = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDBPath
            // 
            this.txtDBPath.Cursor = System.Windows.Forms.Cursors.No;
            this.txtDBPath.Location = new System.Drawing.Point(12, 59);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.ReadOnly = true;
            this.txtDBPath.Size = new System.Drawing.Size(685, 31);
            this.txtDBPath.TabIndex = 20;
            // 
            // btnBroweDB
            // 
            this.btnBroweDB.Location = new System.Drawing.Point(718, 53);
            this.btnBroweDB.Name = "btnBroweDB";
            this.btnBroweDB.Size = new System.Drawing.Size(120, 58);
            this.btnBroweDB.TabIndex = 0;
            this.btnBroweDB.Text = "Browse";
            this.btnBroweDB.UseVisualStyleBackColor = true;
            this.btnBroweDB.Click += new System.EventHandler(this.btnBroweDB_Click);
            // 
            // btnDBOK
            // 
            this.btnDBOK.Enabled = false;
            this.btnDBOK.Location = new System.Drawing.Point(718, 233);
            this.btnDBOK.Name = "btnDBOK";
            this.btnDBOK.Size = new System.Drawing.Size(120, 57);
            this.btnDBOK.TabIndex = 2;
            this.btnDBOK.Text = "OK";
            this.btnDBOK.UseVisualStyleBackColor = true;
            this.btnDBOK.Click += new System.EventHandler(this.btnDBOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select HyperList:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select ROM Directory:";
            // 
            // btnROMDir
            // 
            this.btnROMDir.Location = new System.Drawing.Point(718, 141);
            this.btnROMDir.Name = "btnROMDir";
            this.btnROMDir.Size = new System.Drawing.Size(120, 58);
            this.btnROMDir.TabIndex = 1;
            this.btnROMDir.Text = "Browse";
            this.btnROMDir.UseVisualStyleBackColor = true;
            this.btnROMDir.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtROMDir
            // 
            this.txtROMDir.Cursor = System.Windows.Forms.Cursors.No;
            this.txtROMDir.Location = new System.Drawing.Point(12, 147);
            this.txtROMDir.Name = "txtROMDir";
            this.txtROMDir.ReadOnly = true;
            this.txtROMDir.Size = new System.Drawing.Size(685, 31);
            this.txtROMDir.TabIndex = 4;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F);
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(11, 269);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 33);
            this.lblError.TabIndex = 7;
            // 
            // frmSelectDB
            // 
            this.AcceptButton = this.btnDBOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 311);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnROMDir);
            this.Controls.Add(this.txtROMDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDBOK);
            this.Controls.Add(this.btnBroweDB);
            this.Controls.Add(this.txtDBPath);
            this.MaximizeBox = false;
            this.Name = "frmSelectDB";
            this.Text = "Brudog\'s ROM Hunter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Button btnBroweDB;
        private System.Windows.Forms.Button btnDBOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnROMDir;
        private System.Windows.Forms.TextBox txtROMDir;
        private System.Windows.Forms.Label lblError;
    }
}

