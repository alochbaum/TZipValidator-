# TZipValidator-
1	TZip Validator and Fixer for GV iTX
Version 0.9, need to add details and variables about the TZip header values to save and compare in 1.2.5 and 1.2.7.
1.1	Executive Overview
This tool is designed to monitor a watch folder for TZip files made of Targa sequences, if it finds a file it unzips the file to a TEMP folder, and adds a corner dot to TGA, rezip and move corrected file to a processed folder.
This program was created as a testing tool and is not supported by Grassvalley.
1.2	Functional Spec Logic
1.2.1	Scan Watch Folder for New TZips
This is a sleeping function that wakes up and scans watch folder for new TZip files every five seconds.
When it finds a new TZip file it uses file name to run through section 1.2.2 through 1.2.9  [Special Error: can’t scan watch folder.]  Before running file name, this function should check for Rejected and Completed Folders.  [Special Error: Can’t find Rejected and Completed Folders]  This function should also change label (lblStatus) to report: Sleeping, Folder Scanning, and Processing <File Name>.
1.2.2	Unzip to TEMP folder
Program renames the .tzip extension to zip. Then the tool uses the included command line 7z.exe to unzip file to the windows %TEMP%/TZipV.  Program stores the unzipped directory in strUzDir string. [Log step and string]  Function should clear tbLogging for each file.
1.2.3	Count and Check File names
Program sees if .TGA file is in the root directory, if not it goes one directory deeper for the .TGA files. [1.2.3 Error: TGA files not found]. If there is any subdirectory starting with “_” like “_MACOS” it will delete that directory. [1.2.3 Error: Can’t delete extra directories in TZip].  lFileCount long stores count of TGA files, it then checks the first and last numbers of the name for sequence validation. [1.2.3 Error: TZip wasn’t created in sequential order, or there are missing TGA files in the sequential order.].  
lFileStart and lFileEnd (both long) is computed for sequence as well an strSeqPath string. [Log step and values]
1.2.4	Optional Error: Move to Rejected Folder and Create Text Doc
In the case of an error the %TEMP%/strUzDir is deleted, and the renamed zip file is transferred to the rejected folder and a text file is created with the reason for the rejection, from string sent with error message. [Log step and values]
1.2.5	Jump to Middle Frame Read and Store Header Data
Store size, compression, pixel depth, byte read order for TGA file.  [Write all to log.]
1.2.6	Compute Corner or Use Default of Lower Left
 
Scan middle frame for first visible pixel; if it is in top half it is a top corner, and if it is in left half it is a left corner pixel.  If file is uncompressed find the seek location lSeekLocation (long) to write in future frames. If all frames are transparent use Lower Left pixel. [Log step and value]
1.2.7	Loop Through Sequence Compare Header, Then Seek and Write Pixel
lFileCur (long) is incremented for each file.  Header is compared. [1.2.3 Error: Frame <lFileCur> doesn’t match same values as preceding or middle frame.]  Close file and open for writing seek to corner value and write 10101009 (RGBA hex) for odd frames and 20202008 for even frames. [1.2.3 Error: Can’t write corner pixel in to <IFileCur> file.]   [Log step and final lFireCur]
1.2.8	Rezip to TEMP Folder
Rezip folder to TEMP folder [1.2.3 Error: Can’t rezip corrected folder] [Log step]
1.2.9	Move and Rename Zipped File, and Clean Up
Call API to move and rename zipped file. [1.2.3 Error: Can’t move and rename zipped folder].  Then Delete the old unzipped folder in temp folder [Error Special: can’t delete old temp folder] [Log step]
1.3	Functional Spec Interface
1.3.1	String Section for Folders
There should be a way to input 3 folders: Watch, Rejected and Completed Folders, preferably with a selection button. Variables: strWatchDir, strRejectedDir, str CompletedDir.
1.3.2	Label to Report Status (lblStatus)
This label should report: Sleeping, Folder Scanning, and Processing <File Name>
1.3.3	There Should be Large Rich Text Box which Should Mirror Logging Status
tbLogging should be multiple lines to repeat Logging Status
1.3.4	Saving State
Variables used for 1.3.1 should be saved for next use of program.


 
