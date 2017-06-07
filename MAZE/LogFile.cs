using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAZE
{
    public static class LogFile
    {

        public static string InstalPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string LogFilePath = InstalPath + "\\Log.txt";
        public static string LogFilename = "Log.txt";
        /// <summary>
        /// Returns all text from the LOG file
        /// </summary>
        /// <returns></returns>
        private static string read_LogFile()
        {

            //string LogFile = Directory.GetCurrentDirectory() + "\\Log.txt";
            


            if (File.Exists(LogFilePath) == true)
            {
                StreamReader configReader;
                configReader = new StreamReader(LogFilePath);
                string returnString = configReader.ReadToEnd();
                configReader.Close();
                return returnString;
            }
            else
            {
                File.WriteAllText(LogFilePath, "");
                return "";
            }
        }

        /// <summary>
        /// Writes text to the Log file
        /// </summary>
        /// <param name="text">text to be writen</param>
        /// <param name="append">append?(default=true)</param>
        public static void write_LogFile(string text, bool append = true)
        {

            try
            {
                


                if (File.Exists(LogFilePath) == true)
                {
                    StreamWriter configWriter;
                    configWriter = new StreamWriter(LogFilePath, append);
                    configWriter.Write("\r\n" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "; " + text);
                    configWriter.Close();
                }
                else
                {
                    File.WriteAllText(LogFilePath, text);
                }
            }
            catch { }
        }

        /// <summary>
        /// Writes exception to the log file
        /// </summary>
        /// <param name="except"></param>
        public static void write_LogFile(Exception except, bool append = true)
        {

            try
            {
                //string LogFile = Directory.GetCurrentDirectory() + "\\Log.txt";
                

                if (File.Exists(LogFilePath) == true)
                {
                    StreamWriter configWriter;
                    configWriter = new StreamWriter(LogFilePath, append);
                    configWriter.Write("\r\n" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "; " + except.Source.ToString().Trim() + ": " + except.Message.ToString().Trim());
                    configWriter.Close();
                }
                else
                {
                    File.WriteAllText(LogFilePath, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "; " + except.Source.ToString().Trim() + ": " + except.Message.ToString().Trim());
                }
            }
            catch { }
        }







    }
}
