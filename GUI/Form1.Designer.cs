
namespace GUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.dstFolderTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.srcFolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.foldersGroup = new System.Windows.Forms.GroupBox();
            this.radioModeCompile = new System.Windows.Forms.RadioButton();
            this.radioModeDecompile = new System.Windows.Forms.RadioButton();
            this.operationGroup = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.foldersGroup.SuspendLayout();
            this.operationGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output Directory";
            // 
            // dstFolderTextBox
            // 
            this.dstFolderTextBox.Location = new System.Drawing.Point(22, 182);
            this.dstFolderTextBox.Name = "dstFolderTextBox";
            this.dstFolderTextBox.Size = new System.Drawing.Size(404, 26);
            this.dstFolderTextBox.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 212);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // srcFolderTextBox
            // 
            this.srcFolderTextBox.Location = new System.Drawing.Point(22, 62);
            this.srcFolderTextBox.Name = "srcFolderTextBox";
            this.srcFolderTextBox.Size = new System.Drawing.Size(404, 26);
            this.srcFolderTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Input Folder";
            // 
            // foldersGroup
            // 
            this.foldersGroup.Controls.Add(this.label2);
            this.foldersGroup.Controls.Add(this.label1);
            this.foldersGroup.Controls.Add(this.srcFolderTextBox);
            this.foldersGroup.Controls.Add(this.dstFolderTextBox);
            this.foldersGroup.Controls.Add(this.button2);
            this.foldersGroup.Controls.Add(this.button1);
            this.foldersGroup.Location = new System.Drawing.Point(258, 46);
            this.foldersGroup.Name = "foldersGroup";
            this.foldersGroup.Size = new System.Drawing.Size(442, 278);
            this.foldersGroup.TabIndex = 1;
            this.foldersGroup.TabStop = false;
            this.foldersGroup.Text = "Where are the files?";
            // 
            // radioModeCompile
            // 
            this.radioModeCompile.AutoSize = true;
            this.radioModeCompile.BackColor = System.Drawing.SystemColors.Control;
            this.radioModeCompile.Checked = true;
            this.radioModeCompile.Location = new System.Drawing.Point(16, 35);
            this.radioModeCompile.Name = "radioModeCompile";
            this.radioModeCompile.Size = new System.Drawing.Size(171, 24);
            this.radioModeCompile.TabIndex = 3;
            this.radioModeCompile.TabStop = true;
            this.radioModeCompile.Text = "Compile .c to .bytes";
            this.radioModeCompile.UseVisualStyleBackColor = false;
            this.radioModeCompile.CheckedChanged += new System.EventHandler(this.modeRadio_CheckedChanged);
            // 
            // radioModeDecompile
            // 
            this.radioModeDecompile.AutoSize = true;
            this.radioModeDecompile.Location = new System.Drawing.Point(16, 74);
            this.radioModeDecompile.Name = "radioModeDecompile";
            this.radioModeDecompile.Size = new System.Drawing.Size(189, 24);
            this.radioModeDecompile.TabIndex = 4;
            this.radioModeDecompile.Text = "Decompile .bytes to .c";
            this.radioModeDecompile.UseVisualStyleBackColor = true;
            this.radioModeDecompile.CheckedChanged += new System.EventHandler(this.modeRadio_CheckedChanged);
            // 
            // operationGroup
            // 
            this.operationGroup.Controls.Add(this.radioModeCompile);
            this.operationGroup.Controls.Add(this.radioModeDecompile);
            this.operationGroup.Location = new System.Drawing.Point(12, 46);
            this.operationGroup.Name = "operationGroup";
            this.operationGroup.Size = new System.Drawing.Size(230, 128);
            this.operationGroup.TabIndex = 5;
            this.operationGroup.TabStop = false;
            this.operationGroup.Text = "What are we doing?";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGo.Enabled = false;
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(740, 85);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(243, 202);
            this.btnGo.TabIndex = 6;
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 302);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 242);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(258, 365);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(675, 35);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Value = 50;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(258, 331);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 20);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Ready";
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(940, 365);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 35);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "❌";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 418);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.operationGroup);
            this.Controls.Add(this.foldersGroup);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Plasma\'s TMGS Scenario Tools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.foldersGroup.ResumeLayout(false);
            this.foldersGroup.PerformLayout();
            this.operationGroup.ResumeLayout(false);
            this.operationGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dstFolderTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox srcFolderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox foldersGroup;
        private System.Windows.Forms.RadioButton radioModeCompile;
        private System.Windows.Forms.RadioButton radioModeDecompile;
        private System.Windows.Forms.GroupBox operationGroup;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStop;
    }
}

