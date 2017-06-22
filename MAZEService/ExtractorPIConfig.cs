using MAZE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace MAZE
{
    class ExtractorPIConfig : ExtractorInterface
    {

        String ConfigName, Host, User, Pass, OutP, Peri, Pref, Mtrx, Port, Inte;
        private System.Timers.Timer timer = null;


        public ExtractorPIConfig(string mConfigName)
        {
            ConfigName = mConfigName;
            Host = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Host);
            Port = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Port);
            User = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_User);
            Pass = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Pass);
            OutP = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_OutP);
            Peri = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Peri);
            Pref = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Pref);
            Mtrx = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Mtrx);
            Inte = ConfigFile.read_attribute(mConfigName, ConfigFile.AttribPIConfig_Inte);

            timer = new System.Timers.Timer();
            int x = 0;
            if (Int32.TryParse(Peri, out x))
                timer.Interval = x * 1000;
            else
            {
                LogFile.write_LogFile("Error parsing to (int) the Period (string) found at the ConfigFile for configuration: " + ConfigName + ". Defaulting to 60 second extraction period.");
                timer.Interval = 60000; //default time = 30 seconds
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


        //creates the string for input file
        private string configInput()
        {
            
            string result = "";
            result = result + "@logi " + Host + "," + User + "," + Pass + "," + Port + "\r\n";
            result = result + "@maxerr 65535" + "\r\n";
            result = result + "@table pisnap" + "\r\n";
            result = result + "@mode list" + "\r\n";
            result = result + "@timf 1,F" + "\r\n";
            result = result + "@ostr tag,time,value" + "\r\n";
            result = result + "@ostr ..." + "\r\n";
            result = result + "@istr tag" + "\r\n";
            Support.CreateFile(Support.SelectedTagFilePrefix + ConfigName + ".txt");
            result = result + "@input " + Support.InstalPath + "\\" + Support.SelectedTagFilePrefix + ConfigName + ".txt" + "\r\n";
            result = result + "@endsection" + "\r\n";
            result = result + "@bye";
            
            return result;
        }

        //runs the extraction
        private bool runExtraction()
        {
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.FileName = Support.InstalPath + "resources\\piconfig.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            string input = configInput();
            process.StandardInput.Write(input);
            process.StandardInput.Flush();

            process.StandardInput.Close();
            
            string result = (process.StandardOutput.ReadToEnd());
            
            string[] results = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string finalresult = "";
            string dataline = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            string datalineout = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string datalines = "Timestamp";

            if (Mtrx == "0")
            {
                for (int i = 0; i < results.Length; i++)
                {
                    //Selects only usefull info (removes alerts, erros and other messages) 
                    if (results[i].Contains(",") == true)
                    {
                        string[] thisline = results[i].Split(',');
                        if (thisline.Length == 3)
                        {
                            DateTime timestamp;
                            if (DateTime.TryParse(thisline[1], out timestamp))
                            {
                                
                                thisline[1] = timestamp.ToString("yyyyMMdd_HHmmss");
                                if (Inte == "1")
                                    thisline[1] = datalineout;
                                finalresult = finalresult + thisline[0] + ";" + thisline[1] + ";" + thisline[2] + "\r\n";
                            }

                        }

                    }

                }
            }
            else
            {
                List<string> tags = new List<string>();
                List<string> times = new List<string>();

                //for each line from response
                for (int i = 0; i < results.Length; i++)
                {
                    //if line is data (non data line wont contain the "," char)
                    if (results[i].Contains(",") == true)
                    {
                        string[] thisline = results[i].Split(',');
                        if (thisline.Length == 3)
                        {
                            //add tag to tag array
                            if (!tags.Contains(thisline[0]))
                                tags.Add(thisline[0]);
                            //add times to time array
                            string hora = thisline[1];
                            if (Inte == "1")
                                hora = dataline;
                            if (!times.Contains(hora))
                                times.Add(hora);
                        }
                    }
                }
                //creates data table (matrix format)
                string[,] table = new string[tags.Count(), times.Count()];
                for (int i = 0; i < results.Length; i++)
                {
                    if (results[i].Contains(",") == true)
                    {
                        string[] thisline = results[i].Split(',');
                        if (thisline.Length == 3)
                        {
                            //add value to the spot on the table with corresponding tag and time
                            string hora = thisline[1];
                            if (Inte == "1")
                                hora = dataline;
                            table[tags.IndexOf(thisline[0]), times.IndexOf(hora)] = thisline[2];
                        }
                    }
                }

                foreach (var tag in tags)
                {
                    datalines += ";" + tag;
                }
                datalines += "\r\n";
                foreach (var time in times)
                {
                    DateTime timestamp;
                    if (DateTime.TryParse(time, out timestamp))
                    {
                        datalines += timestamp.ToString("yyyyMMdd_HHmmss");
                        foreach (var tag in tags)
                        {
                            string value = (table[tags.IndexOf(tag), times.IndexOf(time)] != null) ? table[tags.IndexOf(tag), times.IndexOf(time)] : "";
                            datalines += ";" + value.Replace(";", "");
                        }
                        datalines += "\r\n";
                    }
                }

                finalresult = datalines;
            }



            string Outputpath = Regex.Split(OutP, @"(.*[\\|\/])([^\\|\/]*)")[1];
            try
            {

                string filename = (Pref == "") ? ConfigName : Pref;
                File.AppendAllText(Outputpath + filename + "_" + datalineout + ".csv", finalresult);
            }
            catch (Exception exx)
            {
                LogFile.write_LogFile("Error saving output for config " + ConfigName + " with message: " + exx.Message);
            }

            process.WaitForExit();
            return true;
        }

    }
}
