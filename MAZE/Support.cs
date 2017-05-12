using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MAZE
{
    public static class Support
    {
        public static string InstalPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string ProcessedFilesLog = "ProcessedFilesLog.txt";
        public static string WhitelistFilePrefix = "WhiteList_";
        public static string TablelistFilePrefix = "TableList_";

        //Methods for checking if files were already processed and updating the record of processed files.
        #region Processed Files Log Managing

        /// <summary>
        /// Returns the unique HASH string for a file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string BuildMd5Checksum(string filename)
        {
            using (var md5 = MD5.Create())
            {
                try
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        string Hash = BitConverter.ToString(md5.ComputeHash(stream));
                        stream.Close();
                        stream.Dispose();
                        return Hash;
                    }
                }
                catch (IOException ex)
                {
                    LogFile.write_LogFile("Error building HASH for processed file: "+ ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns TRUE if file was modified or if it's new.
        /// </summary>
        /// <param name="m_filePath">Full file path like: C:\\folder\\file.xls</param>
        /// <returns></returns>
        public static bool FileModified(string m_filePath)
        {
            Support.CreateFile(ProcessedFilesLog); // if file already exists, nothing is done.
            string[] ProcessedFiles = Support.getFileLines(ProcessedFilesLog);

            foreach (string ProcessedFile in ProcessedFiles)
            {
                if (ProcessedFile.Split(';')[0] == m_filePath && ProcessedFile.Split().Length > 1)
                {
                    try
                    {
                        FileInfo FileInfo = new FileInfo(m_filePath);
                        //LogFile.write_LogFile("FileModified?: " + FileInfo.LastWriteTime.ToString() + "==" + ProcessedFile.Split(';')[1] + " e " + (BuildMd5Checksum(m_filePath) + "==" + ProcessedFile.Split(';')[2]));
                        if (FileInfo.Name.Contains("~$"))
                            return false; //file is a temporary file
                        if (FileInfo.LastWriteTime.ToString() == ProcessedFile.Split(';')[1])// && (BuildMd5Checksum(m_filePath) == ProcessedFile.Split(';')[2]))
                            return false; //file wasnt modified                   
                        return true; //file was modified
                    }
                    catch (Exception e)
                    {
                        LogFile.write_LogFile("Error verifying file modification for: "+m_filePath+" with message: "+e.Message);
                        return false;
                    }
                }

            }
            //File is new
            return true;
        }

        /// <summary>
        /// Adds/update file info on processed files log.
        /// </summary>
        /// <param name="m_filePath"></param>
        public static void RecordProcessedFile(string m_filePath)
        {
            Support.CreateFile(ProcessedFilesLog); // if file already exists, nothing is done.
            string[] ProcessedFiles = Support.getFileLines(ProcessedFilesLog);
            FileInfo FileInfo = new FileInfo(m_filePath);

            for (int i = 0; i < ProcessedFiles.Length; i++)
            {
                if (ProcessedFiles[i].Split(';')[0] == m_filePath && ProcessedFiles[i].Split().Length >1)
                {
                    //update existing record for that file
                    ProcessedFiles[i] = m_filePath + ";" + FileInfo.LastWriteTime.ToString();// + ";" + BuildMd5Checksum(m_filePath);
                    //rewrite file
                    WriteAllLines(ProcessedFilesLog, ProcessedFiles, true);
                    return;
                }
            }
            //add new record to end of file
            Support.WriteOneLine(ProcessedFilesLog, m_filePath + ";" + FileInfo.LastWriteTime.ToString(), true);// + ";" + BuildMd5Checksum(m_filePath),true);
        }

        #endregion

        //Methods for managing text files located on the install path
        #region Text Files Managing

        /// <summary>
        /// Checks if a file with name FileName exists on instal path.
        /// </summary>
        /// <param name="FileName">Name of the file like: FileName.txt</param>
        /// <returns></returns>
        public static bool FileExists(string FileName)
        {
            if (File.Exists(InstalPath + "\\" + FileName))
                return true;
            return false;
        }

        /// <summary>
        /// Create a file on install path. If it already exists, do nothing.
        /// </summary>
        /// <param name="FileName">Name of the file like: FileName.txt</param>
        public static void CreateFile(string FileName)
        {
            if (!File.Exists(InstalPath + "\\" + FileName))
            {
                File.WriteAllText(InstalPath + "\\" + FileName, "");
            }
        }

        /// <summary>
        /// Deletes a file from install path.
        /// </summary>
        /// <param name="FileName">Name of the file like: FileName.txt</param>
        public static void DeleteFile(string FileName)
        {
            if (File.Exists(InstalPath + "\\" + FileName))
            {
                File.Delete(InstalPath + "\\" + FileName);
            }
        }

        /// <summary>
        /// Renames a file from install path. Does nothing if file not found.
        /// </summary>
        /// <param name="FileName">Name of the old file like: FileName.txt</param>
        /// /// <param name="NewFileName">Name of the new file like: FileName.txt</param>
        public static void RenameFile(string FileName, string NewFileName)
        {
            if (File.Exists(InstalPath + "\\" + FileName))
            {
                File.Move(InstalPath + "\\" + FileName, InstalPath + "\\" + NewFileName);
            }
        }

        /// <summary>
        /// Reads lines from a text file located on instal path.
        /// </summary>
        /// <param name="FileName"> Name of the file like: FileName.txt</param>
        /// <returns></returns>
        public static string[] getFileLines(string FileName)
        {
            try
            {
                return System.IO.File.ReadAllLines(InstalPath + "\\" + FileName, Encoding.UTF8);
            }
            catch
            {
                LogFile.write_LogFile("Fail trying to read lines from file: " + FileName);
                return new string[] { "" };
            }

        }

        /// <summary>
        ///  Write a line to a text file located on instal path. 
        /// </summary>
        /// <param name="FileName">Name of the file like: FileName.txt</param>
        /// <param name="text">Text to be written</param>
        /// <param name="append">If append=false, file will be erased before writing.</param>
        public static void WriteOneLine(string FileName, string text, bool append = true)
        {
            try
            {
                StreamWriter Writer = new StreamWriter(InstalPath + "\\" + FileName, append, Encoding.UTF8);
                if (text != "") Writer.WriteLine(text);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                LogFile.write_LogFile("Fail trying to write line to file: " + FileName);
            }
        }


        /// <summary>
        /// Writes all lines from one array to a text file located on instal path.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="lines"></param>
        /// <param name="eraseFile">If eraseFile = true, file will be erased before writing</param>
        public static void WriteAllLines(string FileName, string[] lines, bool eraseFile)
        {
            WriteOneLine(FileName, lines[0], !eraseFile);
            if (lines.Length >= 2)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    WriteOneLine(FileName, lines[i], true);
                }
            }
        }



        #endregion
    }
}
