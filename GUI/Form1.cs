using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ConsoleApp1;
using System.Linq.Expressions;

namespace GUI
{
    enum Mode
    {
        Compile,
        Decompile,
    }
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        FolderPicker sourceFolderPicker = new FolderPicker();
        FolderPicker destinationFolderPicker = new FolderPicker();
        private string srcFolder;
        private string dstFolder;
        private Mode mode;

        bool _enableControls = true;
        public bool EnableControls
        {
            get
            {
                return _enableControls;
            }
            set
            {
                _enableControls = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnableControls"));
            }
        }
        App app = new App();

        public event PropertyChangedEventHandler PropertyChanged;

        public string SourceFolder
        {
            get
            {
                return srcFolder;
            }
            set
            {
                validateFolder(value);
                srcFolder = value;
                verifyFolderChange();
            }
        }

        public string DestinationFolder
        {
            get
            {
                return dstFolder;
            }
            set
            {
                validateFolder(value);
                dstFolder = value;
                verifyFolderChange();
            }
        }

        public string StatusLine { get; set; } = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.DataBindings.Add("Text", this, "StatusLine");
            srcFolderTextBox.DataBindings.Add("Text", sourceFolderPicker, "ResultPath");
            dstFolderTextBox.DataBindings.Add("Text", destinationFolderPicker, "ResultPath");

            this.DataBindings.Add("SourceFolder", srcFolderTextBox, "Text");
            this.DataBindings.Add("DestinationFolder", dstFolderTextBox, "Text");

            List<Control> lockableControls = new List<Control> { operationGroup, foldersGroup };

            foreach (Control c in lockableControls)
            {
                c.DataBindings.Add("Enabled", this, "EnableControls");
            }

            app.TotalFilesChanged += totalFilesChanged;
            app.ProcessedFilesChanged += processedFilesChanged;

            progressBar1.Visible = false;
        }

        private void totalFilesChanged(int newCount)
        {
            progressBar1.BeginInvoke(new Action(() => { progressBar1.Maximum = newCount; }));
        }

        private void processedFilesChanged(int newCount, string lastProcessedFile)
        {
            String labelStatus = "";
            switch (mode)
            {
                case Mode.Compile:
                    labelStatus = "Compiled ";
                    break;
                case Mode.Decompile:
                    labelStatus = "Decompiled ";
                    break;
            }
            if (!app.StopRequested)
            {
                lblStatus.BeginInvoke(new Action(() => { lblStatus.Text = labelStatus + lastProcessedFile; }));
            }

            progressBar1.BeginInvoke(new Action(() => {
                if (progressBar1.Value < newCount)
                {
                    progressBar1.Value = newCount;
                }
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sourceFolderPicker.ShowDialog();

        }


        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Top = 118;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Top = 196;
        }

        bool verifyFolderChange()
        {
            if (validateFolder(srcFolder) && validateFolder(dstFolder) && srcFolder != dstFolder)
            {
                this.btnGo.Enabled = true;
                return true;
            }
            else
            {
                this.btnGo.Enabled = false;
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            destinationFolderPicker.ShowDialog();
        }

        private bool validateFolder(string path)
        {
            if (path == "")
            {
                return false;
            }
            bool valid = Directory.Exists(path);
            if (valid)
            {
                return true;
            }
            return false;
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            if (!verifyFolderChange())
            {
                MessageBox.Show("Folder is not valid", "Invalid folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StatusLine = "Starting...";
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            btnGo.Enabled = false;
            EnableControls = false;
            btnStop.Enabled = true;
            btnStop.Visible = true;
            try
            {
                switch(mode) 
                {
                    case Mode.Compile:
                        compile().Wait();
                        break;
                    case Mode.Decompile:
                        await decompile();
                        break;
                } 
                MessageBox.Show("All done!", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (AggregateException exception)
            {

                StringBuilder sb = new StringBuilder();
                for (Exception innerEx = exception.InnerException; innerEx != null; innerEx = innerEx.InnerException)
                {
                    sb.Append(innerEx.Message + "\n");
                }

                MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                StatusLine = "";
                btnStop.Visible = false;
                progressBar1.Value = progressBar1.Maximum;
                EnableControls = true;
                btnGo.Enabled = true;
                progressBar1.Visible = false;
            }

        }

        private Task compile()
        {
            return Task.Run(() => app.CompileDirs(srcFolder, dstFolder));
        }
        
        private async Task decompile()
        {
            await Task.Run(() =>
            {
                app.DecompileDirs(srcFolder, dstFolder);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.EnableControls = !this.EnableControls;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StatusLine = "Stopping...";
            btnStop.Enabled = false;
            app.Stop();
        }

        private void modeRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
            {
                return;
            }
            if (rb == radioModeCompile)
            {
                mode = Mode.Compile;
            }
            if (rb == radioModeDecompile)
            {
                mode = Mode.Decompile;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
