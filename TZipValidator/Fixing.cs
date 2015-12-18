using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class Fixing : Form
    {
        public string strFullFileName;
        public int iCharNumStarts;
        public int iNumberOfFiles;
        public MainForm m_parent;
        private int iSizeNumField;
        public string strMiddleName,strFixMode="";
        //private int iRows, iColumns, iOffset;
        public Fixing()
        {
            InitializeComponent();
        }

        private void Fixing_Shown(object sender, EventArgs e)
        {
            lblStatus.Text = " Full " + strFullFileName + " Num " + iNumberOfFiles.ToString() + " char " + iCharNumStarts.ToString();
            // Calculate the middle frame number
            int iMiddleFrame;
            if (iNumberOfFiles < 3) iMiddleFrame = 1;
            else iMiddleFrame = (int)(iNumberOfFiles / 2);
            // Calculate size of number field 001 would be 3 and 0001 would be 4
            // Also need to add .TGA later to short name
            string strShortName = Path.GetFileNameWithoutExtension(strFullFileName);
            iSizeNumField = strShortName.Length - iCharNumStarts;
            int iNumZeros2Add = iSizeNumField - iMiddleFrame.ToString().Length;
            if (iNumZeros2Add < 1)
            {
                strMiddleName = Path.GetDirectoryName(strFullFileName) + @"\" +
                     strShortName.Substring(0, iCharNumStarts) + iMiddleFrame.ToString() + ".tga";
            }
            else
            {
                string strTemp = new String('0', iNumZeros2Add);
                strMiddleName = Path.GetDirectoryName(strFullFileName) + @"\" +
                    strShortName.Substring(0, iCharNumStarts) + strTemp + iMiddleFrame.ToString() + ".tga";
            }
            Paloma.TargaImage tf = new Paloma.TargaImage(strMiddleName);
            if ((tf.Image.PixelFormat.ToString() == "Format32bppRgb"))
            {
                this.pictureBox1.Image = tf.Image;

                // pixel bytes
                byte a, b, r, g;

                // get the size in bytes of each row in the image, RLE doesn't count
                int intImageRowByteSize = 4 * tf.Header.Width;

                // get the size in bytes of the whole image
                int intImageByteSize = intImageRowByteSize * tf.Header.Height;

                // used to keep track of bytes read
                int intImageBytesRead = 0;
                #region Compressed
                if (tf.Header.ImageType.ToString() == "RUN_LENGTH_ENCODED_TRUE_COLOR")
                {
                    this.lblStatus.Text = "scanning image for corner information";
                    this.Refresh();

                    // RLE Packet info
                    byte bRLEPacket = 0;
                    int intRLEPacketType = -1;
                    int intRLEPixelCount = 0;

                    try
                    {

                        using (BinaryReader binReader = new BinaryReader(File.Open(strMiddleName, FileMode.Open)))
                        {
                            // seek to the beginning of the image data using the ImageDataOffset value
                            binReader.BaseStream.Seek(tf.Header.ImageDataOffset, SeekOrigin.Begin);

                            // keep reading until we have the all image bytes
                            while (intImageBytesRead < intImageByteSize)
                            {
                                // get the RLE packet
                                bRLEPacket = binReader.ReadByte();
                                // if the high bit is 1 then repeat next pixel total of low bits + 1
                                // if the high bit is 0 then next are number of non-repeating pixels +1
                                // logical or broken in this section
                                intRLEPacketType = (int)(bRLEPacket & 128);
                                intRLEPixelCount = (int)(bRLEPacket & 127) + 1;

                                // if the high bit is 1 then repeat
                                if (intRLEPacketType == 128)
                                {
                                    a = binReader.ReadByte();
                                    r = binReader.ReadByte();
                                    b = binReader.ReadByte();
                                    g = binReader.ReadByte();
                                    if (a != 0) break;
                                    else intImageBytesRead += intRLEPixelCount * 4;
                                }
                                else // these are individual bytes
                                {
                                    // get the number of bytes to read based on the read pixel count
                                    int intBytesToRead = intRLEPixelCount * 4;
                                    // read each byte
                                    for (int i = 0; i < intBytesToRead; i++)
                                    {
                                        a = binReader.ReadByte();
                                        r = binReader.ReadByte();
                                        b = binReader.ReadByte();
                                        g = binReader.ReadByte();
                                        if (a != 0) break;
                                        else intImageBytesRead += 4;
                                    }
                                }
                            }
                            // calculating row and half
                            if (intImageBytesRead < (intImageByteSize / 2)) strFixMode = "First_Row_";
                            else strFixMode = "Last_Row_";
                            if ((intImageBytesRead % (tf.Header.Width * 4)) > (tf.Header.Width * 2))
                            {
                                strFixMode += "End";
                            }
                            else { strFixMode += "Start"; }
                            lblStatus.Text = "Total bytes " + intImageBytesRead.ToString() +
                                " " + strFixMode;
                        }

                    }
                    catch { throw; }
                } // end if for compressed mode
                #endregion
                #region NON-COMPRESSED
                else if (tf.Header.ImageType.ToString() == "UNCOMPRESSED_TRUE_COLOR")
                {


                    try
                    {
                        byte[] chunk = new byte[4096];
                        using (BinaryReader binReader = new BinaryReader(File.Open(strMiddleName, FileMode.Open)))
                        {
                            chunk = binReader.ReadBytes(tf.Header.ImageDataOffset);
                            // loop through each row in the image
                            for (int i = 0; i < tf.Header.Height; i++)
                            {
                                // loop through each byte in the row
                                for (int j = 0; j < intImageRowByteSize; j += 4)
                                {
                                    // reading one pixel
                                    a = binReader.ReadByte();
                                    r = binReader.ReadByte();
                                    b = binReader.ReadByte();
                                    g = binReader.ReadByte();
                                    // if there is alpha in pixel quit
                                    if (a != 0) break;
                                    else intImageBytesRead += 4;
                                }
                            }
                        }

                    }
                    catch { throw; }
                }
                #endregion
                #region neither Compress or Non-Compressed
                else
                {
                    // throw back checked for 2 images types and it was neither
                }
                #endregion
                #region Report Fixing Mode
                // calculating row and half
                if (intImageBytesRead < (intImageByteSize / 2)) strFixMode = "First_Row_";
                else strFixMode = "Last_Row_";
                if ((intImageBytesRead % (tf.Header.Width * 4)) > (tf.Header.Width * 2))
                {
                    strFixMode += "End";
                }
                else { strFixMode += "Start"; }
                lblStatus.Text = "Total bytes until alpha change: " + intImageBytesRead.ToString() +
                                " Fixing Mode is: " + strFixMode + " Started Fixing, this can take a long time. ";
                #endregion

            }
            #region not Format32bppRgb
            else
            {
                //throw back not Format32bppRgb
            }
            #endregion
            // clearing out old graphic
            //tf.Dispose();
            // show controls and file counter
            lblListFiles.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Maximum = iNumberOfFiles;
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
            #region Start Fixing if Fix Mode is Greater Than Zero
            if (strFixMode.Length > 0)
            {
                int iXchange, iYchange;
                // setting up where to fix alpha
                switch (strFixMode)
                {
                    case "First_Row_Start":
                        iXchange = 0;
                        iYchange = 0;
                        break;
                    case "First_Row_End":
                        iXchange = 1919;
                        iYchange = 0;
                        break;
                    case "Last_Row_Start":
                        iXchange = 0;
                        iYchange = 1079;
                        break;
                    case "Last_Row_End":
                    default:
                        iXchange = 1919;
                        iYchange = 1079;
                        break;
                }
                try
                {
                    for (int i = 0; i < iNumberOfFiles; i++)
                    {
                        int iNumOfZeros2Add = iSizeNumField - iMiddleFrame.ToString().Length;
                        if (iNumOfZeros2Add < 1)
                        {
                            strMiddleName = Path.GetDirectoryName(strFullFileName) + @"\" +
                                 strShortName.Substring(0, iCharNumStarts) + i.ToString() + ".tga";
                        }
                        else
                        {
                            string strTemp = new String('0', iNumOfZeros2Add);
                            strMiddleName = Path.GetDirectoryName(strFullFileName) + @"\" +
                                strShortName.Substring(0, iCharNumStarts) + strTemp + iMiddleFrame.ToString() + ".tga";
                        }
                        lblListFiles.Text = "Fixing number: " + i.ToString() + " File Name: " + strMiddleName;
                        m_parent.logString(lblListFiles.Text);
                        progressBar1.Value = i;
                        Refresh();
                        #region Fixing Uncompressed
                        if (tf.Header.ImageType.ToString() == "UNCOMPRESSED_TRUE_COLOR")
                        {
                            using (BinaryReader binReader = new BinaryReader(File.Open(strMiddleName, FileMode.Open)))
                            {
                                // Reading buffers
                                byte[] chunk = new byte[4096];
                                byte[] bPixel = new byte[4];
                                // going to write file to %TEMP%\TZipV\Out\ folder
                                using (BinaryWriter binWriter = new BinaryWriter(File.Open(
                                    System.IO.Path.GetTempPath() + @"TZipV\Out\" +
                                    Path.GetFileName(strMiddleName), FileMode.Create)))
                                {
                                    // write file header
                                    chunk = binReader.ReadBytes(tf.Header.ImageDataOffset);
                                    binWriter.Write(chunk, 0, tf.Header.ImageDataOffset);
                                    // loop through each row in the image
                                    for (int j = 0; j < tf.Header.Height; i++)
                                    {
                                        // loop through each byte in the row
                                        for (int k = 0; k < tf.Header.Width; j += 4)
                                        {
                                            // reading one pixel
                                            bPixel = binReader.ReadBytes(4);
                                            // checking if it the change pixel
                                            if ((k == iYchange) && (j == iXchange))
                                            {
                                                // changing byte every file, to amount higher than 22 to test
                                                bPixel[0] = (byte)(((i % 4) * 25)+22);
                                            }
                                            binWriter.Write(bPixel, 0, 4);
                                        }
                                    }
                                    // write out rest of file
                                    while (binReader.PeekChar() != -1)
                                    {
                                        binWriter.Write(binReader.ReadChar());
                                    }
                                }
                            }
                        }
#endregion
                        #region Fixing Compress
                        else if (tf.Header.ImageType.ToString() == "RUN_LENGTH_ENCODED_TRUE_COLOR")
                        {
                            using (BinaryReader binReader = new BinaryReader(File.Open(strMiddleName, FileMode.Open)))
                            {
                                //
                                byte[] chunk = new byte[4096];
                                byte[] bPixel = new byte[4];
                                byte bRLEPacket;
                                int intImageBytesRead = 0, intRLEPacketType, intRLEPixelCount;

                                // get the size in bytes of each row in the image, RLE doesn't count
                                int intImageRowByteSize = 4 * tf.Header.Width;

                                // get the size in bytes of the whole image
                                int intImageByteSize = intImageRowByteSize * tf.Header.Height;

                                // compute the change byte (right now it is end of the rows for simple compression change)
                                int iChangeByte = (iYchange * 7680) + 7676;
                                using (BinaryWriter binWriter = new BinaryWriter(File.Open(
                                    System.IO.Path.GetTempPath() + @"TZipV\Out\" +
                                    Path.GetFileName(strMiddleName), FileMode.Create)))
                                {
                                    // write file header
                                    chunk = binReader.ReadBytes(tf.Header.ImageDataOffset);
                                    binWriter.Write(chunk, 0, tf.Header.ImageDataOffset);
                                    // keep reading until we have the all image bytes
                                    while (intImageBytesRead < intImageByteSize)
                                    {
                                        // get the RLE packet
                                        bRLEPacket = binReader.ReadByte();
                                        // if the high bit is 1 then repeat next pixel total of low bits + 1
                                        // if the high bit is 0 then next are number of non-repeating pixels +1
                                        // logical or broken in this section
                                        intRLEPacketType = (int)(bRLEPacket & 128);
                                        intRLEPixelCount = (int)(bRLEPacket & 127) + 1;

                                        // if the high bit is 1 then repeat
                                        if (intRLEPacketType == 128)
                                        {
                                            bPixel = binReader.ReadBytes(4);
                                            intImageBytesRead += intRLEPixelCount * 4;
                                            //counting on compressing by row  just changing row before fixing
                                            if (iChangeByte < intImageBytesRead)
                                            {
                                                bRLEPacket--;
                                                binWriter.Write(bRLEPacket);
                                                binWriter.Write(bPixel, 0, 4);
                                                // value for 1 unique pixel
                                                bRLEPacket = 0;
                                                bPixel[0] = (byte)(((i % 4) * 25) + 22);
                                                binWriter.Write(bRLEPacket);
                                                binWriter.Write(bPixel, 0, 4);
                                            }
                                        }
                                        else // these are individual bytes
                                        {
                                            // get the number of bytes to read based on the read pixel count
                                            int intBytesToRead = intRLEPixelCount * 4;
                                            // read each byte
                                            for (int ii = 0; ii < intBytesToRead; ii++)
                                            {
                                                bPixel = binReader.ReadBytes(4);
                                                if (iChangeByte == intImageBytesRead)
                                                    // changing byte every file, to amount higher than 22 to test
                                                    bPixel[0] = (byte)(((i % 4) * 25)+22);
                                                intImageBytesRead += 4;
                                                binWriter.Write(bRLEPacket);
                                                binWriter.Write(bPixel, 0, 4);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        #endregion
                        else
                        {
                            // Throw error modified file not correct
                        }
                    }

                }
                catch
                {
                    throw;
                }

            }
            #endregion  // fixing is mode is length is there


            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
            m_parent.postFixingGood("Fixing Made it to End");


        }
    }
}
