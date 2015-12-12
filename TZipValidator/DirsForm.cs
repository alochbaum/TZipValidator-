using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZipValidator
{
    public partial class DirsForm : Form
    {
        public bool blChanged = false;
        public string strSInDir, strSRejected, strSProcessed, strSLogging;
        public DirsForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (sender.Equals(this.btnInDir)) tbInDir.Text = folderBrowserDialog1.SelectedPath;
                if (sender.Equals(this.btnRejectDir)) tbRejectDir.Text = folderBrowserDialog1.SelectedPath;
                if (sender.Equals(this.btnProcessed)) tbProcessed.Text = folderBrowserDialog1.SelectedPath;
                if (sender.Equals(this.btnLogging)) tbLogging.Text = folderBrowserDialog1.SelectedPath;
                setChange();
            }
        }
        private void setChange()
        {
            btnSave.Visible = true;
            blChanged = true;
        }

        private void DirsForm_Shown(object sender, EventArgs e)
        {
            tbInDir.Text = strSInDir;
            tbRejectDir.Text = strSRejected;
            tbProcessed.Text = strSProcessed;
            tbLogging.Text = strSLogging;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            strSInDir = tbInDir.Text;
            strSRejected = tbRejectDir.Text;
            strSProcessed = tbProcessed.Text;
            strSLogging = tbLogging.Text;
            this.Close();
        }
    }
}
