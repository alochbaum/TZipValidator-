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
        // To keep a single instance of this creating it, here and nulling before needed
        Fixing myFixing = new Fixing();

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
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("TZipValidator Version {0}", version);
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
        private void btnAllHeaders_Click(object sender, EventArgs e)
        {
            CheckFiles("All headers");
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            CheckFiles("Fix All");
        }
        private void btQuick_Click(object sender, EventArgs e)
        {
            CheckFiles("Just header");
        }

        //
        // I plan to break this up in to functions, for scanning and copying when other buttons are activated
        //
        private void CheckFiles(string strMode)
        {
            lblStatus.Text = "First Level Checking";
            // To copy all the TZips files in InDir directory to TEMP directory change their extension
            string strFileName, strDestFile, strDestdir, strErr;
            if (System.IO.Directory.Exists(strInDir))
            {
                #region counting and checking files
                //
                // here is the scanning of the input folder for .TZip files, then copy the .TZip to Temp, unzip and explore folder
                //
                string[] files = System.IO.Directory.GetFiles(strInDir);
#if DEBUG
                logString("Looking for files in: " + strInDir);
#endif
                // cleaning data out of counting object
                CountAndCheckTGAs myCacTs = null;
                // cleaning data out of first header object
                ReadTargaHeader myFirstReadHeader = null;

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
#if DEBUG
                    logString("Working File: " + s.ToString());
#endif
                    // Use static Path methods to extract only the file name from the path.
                    if (System.IO.Path.GetExtension(s).ToUpper() == ".TZIP")
                    {
                        try
                        {

                            strFileName = System.IO.Path.GetFileName(s);
                            // show user copied file
                            logString("Copying File: " + strFileName);

                            strFileName = System.IO.Path.GetFileNameWithoutExtension(s);
                            strDestFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), strFileName + ".zip");
                            System.IO.File.Copy(s, strDestFile, true);
                            strDestdir = System.IO.Path.GetTempPath() + @"TZipV\";

                            // if can't unzip error is thrown in a string
                            strErr = UnZipFiles.extract2dir(strDestFile, strDestdir);
                            if (strErr.Length > 0) throw new MyException(strErr);
#if DEBUG
                            else logString("Extracted file" + strDestFile + " to " + strDestdir);
#endif
                            // check TZip for mode.txt (optional)
                            logString(PrintModeDotText.ReturnMode(strDestdir));
                            string strTemp = PrintModeDotText.RemoveUnderScoreDirectories(strDestdir);
                            if (strTemp.Length > 0) { logString(strTemp); }

                            // checking TZip which could lead to rejection 
                            myCacTs = new CountAndCheckTGAs();
                            strTemp = myCacTs.CountCheck(strDestdir);
                            if (strTemp.Length > 0) { logString(strTemp + "\r\n"); }

                            // reporting back count and first and last strings
                            logString("---------Info on all TGA Files---------\r\n There are "
                                + myCacTs.iCountArray.ToString() + " TGA files, starting with "
                                + Path.GetFileName(myCacTs.strFirstFile) +
                                " to " + Path.GetFileName(myCacTs.strLastFile) + "."
                             );

                            // create new instance of dll class to read header, just for first file
                            myFirstReadHeader = new ReadTargaHeader();

                            // checking first sorted name length
                            if (myCacTs.strFirstFile.Length > 0)
                            {
                                // if good log optional data first (includes filename)
                                myFirstReadHeader.HeaderString(myCacTs.strFirstFile);
                                // then log header data which must match all other headers
                                logString(myFirstReadHeader.strTGAOptionalData);
                                logString(myFirstReadHeader.strTGAVitalData);
                                if (myFirstReadHeader.blTGAisTransparent)
                                    logString("--TGA is transparent!!");
                            }
                #endregion
                            #region All headers check
                            // if mode is check all headers we should check all headers
                            if (strMode == "All headers")
                            {
                                // Set cursor as hourglass
                                Cursor.Current = Cursors.WaitCursor;
                                lblStatus.Text = "Processing possibly many TGA files";

                                logString("Above is data from first header, comparing all. This takes time!");

                                LooppingReadHeaders myLooping = new LooppingReadHeaders();
                                strTemp = myLooping.TestAllHeadersWithFirst(strDestdir,
                                    myCacTs.iCountArray, myFirstReadHeader);
                                if (strTemp.Length > 0) { logString(strTemp); }
                                else
                                {
                                    logString("All headers in " + myCacTs.iCountArray.ToString() +
                                        " files are good!");
                                }
                                // Set cursor as default arrow
                                Cursor.Current = Cursors.Default;
                            }
                            #endregion
                            #region Fix All
                            // if mode is fix then we had better fix
                            if (strMode == "Fix All")
                            {
                                myFixing = null;
                                myFixing = new Fixing();
                                // setting the data
                                myFixing.strFullFileName = myCacTs.strFirstFile;
                                myFixing.iCharNumStarts = myCacTs.iFirstNumCharInString;
                                myFixing.iNumberOfFiles = myCacTs.iCountArray;
                                myFixing.m_parent = this;
                                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                                if (myFixing.ShowDialog(this) == DialogResult.OK)
                                {
                                    // Folder needs compressing at System.IO.Path.GetTempPath() + @"TZipV\Out\"
                                    // if can't unzip error is thrown in a string
                                    string strCompressErr = UnZipFiles.compressNmove(System.IO.Path.GetTempPath() + @"TZipV\Out\", System.IO.Path.GetTempPath() + @"TZipV\Out.tZip");
                                    if (strCompressErr.Length > 0) logString(strCompressErr);
#if DEBUG
                                    else logString(@" I created %Temp%\TZipV\Out.tZip");
#endif
                                    // moving the good file to processed directory and deleting original
                                    File.Move(System.IO.Path.GetTempPath() + @"TZipV\Out.tZip", strProcessed + @"\" + System.IO.Path.GetFileName(s)); // Try to move
                                    File.Delete(s);
                                }
                                else
                                {
                                    logString(myFixing.strError);
                                }
                                myFixing.Dispose();
                                // Set cursor as default arrow
                                Cursor.Current = Cursors.Default;
                                logString(s);
                            
                            }
                            #endregion
                            // cleans up temp folder unless fixing is calling post fixing functions
                            if (strMode != "Fix All")
                                Directory.Delete(strDestdir, true);

                        }
                        catch (System.Exception excep)
                        {
                            string err = "Error Processing found TZip Files: ";
                            err += excep.Message;
                            logString(err);
                            if(strMode == "Fix All")
                            {
                                try
                                {
                                    File.Move(s, strRejected + @"\" + System.IO.Path.GetFileName(s)); // Try to move
                                    logString("Regected: " + s + " because of Error logged above.");
                                }
                                catch (IOException ex)
                                {
                                    logString("Error moving file with Error: " + ex); // Write error
                                }
                            }
                        }
                    } // end if file had .TZip Extension
                    else if (strMode == "Fix All")
                    {
                        try
                        {
                            File.Move(s, strRejected + @"\"+ System.IO.Path.GetFileName(s) ); // Try to move
                            logString("Regected: " + s + " because it didn't have .tzip extenstion.");
                        }
                        catch (IOException ex)
                        {
                            logString("Error moving file without tzip extension: "+ex); // Write error
                        }
                    }

                   
                    lblStatus.Text = "Looping through found Files";
                }

            }
            else
            {
                logString("Source path does not exist!");
                lblStatus.Text = "Please Fix Source Dir Settings";
            }
            lblStatus.Text = "Waiting for user input";
        }

        #region logString
        //
        // This sends logging string out to special Log2File class method and writes to richtext
        // Also used by fixing class
        //
        public void logString(string str2log)
        {
            str2log = DateTime.Now.TimeOfDay.ToString() + " " + str2log;
            string strOut = myLog2File.WriteLine(str2log);
            // if there is an error write line to log, put on screen
            if (strOut.Length > 0)
            {
                richTextBox1.Text += strOut + "\r\n";
            }
            richTextBox1.Text += str2log + "\r\n";
            Refresh();
        }
        #endregion
        #region Saving Settings as FormClosing
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
        #endregion
    }
    class MyException : ApplicationException
    {
        public MyException(String Msg)
            : base(Msg)
        {
        }
    }

}
