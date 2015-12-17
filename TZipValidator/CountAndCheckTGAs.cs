using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZipValidator
{
    public class CountAndCheckTGAs
    {
        //
        // After getting directory this returns count of TGA
        //

        // save a little time creating number array for testing
        char[] charAllNumbers = "0123456789".ToCharArray();
        public bool blError = false;
        public int iLastSequenceNum = -1, iLastNumCharInString = -1, iCountArray = 0, iFirstNumCharInString = -1;
        public string strFirstFile = "", strLastFile = "";
        public string CountCheck(string strDir)
        {
            string strReturn = "";
            try
            {
                //
                // Errors should be thrown to calling class
                //
                string[] array1 = Directory.GetFiles(strDir, "*.tga", SearchOption.AllDirectories);
                // sorting to check sequence later
                Array.Sort(array1);
                foreach (string name in array1)
                {
                    string shortname = Path.GetFileNameWithoutExtension(name);
                    if (!checkNumPos(shortname))
                    {
                        blError = true;
                        return "Error in TGA naming starting with : " + name;
                    }
                    if(iLastNumCharInString<0)
                    {
                        blError = true;
                        return "Couldn't find any number patter at end of TGA name: " + name;
                    }
                    int iSequencNum;
                    if(!Int32.TryParse(shortname.Substring(iFirstNumCharInString),
                        out iSequencNum))
                    {
                        blError = true;
                        return "Error in TGA naming starting with : " + name;
                    }
                    if(iLastSequenceNum<0)
                    {
                        iLastSequenceNum = iSequencNum;
                        // saving this for the first file header
                        strFirstFile = name;
                    }
                    else
                    {
                        iLastSequenceNum++;
                        if(iSequencNum!=iLastSequenceNum)
                        {
                            blError = true;
                            return "Error in TGA sequence numbering starting with :" + name;
                        }
                    }

                }
                //
                // Done with looping report count of array, and last name, first name is allready out
                //

                iCountArray = array1.Count();
                strLastFile = array1[iCountArray-1];
            }
            catch
            {
                blError = true;
                throw;
            }
            return strReturn;
        }
        //
        // Searches backward for first number, prevents my1.tga and my10.tga number problem
        // Also sets marker on first char num digit to determine number of zeros later
        //
        private bool checkNumPos(string strIn)
        {
            int i = strIn.LastIndexOfAny(charAllNumbers);
            // this is first run through set the base line
            if (iLastNumCharInString < 0)
            {
                // first run through setting the base number
                iLastNumCharInString = i;
                // now determine the start of the number sequence to determine number of zeros, doing this once
                for(int ii = i;i>0;ii--)
                {
                    int iTemp =strIn[ii].ToString().IndexOfAny(charAllNumbers);
                    if (iTemp != 0)break;
                    iFirstNumCharInString = ii;

                }
            }
            else
            {
                // checking is string had added digit like my1.tga growing to my10.tga
                if (i != iLastNumCharInString)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
