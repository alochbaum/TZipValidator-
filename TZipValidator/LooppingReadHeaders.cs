#define DEBUG
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZipValidator
{
    class LooppingReadHeaders:MainForm
    {
        public string TestAllHeadersWithFirst(string strDir,int iTotal,ReadTargaHeader mFirstHeader)
        {
            try
            {
                int iCount = 0;
                ReadTargaHeader myReadHeader = new ReadTargaHeader();
                string[] array1 = Directory.GetFiles(strDir, "*.tga", SearchOption.AllDirectories);
                // sorting to check sequence later
                Array.Sort(array1);
                foreach (string name in array1)
                {
                    if (iCount > 0)
                    {
                        if(!myReadHeader.Compare(name, mFirstHeader))
                        {
                            return ("Error headers don't match with " +
                                Path.GetFileName(name));
                        }
                       
                    }
                    iCount++;
#if DEBUG
                    if (iCount % 30 == 0)
                    {
                        logString("I checked another 30 ending with: "+iCount.ToString());
                    }
#endif
                }
                if (iCount != iTotal)
                {
                    return("Count of files did not match sent number: "+iTotal);
                }
            }
            catch
            {
                throw;
            }
            // return an empty string on good processing
            return ("");
        }
    }
}
