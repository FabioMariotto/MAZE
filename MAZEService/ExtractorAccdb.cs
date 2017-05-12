using MAZE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace MAZE
{
    class ExtractorAccdb : ExtractorInterface
    {

        String ConfigName, FilePath, Host, User, Password, Destination, Period, NamePrefix, ModifiedOnly;
        private System.Timers.Timer timer = null;
        

        public ExtractorAccdb(string mConfigName)
        {
            ConfigName = mConfigName;
            FilePath = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_FilePath);
            Host = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_Host);
            User = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_User);
            Password = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_Password);
            Destination = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_Destination);
            Period = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_Period);
            NamePrefix = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_NamePrefix);
            ModifiedOnly = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribAccdb_ModifiedOnly);

            timer = new System.Timers.Timer();
            int x = 0;
            if (Int32.TryParse(Period, out x))
                timer.Interval = x*1000;
            else
            {
                LogFile.write_LogFile("Error parsing to (int) the Period (string) found at the ConfigFile for configuration: " + ConfigName + ". Defaulting to 5 min extraction period.");
                timer.Interval = 300000; //default time = 300 seconds
            }
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timerTick);
        }

        public void Start()
        {
            //LogFile.write_LogFile("Extraction timer started for Accdb config named: " + ConfigName);
            timer.Enabled = true;
            
        }

        private void timerTick(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            runExtraction();
            timer.Enabled = true;
            //Thread T = new Thread(new ThreadStart(() => runExtraction()));
            //T.IsBackground = true;
            //T.Start();
        }

        public void Stop()
        {
            //test if thread is running, and stop it
            timer.Enabled = false;
            
        }


        private bool runExtraction()
        {
            string[] FilesToExtract;

            string filename = "";
            string filefolder = "";
            string[] pathParts = Regex.Split(FilePath, @"(.*[\\|\/])([^\\|\/]*)");
            if (pathParts.Length != 4)
            {
                LogFile.write_LogFile("Bad file Path format for: "+FilePath);
                return false;
            }
            else
            {
                filename = pathParts[2];
                filefolder = pathParts[1];
            }

            try
            {
                FilesToExtract = Directory.GetFiles(filefolder, filename);
            }
            catch (Exception e)
            {
                LogFile.write_LogFile("Error trying to get files in: " + FilePath  +". ErrorMessage="+ e.Message);
                return false;
            }


            //for each file in FilesToExtract check if it has been modified and the run ConvertExcelFile(that file)
            //If conversion goes trough, add that file to processedfiles Log.
            foreach (string filepath in FilesToExtract)
            {
                //LogFile.write_LogFile("checking ACCDB file: " + filepath);
                if (Support.FileModified(filepath) || ModifiedOnly=="0")
                {
                    if (ConvertAccdbFile(filepath))
                    {
                        Support.RecordProcessedFile(filepath);
                    }
                }
            }
            

            return true;
        }




        /// <summary>
        /// Tries to convert excel file to csv. Returns TRUE in case of success.
        /// </summary>
        /// <param name="m_filePath"></param>
        /// <returns></returns>
        public bool ConvertAccdbFile(string m_filePath)
        {
            string[] tablelistLines;
            bool fileinlist = false;
            List<string[]> TableFilters = new List<string[]>();

            try
            {
                Support.CreateFile(Support.TablelistFilePrefix + ConfigName + ".txt");
                tablelistLines = Support.getFileLines(Support.TablelistFilePrefix + ConfigName + ".txt");
            }
            catch
            {
                LogFile.write_LogFile("No WhiteList file was found. All files will be converted.");
                tablelistLines = new string[] { "*;*" };
            }

            FileInfo currentFileInfo = new FileInfo(m_filePath);
            if (Regex.IsMatch(currentFileInfo.Extension, @"(.*db$)") || currentFileInfo.Extension == ".oee")
            {
                try
                {
                    string fileName = currentFileInfo.Name;
                    foreach (string currentLine in tablelistLines)
                    {
                        string[] currentLineSplit = currentLine.Split(';');
                        if (Regex.IsMatch(fileName.ToLower(), "^" + currentLineSplit[0].ToLower().Replace("*", ".*") + "$"))
                        {
                            fileinlist = true;

                            if (currentLineSplit.Length < 4)
                            {
                                //LogFile.write_LogFile("Table filter: "+currentLineSplit[0]);
                                TableFilters.Add(new string[] { currentLineSplit[1] });
                            }
                            else if (currentLineSplit.Length >= 4)
                            {
                                //LogFile.write_LogFile("Table filter: "+currentLineSplit[0]+"," + currentLineSplit[1]+"," + currentLineSplit[2]);
                                TableFilters.Add(new string[] { currentLineSplit[1], currentLineSplit[2], currentLineSplit[3] });
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    LogFile.write_LogFile(e);
                }

                //Don't extract if there's no table defined
                if (!fileinlist)
                {
                    //LogFile.write_LogFile("File não tava na lista");
                    return false;
                }

                DataSet outputDS = new DataSet();
                string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + m_filePath + ";Persist Security Info=False";

                OleDbConnection oleCon = null;
                bool OneTableExtracted = false;

                try
                {

                    using (oleCon = new OleDbConnection(ConnectionString))
                    {
                        OneTableExtracted = false;
                        oleCon.Open();
                        OleDbDataReader reader = null;
                        foreach (string[] tableFilter in TableFilters)
                        {
                            string SQL_Comand;
                            if (tableFilter.Length == 1)
                                SQL_Comand = "SELECT * FROM " + tableFilter[0] + ";";
                            else if (tableFilter.Length == 3)
                                SQL_Comand = "SELECT * FROM " + tableFilter[0] + " WHERE " + tableFilter[1] + ">(Select Max(" + tableFilter[1] + ") from " + tableFilter[0] + ")-" + tableFilter[2] + ";";
                            else break;

                            //LogFile.write_LogFile("Conectando com string: " + SQL_Comand);

                            OleDbCommand command = new OleDbCommand(SQL_Comand, oleCon);
                            //command.Parameters.AddWithValue("@1", userName);
                            reader = command.ExecuteReader();
                            string outputString = "";

                            var table = reader.GetSchemaTable();
                            var nameCol = table.Columns["ColumnName"];
                            
                            outputString = table.Rows[0][nameCol].ToString().Replace(";", "");
                            if (table.Rows.Count > 1)
                            {
                                for (int i = 1; i < table.Rows.Count; i++)
                                {
                                    outputString = outputString + ";" + table.Rows[i][nameCol].ToString().Replace(";", "");
                                }
                            }
                            outputString = outputString + Environment.NewLine;

                            while (reader.Read())
                            {
                                outputString = outputString + reader[0];
                                if (reader.FieldCount > 1)
                                {
                                    for (int i = 1; i < reader.FieldCount; i++)
                                    {
                                        outputString = outputString + ";" + reader[i].ToString().Replace(";", "");
                                    }
                                }
                                outputString = outputString + Environment.NewLine;
                            }
                            string Outputpath = Regex.Split(Destination, @"(.*[\\|\/])([^\\|\/]*)")[1];
                            //LogFile.write_LogFile("Saving excel output to: " + currentFileInfo.Name.Split('.')[0] + "_" + Regex.Replace(currentTable.TableName, @"[;\'$]", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
                            File.AppendAllText(Outputpath + NamePrefix + currentFileInfo.Name.Split('.')[0] + "_" + Regex.Replace(tableFilter[0], @"[;\'$]", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", outputString.ToString());
                            OneTableExtracted = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogFile.write_LogFile("Error during extraction for config: " + ConfigName + "at file:" + currentFileInfo.Name.Split('.')[0] + " with message: " + e.Message);
                }
                finally
                {
                    oleCon.Close();
                    oleCon.Dispose();
                    oleCon = null;
                }

                return OneTableExtracted;
            }
            return false;
        }
        

    }
}
