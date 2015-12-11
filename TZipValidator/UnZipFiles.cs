using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZipValidator
{
    public static class UnZipFiles
    {
        //
        // using .Net System.IO.Compression.FileSystem reference the program extracts files
        //
        public static string extract2dir(string strFile, string strDir)
        {
            try
            {
                // check to see if directory exists, if found wipe out files
                if (Directory.Exists(strDir))
                {
                    System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(strDir);
                    Empty(directory);
                }
                ZipFile.ExtractToDirectory(strFile, strDir);
                return "";
            }
            catch (System.Exception excep)
            {
                string err = "Error extracting "+strFile+": ";
                err += excep.Message;
                return err;
            }
        }
        //
        // cleans out directory
        //
        private static void Empty(this System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
        
    }
}
