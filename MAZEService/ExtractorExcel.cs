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
    class ExtractorExcel : ExtractorInterface
    {

        String ConfigName, FilePath, Host, User, Password, Destination, Period, NamePrefix, ModifiedOnly;
        private System.Timers.Timer timer = null;
        

        public ExtractorExcel(string mConfigName)
        {
            ConfigName = mConfigName;
            FilePath = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_FilePath);
            Host = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_Host);
            User = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_User);
            Password = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_Password);
            Destination = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_Destination);
            Period = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_Period);
            NamePrefix = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_NamePrefix);
            ModifiedOnly = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribExcel_ModifiedOnly);

            timer = new System.Timers.Timer();
            int x = 0;
            if (Int32.TryParse(Period, out x))
                timer.Interval = x*1000;
            else
            {
                LogFile.write_LogFile("Error parsing to (int) the Period (string) found at the ConfigFile for configuration: " + ConfigName + ". Defaulting to 30 second extraction period.");
                timer.Interval = 30000; //default time = 30 seconds
            }
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timerTick);
        }

        public void Start()
        {
            //LogFile.write_LogFile("Extraction timer started for Excel config named: " + ConfigName);
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
                filename = pathParts[2].Replace("<yyyy>", DateTime.Now.ToString("yyyy"))
                    .Replace("<yy>", DateTime.Now.ToString("yy"))
                    .Replace("<MM>", DateTime.Now.ToString("MM"));
                filefolder = pathParts[1].Replace("<yyyy>", DateTime.Now.ToString("yyyy"))
                    .Replace("<yy>", DateTime.Now.ToString("yy"))
                    .Replace("<MM>", DateTime.Now.ToString("MM"));
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
                //LogFile.write_LogFile("checking excel file: " +filepath);
                
                if (Support.FileModified(filepath) || ModifiedOnly=="0")
                {
                    FileInfo currentFileInfo = new FileInfo(filepath);
                    string tempFilePath = Support.InstalPath + "\\temp\\" + currentFileInfo.Name;
                    File.Copy(filepath, tempFilePath);
                    if (ConvertExcelFile(tempFilePath))
                    {
                        Support.RecordProcessedFile(filepath);
                    }
                    File.Delete(tempFilePath);
                }
            }
            

            return true;
        }



        
        /// <summary>
        /// Tries to convert excel file to csv. Returns TRUE in case of success.
        /// </summary>
        /// <param name="m_filePath"></param>
        /// <returns></returns>
        public bool ConvertExcelFile(string m_filePath)
        {
            string[] whitelistLines;
            bool fileinlist = false;
            List<string[]> SheetFilters = new List<string[]>();

            try
            {

                FileInfo currentFileInfo = new FileInfo(m_filePath);

                try
                {
                    Support.CreateFile(Support.WhitelistFilePrefix + ConfigName + ".txt");
                    whitelistLines = Support.getFileLines(Support.WhitelistFilePrefix + ConfigName + ".txt");
                }
                catch
                {
                    LogFile.write_LogFile("No WhiteList file was found. All files will be converted.");
                    whitelistLines = new string[] { "*;*" };
                    throw;
                }




                if (currentFileInfo.Extension == ".xlsx" || currentFileInfo.Extension == ".xls")
                {
                    try
                    {
                        string fileName = currentFileInfo.Name;
                        foreach (string currentLine in whitelistLines)
                        {
                            string[] currentLineSplit = currentLine.Split(';');
                            if (Regex.IsMatch(fileName.ToLower(), "^" + currentLineSplit[0].ToLower().Replace("*", ".*") + "$"))
                            {
                                fileinlist = true;
                                if (currentLineSplit.Length == 2)
                                {
                                    SheetFilters.Add(new string[] { currentLineSplit[1], "" });
                                }
                                else if (currentLineSplit.Length >= 3)
                                {
                                    SheetFilters.Add(new string[] { currentLineSplit[1], currentLineSplit[2] });
                                }

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        LogFile.write_LogFile("Error reading whitelist: " + e.Message);
                        //throw;
                    }

                    DataSet outputDS = new DataSet();
                    string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + m_filePath + ";" + "Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1;'";

                    using (OleDbConnection oleCon = new OleDbConnection(ConnectionString))
                    {
                        oleCon.Open();
                        OleDbCommand oleCmd = new OleDbCommand();
                        oleCmd.Connection = oleCon;


                        DataTable SheetsTable = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        foreach (DataRow current_sheet in SheetsTable.Rows)
                        {
                            string sheetName = Regex.Replace(current_sheet["TABLE_NAME"].ToString(), @"[;\'$]", "");

                            if (!fileinlist || SheetFilters.Exists(x => Regex.IsMatch(sheetName.ToLower(), "^" + x[0].ToLower().Replace("*", ".*") + "$")))
                            {
                                oleCmd.CommandText = "select * from [" + sheetName + "$]";
                                DataTable currentDataTable = new DataTable();
                                OleDbDataAdapter OleDA;
                                try
                                {
                                    OleDA = new OleDbDataAdapter(oleCmd);
                                    currentDataTable.TableName = sheetName;
                                    OleDA.Fill(currentDataTable);
                                }
                                catch
                                {
                                    continue;
                                }



                                outputDS.Tables.Add(currentDataTable);
                            }
                        }

                        oleCmd = null;
                        oleCon.Close();
                        StringBuilder outputString = new StringBuilder();
                        string cellvalue = "";

                        foreach (DataTable currentTable in outputDS.Tables)
                        {
                            outputString.Clear();
                            int firstrow = 0;
                            foreach (string[] currentfilter in SheetFilters)
                            {
                                if (Regex.IsMatch(Regex.Replace(currentTable.TableName, @"[;\'$]", ""), "^" + currentfilter[0].Replace("*", ".*") + "$"))
                                {
                                    if (!int.TryParse(currentfilter[1], out firstrow) && currentfilter[1] != "")
                                    {
                                        for (int i = 0; i < currentTable.Rows.Count; i++)
                                            for (int j = 0; j < currentTable.Columns.Count; j++)
                                                if (currentfilter[1] == currentTable.Rows[i].ItemArray[j].ToString())
                                                    firstrow = i;
                                    }
                                    break;
                                }
                            }

                            for (int i = firstrow; i < currentTable.Rows.Count; i++)
                            {
                                cellvalue = "";
                                for (int j = 0; j < currentTable.Columns.Count; j++)
                                    cellvalue += Regex.Replace(currentTable.Rows[i].ItemArray[j].ToString(), @"[;\'$|\r\n|\r|\n]", " ") + ";";
                                outputString.AppendFormat("{0}", cellvalue.TrimEnd(';'));
                                outputString.AppendLine();
                            }
                            string Outputpath = Regex.Split(Destination, @"(.*[\\|\/])([^\\|\/]*)")[1];
                            //LogFile.write_LogFile("Saving excel output to: " + currentFileInfo.Name.Split('.')[0] + "_" + Regex.Replace(currentTable.TableName, @"[;\'$]", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
                            try
                            {
                                File.AppendAllText(Outputpath + NamePrefix + currentFileInfo.Name.Split('.')[0] + "_" + Regex.Replace(currentTable.TableName, @"[:punct:]", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", outputString.ToString());
                            }
                            catch (Exception exx)
                            {
                                LogFile.write_LogFile("Error saving output for file " + currentFileInfo.Name.Split('.')[0] + " at sheet " + Regex.Replace(currentTable.TableName, @"[:punct:]", "") + " with message: " + exx.Message);
                            }

                        }
                        return true;
                    }

                }
                return false;
            }
            catch (Exception e)
            {
                LogFile.write_LogFile("Error during excel conversion, with message: " + e.Message);
                LogFile.write_LogFile("File was added to list of processed files: " + m_filePath);
                return true;
            }

        }

    }
}
