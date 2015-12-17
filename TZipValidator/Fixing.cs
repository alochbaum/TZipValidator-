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
        public string strMiddleName;
        private int iRows, iColumns, iOffset;
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
            int iSizeNumField = strShortName.Length - iCharNumStarts;
            int iNumZeros2Add = iSizeNumField - iMiddleFrame.ToString().Length;
            if(iNumZeros2Add<1){
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
            if ((tf.Image.PixelFormat.ToString() == "Format32bppRgb") )
            {
                this.pictureBox1.Image = tf.Image;
                //
                // Scanning run length compression
                //
                if (tf.Header.ImageType.ToString() == "RUN_LENGTH_ENCODED_TRUE_COLOR")
                {
                    this.lblStatus.Text = "scanning image for corner information";
                    this.Refresh();
                    // pixel bytes
                    byte a,b,r,g;
                    
                    // get the size in bytes of each row in the image
                    int intImageRowByteSize = 5 * tf.Header.Width;

                    // get the size in bytes of the whole image
                    int intImageByteSize = intImageRowByteSize * tf.Header.Height;

                    // RLE Packet info
                    byte bRLEPacket = 0;
                    int intRLEPacketType = -1;
                    int intRLEPixelCount = 0;

                    // used to keep track of bytes read
                    int intImageBytesRead = 0;
                    int intImageRowBytesRead = 0;

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
                                intRLEPacketType = (int)(bRLEPacket|128);
                                intRLEPixelCount = (int)(bRLEPacket|127) + 1;
                                
                                // if the high bit is 1 then repeat
                                if (intRLEPacketType == 128)
                                {
                                    a = binReader.ReadByte();
                                    r = binReader.ReadByte();
                                    b = binReader.ReadByte();
                                    g = binReader.ReadByte();
                                    if (a != 0) break;
                                    else intImageRowBytesRead += intRLEPixelCount;
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
                                        else intImageRowBytesRead++;
                                    }
                                }
                            }
                            // calculate row later
                            lblStatus.Text = "result " + intImageRowBytesRead.ToString();
                        }

                    }
                    catch { throw; }

                    //int iMaxPixels = iRows * iColumns;
                    //int iCountPixel = 0, iSeekToFix = -1;
                    //byte[] PixalArray = new byte[4];
                    //using (BinaryReader b = new BinaryReader(File.Open(strMiddleName, FileMode.Open)))
                    //{
                    //    b.BaseStream.Seek(iOffset, SeekOrigin.Begin);
                    //    for (iCountPixel = 0; iCountPixel < iMaxPixels; iCountPixel++)
                    //    {
                    //        b.BaseStream.Read(PixalArray, 0, 4);
                    //        if (PixalArray[3] != 0) break;
                    //    }
                    //    // check again could have ended with iMaxPixels hit
                    //    if (PixalArray[3] != 0) iSeekToFix = iOffset;
                    //    else
                    //    {
                    //        // if it below half the screen, put it on bottom row, else put in on top row
                    //        if (iCountPixel > ((int)(iRows / 2) * iColumns)) iSeekToFix = ((iRows - 1) * iColumns) + iOffset;
                    //        else iSeekToFix = iOffset;
                    //        // if it is on right of row put it on far right
                    //        if ((iCountPixel % iRows) > ((int)iColumns / 2)) iSeekToFix += (iColumns - 1);
                    //    }
                    //    b.Close();
                    //}

                }
            }

            

        }
    }
}
