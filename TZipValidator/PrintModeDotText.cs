using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZipValidator
{
    public static class PrintModeDotText
    {
        //
        // This function locates the mode.txt file in the directory and returns lines or errors
        //
        public static string ReturnMode(string strDir)
        {
            string strReturn = "", strModeFile = "";
            int iCountFiles = 0;
            try
            {
                //
                // Errors should be thrown to calling class
                //
                string[] array1 = Directory.GetFiles(strDir, "*.txt", SearchOption.AllDirectories);
                foreach (string name in array1)
                {
                    if (Path.GetFileName(name).ToLower() != "mode.txt")
                    {
                        strReturn += "Extra Text File = " + name + "\r\n";
                    }
                    else
                    {
                        iCountFiles++;
                        strModeFile = name;
                        if (iCountFiles > 1)
                            strReturn += "More than one mode.txt file in TZip\r\n";
                    }
                }
                // check to see if we got anything
                if (!String.IsNullOrEmpty(strModeFile))
                {
                    // the using feature closes up the StreamReader automatically
                    using (StreamReader reader = new StreamReader(strModeFile))
                    {
                        strReturn += "---------There is Mode.txt file---------\r\n";
                        // reading line while loop
                        while (true)
                        {
                            string line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            strReturn +=line+"\r\n"; 
                        }
                    }
                }
                else
                {
                    strReturn += "Couldn't find mode.txt file";
                }
            }
            catch
            {
                throw;
            }
            return strReturn;
        }
        //
        // This function removes _MACOS or and sub directory starting with _
        // Up to 2 diretories deep
        //
        public static string RemoveUnderScoreDirectories (string dirStr)
        {
            string strReturn = "";
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(dirStr);
                DirectoryInfo[] subdirs = dInfo.GetDirectories();
                foreach(DirectoryInfo subdir in subdirs)
                {
                    if(subdir.Name.IndexOf('_')==0)
                    {
                        strReturn += "Found subdirectory with : " + subdir.FullName + " removing\r\n";
                        // wiping out directory, the true at end makes it recursive all subs and files
                        // since using a recursive command need to check if directory still exists
                        if(Directory.Exists(subdir.FullName))
                            Directory.Delete(subdir.FullName, true);
                    }
                    // check one more subdirectory deep
                    DirectoryInfo[] subsubdirs = subdir.GetDirectories();
                    foreach(DirectoryInfo subsubdir in subsubdirs)
                    {
                      if(subsubdir.Name.IndexOf('_')==0)
                        {
                            strReturn += "Found subdirectory with \"" + subsubdir.FullName + 
                                "\" programe is removing directory.\r\n";
                            // wiping out directory, the true at end makes it recursive all subs and files
                            // since using a recursive command need to check if directory still exists
                            if (Directory.Exists(subsubdir.FullName))
                                Directory.Delete(subsubdir.FullName, true);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return strReturn;
        }
    }
}
