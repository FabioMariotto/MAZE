using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAZE.GUI
{
    public partial class Form_PIConfig_HistExtract : Form
    {
        string starttime, endtime;
        DateTime startdate, enddate;
        string configName;
        double count;
        string period;
        double steps, maxLinhas = 1440, partSeconds=1;
        Thread thread;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_PIConfig_HistExtract_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                thread.Abort();
            }
            catch
            {

            }
        }

        public Form_PIConfig_HistExtract(string tstarttime, string tendtime, string tconfigName, string tcount)
        {
            InitializeComponent();
            starttime =tstarttime;
            endtime   =tendtime;
            configName=tconfigName;
            period = tcount;
        }

        private string PItime(DateTime date)
        {
            int month = date.Month;
            string moi;
            switch (month)
            {
                default:
                    moi = "JAN";
                    break;
                case 2:
                    moi = "FEB";
                    break;
                case 3:
                    moi = "MAR";
                    break;
                case 4:
                    moi = "APR";
                    break;
                case 5:
                    moi = "MAY";
                    break;
                case 6:
                    moi = "JUN";
                    break;
                case 7:
                    moi = "JUL";
                    break;
                case 8:
                    moi = "AUG";
                    break;
                case 9:
                    moi = "SEP";
                    break;
                case 10:
                    moi = "OCT";
                    break;
                case 11:
                    moi = "NOV";
                    break;
                case 12:
                    moi = "DEC";
                    break;
                case 1:
                    moi = "JAN";
                    break;
            }
            return date.Day + "-" + moi + "-" + date.Year + " " + date.ToString("HH:mm:ss");
        }

        private void Form_PIConfig_HistExtract_Load(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(starttime, out startdate) || !DateTime.TryParse(endtime, out enddate) || !double.TryParse(period, out count))
            {
                MessageBox.Show("Date format is incorrect");
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            thread = new Thread(runExtraction);
            thread.Start();
            
        }
        
        private void runExtraction() {

            
            steps = (((enddate-startdate).TotalSeconds)/count);
            if (steps < maxLinhas)
            {
                steps = 1;
                partSeconds= ((enddate - startdate).TotalSeconds);
            }
            else
            {
                steps = Math.Ceiling((((enddate - startdate).TotalSeconds) / count) / maxLinhas);
                partSeconds = Math.Ceiling(((enddate - startdate).TotalSeconds) / steps);
            }
            count = Math.Ceiling(partSeconds / count);

            startdate= startdate.AddSeconds(-partSeconds);

            progressBar_extr.Value = 1;
            for (double cstep = 1; cstep <= steps; cstep++)
            {
                startdate = startdate.AddSeconds(partSeconds);
                enddate = startdate.AddSeconds(partSeconds);

                starttime = PItime(startdate);
                endtime = PItime(enddate);
                
                //MessageBox.Show("Step:" + cstep + "/" + steps + "  starttime:" + starttime + "  endtime:" + endtime + "   (partial=" + partSeconds + ")" + "   (count=" + count + ")");

                System.Diagnostics.Process process = new System.Diagnostics.Process();

                process.StartInfo.FileName = Support.InstalPath + "resources\\piconfig.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                this.BringToFront();

                string input = "";
                string mode = (ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Inte) == "1") ? "even" : "comp";
                input = input + "@logi " + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Host) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_User)
                + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pass) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Port) + "\r\n";
                input = input + "@maxerr 65535" + "\r\n";
                input = input + "@table piarc" + "\r\n";
                input = input + "@mode list" + "\r\n";
                input = input + "@modify count=" + (count+1) + "\r\n";
                input = input + "@modify starttime = " + starttime + "\r\n";
                input = input + "@modify endtime = " + endtime + "\r\n";
                input = input + "@modify mode = " + mode + "\r\n";
                input = input + "@timf 1,F" + "\r\n";
                input = input + "@ostr tag,time,value" + "\r\n";
                input = input + "@ostr ..." + "\r\n";
                //input = input +" @ostructure tag" + "\r\n";
                //input = input + "@ostructure time" + "\r\n";
                //input = input + "@ostructure value" + "\r\n";
                //input = input + "@ostructure *" + "\r\n";
                input = input + "@istr tag,starttime,endtime" + "\r\n";
                Support.CreateFile(Support.SelectedTagFilePrefix + configName + ".txt");
                input = input + "@input " + Support.InstalPath + "\\" + Support.SelectedTagFilePrefix + configName + ".txt" + "\r\n";
                input = input + "@endsection" + "\r\n";
                input = input + "@bye";


                process.StandardInput.Write(input);
                this.BringToFront();
                process.StandardInput.Flush();
                this.BringToFront();
                process.StandardInput.Close();
                this.BringToFront();
                string result = (process.StandardOutput.ReadToEnd());

                //MessageBox.Show(result);

                string[] results = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string finalresult = "";
                string datalines = "Timestamp";


                //no matrix format
                if (ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Mtrx) == "0")
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
                                    if (thisline[2] == "Digital State")
                                        thisline[2] = "";
                                    thisline[1] = timestamp.ToString("yyyyMMdd_HHmmss");
                                    finalresult = finalresult + thisline[0] + ";" + thisline[1] + ";" + thisline[2] + "\r\n";
                                }

                            }

                        }

                    }
                }
                //matrix format
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
                                if (!times.Contains(thisline[1]))
                                    times.Add(thisline[1]);
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
                                if (thisline[2] == "Digital State")
                                    thisline[2] = "";
                                //add value to the spot on the table with corresponding tag and time
                                table[tags.IndexOf(thisline[0]), times.IndexOf(thisline[1])] = thisline[2];
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


                process.WaitForExit();

                string Outputpath = Regex.Split(ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_OutP), @"(.*[\\|\/])([^\\|\/]*)")[1];
                try
                {
                    string Pref = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pref);
                    string filename = (Pref == "") ? configName : Pref;
                    //Support.WriteOneLine(filename + "_Historic_" + startdate.ToString("yyyyMMdd_HHmmss") + "_to_" + enddate.ToString("yyyyMMdd_HHmmss") + ".csv", finalresult, false);
                    File.WriteAllText(Outputpath + filename + "_Historic_" + startdate.ToString("yyyyMMdd_HHmmss")+"_to_"+ enddate.ToString("yyyyMMdd_HHmmss") + ".csv", finalresult);
                }
                catch (Exception exx)
                {
                    LogFile.write_LogFile("Error saving output for config " + configName + " with message: " + exx.Message);
                }

                progressBar_extr.Value = ((int)Math.Ceiling((cstep / steps) * 100)>1)? (int)Math.Ceiling((cstep / steps) * 100) :1;
                this.Update();
            }

            MessageBox.Show(this, "Historical Extraction Complete!", "Success", MessageBoxButtons.OK);
            
            this.Close();
        }
    }
}
