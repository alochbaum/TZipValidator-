using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZipValidator
{
    class Log2File
    {
        private string strFileName;
        public void SetDirectory(string strInDirectory)
        {
            strFileName = strInDirectory + "\\tzip" + DateTime.Now.Day.ToString() + ".log";
        }
        public string WriteLine(string str2Log)
        {
            try
            {
                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter(strFileName, true);
                file.WriteLine(str2Log);

                file.Close();
                return "";
            }
            catch (System.Exception excep)
            {
                string err = "Error writing log: ";
                err += excep.Message;
                return err;
            }
        }
    }
}
