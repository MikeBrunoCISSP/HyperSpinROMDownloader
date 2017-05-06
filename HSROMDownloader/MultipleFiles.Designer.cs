namespace HSROMDownloader
{
    partial class MultipleFiles
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
            this.lstROMs = new System.Windows.Forms.ListBox();
            this.btnKeep = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstROMs
            // 
            this.lstROMs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstROMs.FormattingEnabled = true;
            this.lstROMs.ItemHeight = 31;
            this.lstROMs.Location = new System.Drawing.Point(22, 72);
            this.lstROMs.Name = "lstROMs";
            this.lstROMs.Size = new System.Drawing.Size(1236, 717);
            this.lstROMs.TabIndex = 0;
            this.lstROMs.SelectedIndexChanged += new System.EventHandler(this.lstROMs_SelectedIndexChanged);
            // 
            // btnKeep
            // 
            this.btnKeep.Enabled = false;
            this.btnKeep.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeep.Location = new System.Drawing.Point(1051, 825);
            this.btnKeep.Name = "btnKeep";
            this.btnKeep.Size = new System.Drawing.Size(207, 47);
            this.btnKeep.TabIndex = 1;
            this.btnKeep.Text = "Keep Selected";
            this.btnKeep.UseVisualStyleBackColor = true;
            this.btnKeep.Click += new System.EventHandler(this.btnKeep_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1016, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "This archive contains multiple ROMs.  Please select the one you would like to kee" +
    "p.";
            // 
            // MultipleFiles
            // 
            this.AcceptButton = this.btnKeep;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 895);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnKeep);
            this.Controls.Add(this.lstROMs);
            this.Name = "MultipleFiles";
            this.Text = "Select ROM - Brudog\'s HyperSpin ROM Hunter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstROMs;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.Label label1;
    }
}