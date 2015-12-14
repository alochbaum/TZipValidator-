using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZipValidator
{
    class ReadTargaHeader
    {
        public string strTGAOptionalData = "";
        public string strTGAVitalData = "";
        public bool blTGAisTransparent = false;
        public string HeaderString(string filename)
        {

            string strHeader = ""; 
            try{
                FileInfo fi = new FileInfo(filename);
                Paloma.TargaImage tf = new Paloma.TargaImage(filename);
                StringBuilder sbImageInfoOpt = new StringBuilder();
                StringBuilder sbImageInfoVital = new StringBuilder();

                sbImageInfoOpt.Append("---Optional File Info --- \r\n");
                sbImageInfoVital.Append("---File Parameters which must match ---\r\n");
                sbImageInfoOpt.Append("File: " + fi.Name + "\r\n");
                sbImageInfoOpt.Append("File Size: " + fi.Length.ToString() + " bytes" + "\r\n");
                sbImageInfoVital.Append("PixelFormat: " + tf.Image.PixelFormat.ToString() + "\r\n");
                sbImageInfoVital.Append("Size: " + tf.Image.Size.ToString() + "\r\n");
                sbImageInfoVital.Append("Format: " + tf.Format.ToString() + "\r\n");
                //sbImageInfoVital.Append("Stride: " + tf.Stride.ToString() + "\r\n");
                //sbImageInfoVital.Append("Padding: " + tf.Padding.ToString() + "\r\n");
                sbImageInfoVital.Append("AttributeBits: " + tf.Header.AttributeBits.ToString() + "\r\n");
                sbImageInfoVital.Append("BytesPerPixel,PixelDepth: " + tf.Header.BytesPerPixel.ToString() + ","
                    + tf.Header.PixelDepth.ToString() + "\r\n");
                if ((tf.Header.BytesPerPixel == 4) && (tf.Header.PixelDepth == 32)) blTGAisTransparent = true;
                //sbImageInfoVital.Append("ColorMapEntrySize: " + tf.Header.ColorMapEntrySize.ToString() + "\r\n");
                //sbImageInfoVital.Append("ColorMapLength: " + tf.Header.ColorMapLength.ToString() + "\r\n");
                sbImageInfoVital.Append("ColorMapType: " + tf.Header.ColorMapType.ToString() + "\r\n");
                //sbImageInfoVital.Append("ColorMapFirstEntryIndex: " + tf.Header.ColorMapFirstEntryIndex.ToString() + "\r\n");
                sbImageInfoVital.Append("FirstPixelDestination: " + tf.Header.FirstPixelDestination.ToString() + "\r\n");
                sbImageInfoVital.Append("X,Y Origin: " + tf.Header.XOrigin.ToString() + ","
                     + tf.Header.YOrigin.ToString() + "\r\n");
                sbImageInfoVital.Append("Height,Width: " + tf.Header.Height.ToString() + ","
                    + tf.Header.Width.ToString() + "\r\n");
                sbImageInfoVital.Append("Horz,VertTransferOrder: " + tf.Header.HorizontalTransferOrder.ToString() + ","
                    + tf.Header.VerticalTransferOrder.ToString() + "\r\n");
                sbImageInfoVital.Append("ImageDataOffset: " + tf.Header.ImageDataOffset.ToString() + "\r\n");
                //sbImageInfoVital.Append("ImageIDLength: " + tf.Header.ImageIDLength.ToString() + "\r\n");
                //sbImageInfoVital.Append("ImageIDValue: " + tf.Header.ImageIDValue.ToString() + "\r\n");
                sbImageInfoVital.Append("ImageType: " + tf.Header.ImageType.ToString() + "\r\n");

 
                if (tf.Footer.ExtensionAreaOffset > 0)
                {
                    sbImageInfoOpt.Append("EXTENSION AREA: " + "\r\n");
                    sbImageInfoOpt.Append("AttributesType: " + tf.ExtensionArea.AttributesType.ToString() + "\r\n");
                    sbImageInfoOpt.Append("AuthorComments: " + tf.ExtensionArea.AuthorComments.ToString() + "\r\n");
                    sbImageInfoOpt.Append("AuthorName: " + tf.ExtensionArea.AuthorName.ToString() + "\r\n");
                    sbImageInfoOpt.Append("ColorCorrectionOffset: " + tf.ExtensionArea.ColorCorrectionOffset.ToString() + "\r\n");
                    sbImageInfoOpt.Append("DateTimeStamp: " + tf.ExtensionArea.DateTimeStamp.ToString() + "\r\n");
                    sbImageInfoOpt.Append("ExtensionSize: " + tf.ExtensionArea.ExtensionSize.ToString() + "\r\n");
                    sbImageInfoVital.Append("GammaDenominator: " + tf.ExtensionArea.GammaDenominator.ToString() + "\r\n");
                    sbImageInfoVital.Append("GammaNumerator: " + tf.ExtensionArea.GammaNumerator.ToString() + "\r\n");
                    sbImageInfoVital.Append("GammaRatio: " + tf.ExtensionArea.GammaRatio.ToString() + "\r\n");
                    sbImageInfoOpt.Append("JobName: " + tf.ExtensionArea.JobName.ToString() + "\r\n");
                    sbImageInfoOpt.Append("JobTime: " + tf.ExtensionArea.JobTime.ToString() + "\r\n");
                    sbImageInfoOpt.Append("KeyColor: " + tf.ExtensionArea.KeyColor.ToString() + "\r\n");
                    sbImageInfoVital.Append("PixelAspectRatio: " + tf.ExtensionArea.PixelAspectRatio.ToString() + "\r\n");
                    sbImageInfoVital.Append("PixelAspectRatioDenominator: " + tf.ExtensionArea.PixelAspectRatioDenominator.ToString() + "\r\n");
                    sbImageInfoVital.Append("PixelAspectRatioNumerator: " + tf.ExtensionArea.PixelAspectRatioNumerator.ToString() + "\r\n");
                    sbImageInfoVital.Append("PostageStampOffset: " + tf.ExtensionArea.PostageStampOffset.ToString() + "\r\n");
                    sbImageInfoVital.Append("ScanLineOffset: " + tf.ExtensionArea.ScanLineOffset.ToString() + "\r\n");
                    sbImageInfoOpt.Append("SoftwareID: " + tf.ExtensionArea.SoftwareID.ToString() + "\r\n");
                    sbImageInfoOpt.Append("SoftwareVersion: " + tf.ExtensionArea.SoftwareVersion.ToString() + "\r\n");
                }



                if (tf.Format == Paloma.TGAFormat.NEW_TGA)
                {
                    sbImageInfoOpt.Append("\r\n");
                    sbImageInfoOpt.Append("FOOTER: " + "\r\n");
                    sbImageInfoOpt.Append("ExtensionAreaOffset: " + tf.Footer.ExtensionAreaOffset.ToString() + "\r\n");
                    sbImageInfoOpt.Append("DeveloperDirectoryOffset: " + tf.Footer.DeveloperDirectoryOffset.ToString() + "\r\n");
                    sbImageInfoOpt.Append("Signature: " + tf.Footer.Signature.ToString() + "\r\n");
                    sbImageInfoOpt.Append("ReservedCharacter: " + tf.Footer.ReservedCharacter.ToString() + "\r\n");
                }
                // storing the header data in accessable strings
                strTGAOptionalData = sbImageInfoOpt.ToString();
                strTGAVitalData = sbImageInfoVital.ToString();
                // freeing up the TGA class for memory problems
                tf.Dispose();
            }
            catch
            {
                throw;
            }

            return strHeader;
        }
        public bool Compare(string filename, ReadTargaHeader mFirstHeader)
        {
            StringBuilder sbImageInfoVital = new StringBuilder();
            Paloma.TargaImage newtf = new Paloma.TargaImage(filename);
            bool blreturn = true;
            sbImageInfoVital.Append("---File Parameters which must match ---\r\n");
            sbImageInfoVital.Append("PixelFormat: " + newtf.Image.PixelFormat.ToString() + "\r\n");
            sbImageInfoVital.Append("Size: " + newtf.Image.Size.ToString() + "\r\n");
            sbImageInfoVital.Append("Format: " + newtf.Format.ToString() + "\r\n");
            sbImageInfoVital.Append("AttributeBits: " + newtf.Header.AttributeBits.ToString() + "\r\n");
            sbImageInfoVital.Append("BytesPerPixel,PixelDepth: " + newtf.Header.BytesPerPixel.ToString() + ","
                + newtf.Header.PixelDepth.ToString() + "\r\n");
            sbImageInfoVital.Append("ColorMapType: " + newtf.Header.ColorMapType.ToString() + "\r\n");
            sbImageInfoVital.Append("FirstPixelDestination: " + newtf.Header.FirstPixelDestination.ToString() + "\r\n");
            sbImageInfoVital.Append("X,Y Origin: " + newtf.Header.XOrigin.ToString() + ","
                 + newtf.Header.YOrigin.ToString() + "\r\n");
            sbImageInfoVital.Append("Height,Width: " + newtf.Header.Height.ToString() + ","
                + newtf.Header.Width.ToString() + "\r\n");
            sbImageInfoVital.Append("Horz,VertTransferOrder: " + newtf.Header.HorizontalTransferOrder.ToString() + ","
                + newtf.Header.VerticalTransferOrder.ToString() + "\r\n");
            sbImageInfoVital.Append("ImageType: " + newtf.Header.ImageType.ToString() + "\r\n");
            if (newtf.Footer.ExtensionAreaOffset > 0)
            {
                sbImageInfoVital.Append("GammaDenominator: " + newtf.ExtensionArea.GammaDenominator.ToString() + "\r\n");
                sbImageInfoVital.Append("GammaNumerator: " + newtf.ExtensionArea.GammaNumerator.ToString() + "\r\n");
                sbImageInfoVital.Append("GammaRatio: " + newtf.ExtensionArea.GammaRatio.ToString() + "\r\n");
                sbImageInfoVital.Append("PixelAspectRatio: " + newtf.ExtensionArea.PixelAspectRatio.ToString() + "\r\n");
                sbImageInfoVital.Append("PixelAspectRatioDenominator: " + newtf.ExtensionArea.PixelAspectRatioDenominator.ToString() + "\r\n");
                sbImageInfoVital.Append("PixelAspectRatioNumerator: " + newtf.ExtensionArea.PixelAspectRatioNumerator.ToString() + "\r\n");
                sbImageInfoVital.Append("PostageStampOffset: " + newtf.ExtensionArea.PostageStampOffset.ToString() + "\r\n");
                sbImageInfoVital.Append("ScanLineOffset: " + newtf.ExtensionArea.ScanLineOffset.ToString() + "\r\n");
            }
            string strThisVital = sbImageInfoVital.ToString();
            if (strThisVital != mFirstHeader.strTGAVitalData) blreturn = false;
            // I had memory problems when I did this compare for 444 TGA files,
            // so each time, I'm killing TGA object.
            newtf.Dispose();
            return blreturn;
        }
    }
}
