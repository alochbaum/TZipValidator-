#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZipValidator
{

    public partial class MainForm : Form
    {
        public string strInDir, strRejected, strProcessed, strLogging;
        Log2File myLog2File = new Log2File();
        //
        // loading settings and setting logging directory
        //
        public MainForm()
        {
            InitializeComponent();
            lblInDir.Text = strInDir = TZipValidator.Properties.Settings.Default.InDir;
            strRejected = TZipValidator.Properties.Settings.Default.Rejected;
            strProcessed = TZipValidator.Properties.Settings.Default.Processed;
            strLogging = TZipValidator.Properties.Settings.Default.Logging;
            myLog2File.SetDirectory(strLogging);
            lblStatus.Text = "Waiting For Input";
            myLog2File.WriteLine(DateTime.Now.TimeOfDay.ToString() + 
                "---TZipValidator starting up\r\n");
        }

        //
        // This opens settings Form 
        //
        private void btnDirs_Click(object sender, EventArgs e)
        {
            DirsForm myDirForm = new DirsForm();
            // set settings inside the new form
            myDirForm.strSInDir = strInDir;
            myDirForm.strSRejected = strRejected;
            myDirForm.strSProcessed = strProcessed;
            myDirForm.strSLogging = strLogging;
            myDirForm.ShowDialog();
            //
            // If form has blChanged set to true save settings back
            //
            if (myDirForm.blChanged)
            {
                lblInDir.Text = strInDir = myDirForm.strSInDir;
                strRejected = myDirForm.strSRejected;
                strProcessed = myDirForm.strSProcessed;
                // if logging directory had changed tell logging class
                if (strLogging != myDirForm.strSLogging)
                {
                    myLog2File.SetDirectory(strLogging);
                    // then save setting
                    strLogging = myDirForm.strSLogging;
                }
            }
           
            // closing the created object (form)
            myDirForm = null;
        }

        //
        // I plan to break this up in to functions, for scanning and copying when other buttons are activated
        //
        private void btQuick_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "First Level Checking";
            // To copy all the TZips files in InDir directory to TEMP directory change their extension
            string strFileName, strDestFile, strDestdir, strErr ;
            if (System.IO.Directory.Exists(strInDir))
            {

                string[] files = System.IO.Directory.GetFiles(strInDir);
                logString("Looking for files in: " + strInDir);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
#if DEBUG
                    logString("Working File: "+s.ToString());
#endif
                    // Use static Path methods to extract only the file name from the path.
                    if (System.IO.Path.GetExtension(s).ToUpper() == ".TZIP")
                    {
                        try
                        {

                            strFileName = System.IO.Path.GetFileName(s);
                            logString("Copying File: " + strFileName);
                            strFileName = System.IO.Path.GetFileNameWithoutExtension(s);
                            strDestFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), strFileName + ".zip");
                            System.IO.File.Copy(s, strDestFile, true);
                            strDestdir = System.IO.Path.GetTempPath()+@"TZipV\";
                            
                            // if can't unzip error is thrown in a string
                            strErr = UnZipFiles.extract2dir(strDestFile, strDestdir);
                            if (strErr.Length > 0) throw new MyException(strErr);
#if DEBUG
                            else logString("Extracted file" + strDestFile + " to " + strDestdir);
#endif
                            // check TZip for mode.txt (optional)
                            logString( PrintModeDotText.ReturnMode(strDestdir));
                            string strTemp = PrintModeDotText.RemoveUnderScoreDirectories(strDestdir);
                            if(strTemp.Length > 0) { logString(strTemp); }
                            
                            // checking TZip which could lead to rejection 
                            CountAndCheckTGAs myCacTs = new CountAndCheckTGAs();
                            myCacTs.CountCheck(strDestdir);
                            

                        }
                        catch (System.Exception excep)
                        {
                            string err = "Error Processing found TZip Files: ";
                            err += excep.Message;
                            logString(err);
                        }
                        // unziping file to same folder
                        //logString("Unzipping file: " + destFile);
                        //using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                        //{
                        //    foreach (ZipArchiveEntry entry in archive.Entries)
                        //    {
                        //        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        //        {
                        //            entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
                        //        }
                        //    }
                        //}
                    }
                }
                lblStatus.Text = "Waiting For Input";
            }
            else
            {
                logString("Source path does not exist!");
                lblStatus.Text = "Please Fix Source Dir Settings";
            }
        }

        //
        // This sends logging string out to special Log2File class method and writes to richtext
        //
        private void logString(string str2log)
        {
            str2log = DateTime.Now.TimeOfDay.ToString() + " " + str2log;
            string strOut = myLog2File.WriteLine(str2log);
            // if there is an error write line to log, put on screen
            if (strOut.Length > 0)
            {
                richTextBox1.Text += strOut + "\r\n";
            }
            richTextBox1.Text += str2log + "\r\n";
        }


        //
        // Saving settings on shutdown
        //
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TZipValidator.Properties.Settings.Default.InDir = strInDir;
            TZipValidator.Properties.Settings.Default.Rejected = strRejected;
            TZipValidator.Properties.Settings.Default.Processed = strProcessed;
            TZipValidator.Properties.Settings.Default.Logging = strLogging;
            TZipValidator.Properties.Settings.Default.Save();
        }

     }
    class MyException : ApplicationException
    {
        public MyException(String Msg)
            : base(Msg)
        {
        }
    }

}
