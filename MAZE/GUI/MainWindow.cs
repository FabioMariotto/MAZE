using MAZE.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAZE
{
    public partial class Form_Main : Form
    {
        string lastWrite = "";

        public Form_Main()
        {
            InitializeComponent();
            tabControl_mainTabs.TabPages.Clear();
            UpdateConfigList();
            Thread T = new Thread(new ThreadStart(() => updateGUI()));
            T.IsBackground = true;
            T.Start();

            

        }

        //Commnn controls for the main window
        #region Main window controls

       //updates the GUI periodcally on a separate thread
        private void updateGUI()
        {
            
            while (true)
            {
                updateLOG();

                updateServiceStatus();

                System.Threading.Thread.Sleep(2000);
            }
        }

        #region Service Related Controls
        //update service status on GUI
        private void updateServiceStatus()
        {
            ServiceController serviceController = new ServiceController("MAZE");

            if (serviceController.Status.Equals(ServiceControllerStatus.Running))
            {
             
                this.button_restartService.Click -= new System.EventHandler(this.button_restartService_Click);
                this.button_restartService.Click += new System.EventHandler(this.button_stopService_Click);
                if (label_ServiceStatus.Text == "Service Running")
                    label_ServiceStatus.Text = "<Service Running>";
                else
                    label_ServiceStatus.Text = "Service Running";
                label_ServiceStatus.ForeColor = Color.Green;
                button_restartService.Text = "Stop Service";
                button_restartService.Enabled = true;

            }
                
            else if (serviceController.Status.Equals(ServiceControllerStatus.Stopped))
            {
                
                this.button_restartService.Click -= new System.EventHandler(this.button_stopService_Click);
                this.button_restartService.Click += new System.EventHandler(this.button_restartService_Click);
                label_ServiceStatus.Text = "Service Stopped";
                label_ServiceStatus.ForeColor = Color.DarkRed;
                button_restartService.Text = "Start Service";
                button_restartService.Enabled = true;
            }
        }

        private void button_restartService_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController("MAZE");
            try
            {

                if ((serviceController.Status.Equals(ServiceControllerStatus.Stopped)))
                {
                    serviceController.Start();
                    button_restartService.Text = "Starting...";
                    button_restartService.Enabled = false;
                    serviceController.WaitForStatus(ServiceControllerStatus.Running);
                }

            }
            catch (Exception exc)
            {
                LogFile.write_LogFile("Error trying to restart service: " + exc.Message);
            }
        }

        private void button_stopService_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController("MAZE");
            try
            {

                if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)))
                {
                    button_restartService.Text = "Stopping...";
                    button_restartService.Enabled = false;
                    serviceController.Stop();
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            catch (Exception exc)
            {
                LogFile.write_LogFile("Error trying to stop service:" + exc.Message);
            }
        }
        #endregion

        //updates the log window 
        private void updateLOG()
        {
            Support.CreateFile(LogFile.LogFilename);

            FileInfo FileInfo = new FileInfo(LogFile.LogFilePath);
            if (FileInfo.LastWriteTime.ToString() != lastWrite)
            {
                lastWrite = FileInfo.LastWriteTime.ToString();
                string text = "";
                string[] lines = Support.getFileLines(LogFile.LogFilename);
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    text = text + lines[i] + Environment.NewLine;
                }
                richTextBox_Log.Text = text;
            }
        }


        //User clicks on CREATE NEW CONFIG button
        private void button_NewConfig_Click(object sender, EventArgs e)
        {
            Form_NewConfig formnewConfig = new Form_NewConfig();
            formnewConfig.ShowDialog();
            if (formnewConfig.DialogResult == DialogResult.OK)
            {

                tabControl_mainTabs.TabPages.Clear();

                if (formnewConfig.Choosen_Config == ConfigFile.TypeConfig_Excel)
                {
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_Excel);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);
            
                    ConfigFile.CreateNewConfig(formnewConfig.Choosen_Name);
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribExcel_Host, "127.0.0.1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribExcel_Period, "5");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribExcel_ModifiedOnly, "1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.Attrib_Type, ConfigFile.TypeConfig_Excel);

                }
                else if(formnewConfig.Choosen_Config == ConfigFile.TypeConfig_ACCDB)
                {
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_ACCDB);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);

                    ConfigFile.CreateNewConfig(formnewConfig.Choosen_Name);
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribAccdb_Host, "127.0.0.1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribAccdb_Period, "60");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribAccdb_ModifiedOnly, "1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.Attrib_Type, ConfigFile.TypeConfig_ACCDB);
                }
                else if (formnewConfig.Choosen_Config == ConfigFile.TypeConfig_PIConfig)
                {
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_PIConfig);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);

                    ConfigFile.CreateNewConfig(formnewConfig.Choosen_Name);
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribPIConfig_Host, "127.0.0.1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribPIConfig_Peri, "15");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.AttribPIConfig_Mtrx, "1");
                    ConfigFile.write_attribute(formnewConfig.Choosen_Name, ConfigFile.Attrib_Type, ConfigFile.TypeConfig_PIConfig);
                }

                listBox_ConfigList.Items.Add(formnewConfig.Choosen_Name);
                listBox_ConfigList.SelectedIndex = listBox_ConfigList.Items.Count - 1;
 

            }
            formnewConfig.Close();
            formnewConfig.Dispose();
            tabControl_mainTabs.Enabled = true;
            
        }


        //User clicks on DELETE CONFIG button
        private void button_DeleteConfig_Click(object sender, EventArgs e)
        {
            if (listBox_ConfigList.SelectedIndex != -1)
            {
                if (MessageBox.Show("Delete config \"" + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + "\"?", "Deleting Config", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Support.DeleteFile(Support.WhitelistFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt");
                    Support.DeleteFile(Support.TablelistFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt");
                    Support.DeleteFile(Support.AllTagFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt");
                    Support.DeleteFile(Support.SelectedTagFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt");
                    ConfigFile.DeleteConfig(listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem));
                    UpdateConfigList();
                }
            }
        }

        
        //User clicks on RENAME CONFIG button
        private void button_RenameConfig_Click(object sender, EventArgs e)
        {
            if (listBox_ConfigList.SelectedIndex != -1)
            {
                Form_RenameConfig formRenameConfig = new Form_RenameConfig(listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem));
                formRenameConfig.ShowDialog();
                if (formRenameConfig.DialogResult == DialogResult.OK)
                {
                    int i = listBox_ConfigList.SelectedIndex;
                    Support.RenameFile(Support.WhitelistFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt", Support.WhitelistFilePrefix + formRenameConfig.Choosen_Name + ".txt");
                    Support.RenameFile(Support.TablelistFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt", Support.TablelistFilePrefix + formRenameConfig.Choosen_Name + ".txt");
                    Support.RenameFile(Support.AllTagFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt", Support.TablelistFilePrefix + formRenameConfig.Choosen_Name + ".txt");
                    Support.RenameFile(Support.SelectedTagFilePrefix + listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem) + ".txt", Support.TablelistFilePrefix + formRenameConfig.Choosen_Name + ".txt");
                    ConfigFile.RenameConfig(listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem), formRenameConfig.Choosen_Name);
                    UpdateConfigList();
                    listBox_ConfigList.SelectedIndex = i;
                }
            }
        }

        //Reloads the list of configurations from config file and update the listbox
        private void UpdateConfigList()
        {
            listBox_ConfigList.Items.Clear();
            foreach (string configName in ConfigFile.ConfigNames())
            {
                listBox_ConfigList.Items.Add(configName);
            }
            tabControl_mainTabs.Enabled = false;
            if (listBox_ConfigList.Items.Count > 0)
            {
                listBox_ConfigList.SelectedIndex = 0;
                tabControl_mainTabs.Enabled = true;
            }
        }

        //When the selected config is changed
        private void listBox_ConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_ConfigList.SelectedIndex != -1)
            {
                string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                string tipo = ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type);
                tabControl_mainTabs.TabPages.Clear();

                if (tipo == ConfigFile.TypeConfig_Excel)
                {
                    textBox_File_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_FilePath);
                    textBox_Host_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_Host);
                    textBox_User_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_User);
                    textBox_Password_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_Password);
                    textBox_outPutPath_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_Destination);
                    textBox_Period_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_Period);
                    textBox_NamePrefix_Excel.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_NamePrefix);
                    if (ConfigFile.read_attribute(configName, ConfigFile.AttribExcel_ModifiedOnly) == "1")
                        checkBox_modifiedOnly_Excel.Checked = true;
                    else
                        checkBox_modifiedOnly_Excel.Checked = false;
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_Excel);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);
                    button_Save_Excel.Enabled = false;
                    button_Save_Excel.Text = "Saved";
                }
                else if (tipo == ConfigFile.TypeConfig_ACCDB)
                {
                    textBox_accdbFile_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_FilePath);
                    textBox_host_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_Host);
                    textBox_user_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_User);
                    textBox_password_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_Password);
                    textBox_output_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_Destination);
                    textBox_period_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_Period);
                    textBox_namePrefix_accdb.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_NamePrefix);
                    if (ConfigFile.read_attribute(configName, ConfigFile.AttribAccdb_ModifiedOnly) == "1")
                        checkBox_modifiedOnly_accdb.Checked = true;
                    else
                        checkBox_modifiedOnly_accdb.Checked = false;
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_ACCDB);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);
                    button_save_accdb.Enabled = false;
                    button_save_accdb.Text = "Saved";
                }
                else if (tipo == ConfigFile.TypeConfig_PIConfig)
                {
                    textBox_PIConfig_Port.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Port);
                    textBox_PIConfig_Host.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Host);
                    textBox_PIConfig_User.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_User);
                    textBox_PIConfig_Pass.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pass);
                    textBox_PIConfig_OutP.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_OutP);
                    textBox_PIConfig_Peri.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Peri);
                    textBox_PIConfig_Pref.Text = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pref);
                    if (ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Mtrx) == "1")
                        checkBox_PIConfig_Mtrx.Checked = true;
                    else
                        checkBox_PIConfig_Mtrx.Checked = false;
                    tabControl_mainTabs.TabPages.Add(tabPage_Config_PIConfig);
                    tabControl_mainTabs.TabPages.Add(tabPage_Log);
                    button_PIConfig_Save.Enabled = false;
                    button_PIConfig_Save.Text = "Saved";
                }

            }

        }

        #endregion

        //Excel tab controls
        #region EXCEL TAB CONFIGS

        
        private void ExcelTab_excelFile_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_File_Excel.Text, @"(.*[\\|\/])([^\\|\/]*)(\.xl.?.?$)"))
                textBox_File_Excel.ForeColor = Color.Red;
            else
                textBox_File_Excel.ForeColor = Color.Black;
            ExcelTab_AnyElement_ValueChanged(sender, e);
        }
        private void ExcelTab_excelOutPath_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_outPutPath_Excel.Text, @"(.*[\\|\/])([^\\|\/]*)([\\|\/]$)"))
                textBox_outPutPath_Excel.ForeColor = Color.Red;
            else
                textBox_outPutPath_Excel.ForeColor = Color.Black;
            ExcelTab_AnyElement_ValueChanged(sender, e);
        }
        
        //Event when any text box on EXCEL tab value is changed
        private void ExcelTab_AnyElement_ValueChanged(object sender, EventArgs e)
        {
            button_Save_Excel.Enabled = true;
            button_Save_Excel.Text = "Save";
        }

        
        //when excel config is saved
        private void button_Save_Excel_Click(object sender, EventArgs e)
        {
            string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
            string tipo = ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type);

            if (tipo == ConfigFile.TypeConfig_Excel)
            {
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_FilePath, textBox_File_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_Host, textBox_Host_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_User, textBox_User_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_Password, textBox_Password_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_Destination, textBox_outPutPath_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_Period, textBox_Period_Excel.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_NamePrefix, textBox_NamePrefix_Excel.Text);
                if (checkBox_modifiedOnly_Excel.Checked == true)
                    ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_ModifiedOnly, "1");
                else
                    ConfigFile.write_attribute(configName, ConfigFile.AttribExcel_ModifiedOnly, "0");
                button_Save_Excel.Enabled = false;
                button_Save_Excel.Text = "Saved";
            }

        }

        //Opens whitelist file
        private void button_SheetFilters_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_ConfigList.SelectedIndex != -1)
                {
                    string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                    string tableFilePath = Support.InstalPath + "\\" + Support.WhitelistFilePrefix + configName + ".txt";

                    Support.CreateFile(Support.WhitelistFilePrefix + configName + ".txt");
                    Process p = Process.Start(tableFilePath);
                    //p.WaitForExit();
                    return;
                }

            }
            catch (Exception exp)
            {
                LogFile.write_LogFile("Error trying to open WhiteList: " + exp.Message);
            }
            return;
        }

        //opens browser to select output folder
        private void button_browseOutPath_Excel_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_output_Excel.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; // Directory.GetCurrentDirectory();
            folderBrowserDialog_output_Excel.Description = "Output destination folder";

            if (folderBrowserDialog_output_Excel.ShowDialog() != DialogResult.Cancel)
            {
                textBox_outPutPath_Excel.Text = folderBrowserDialog_output_Excel.SelectedPath+"\\";
            }
        }

        //opens file browser to select excel file
        private void button_BrowseFile_Excel_Click(object sender, EventArgs e)
        {
            openFileDialog_ExcelFile_Excel.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog_ExcelFile_Excel.ShowDialog() != DialogResult.Cancel)
            {
                textBox_File_Excel.Text = openFileDialog_ExcelFile_Excel.FileName;
            }
        }

        #endregion

        //ACCDB tab controls
        #region ACCDB TAB CONFIGS


        private void accdbTab_OutPath_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_output_accdb.Text, @"(.*[\\|\/])([^\\|\/]*)([\\|\/]$)"))
                textBox_output_accdb.ForeColor = Color.Red;
            else
                textBox_output_accdb.ForeColor = Color.Black;
            accdbTab_AnyElement_ValueChanged(sender, e);
        }
        private void accdbTab_accdbFile_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_accdbFile_accdb.Text, @"(.*[\\|\/])([^\\|\/]*)(\..*db$)"))
                textBox_accdbFile_accdb.ForeColor = Color.Red;
            else
                textBox_accdbFile_accdb.ForeColor = Color.Black;
            accdbTab_AnyElement_ValueChanged(sender, e);
        }

        //Event when any text box on accdb tab value is changed
        private void accdbTab_AnyElement_ValueChanged(object sender, EventArgs e)
        {
            button_save_accdb.Enabled = true;
            button_save_accdb.Text = "Save";
        }


        //when ACCDB config is saved
        private void button_Save_accdb_click(object sender, EventArgs e)
        {
            string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
            string tipo = ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type);

            if (tipo == ConfigFile.TypeConfig_ACCDB)
            {
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_FilePath, textBox_accdbFile_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_Host, textBox_host_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_User, textBox_user_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_Password, textBox_password_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_Destination, textBox_output_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_Period, textBox_period_accdb.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_NamePrefix, textBox_namePrefix_accdb.Text);
                if (checkBox_modifiedOnly_accdb.Checked == true)
                    ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_ModifiedOnly, "1");
                else
                    ConfigFile.write_attribute(configName, ConfigFile.AttribAccdb_ModifiedOnly, "0");
                button_save_accdb.Enabled = false;
                button_save_accdb.Text = "Saved";
            }

        }

        //Opens tablelist file
        private void button_tableFilters_accdb_Click(object sender, EventArgs e)
        {
            try
            {

                if (listBox_ConfigList.SelectedIndex != -1)
                {
                    string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                    string tableFilePath = Support.InstalPath + "\\" + Support.TablelistFilePrefix + configName + ".txt";

                    Support.CreateFile(Support.TablelistFilePrefix + configName + ".txt");
                    Process p = Process.Start(tableFilePath);
                    //p.WaitForExit();
                    return;
                }

            }
            catch (Exception exp)
            {
                LogFile.write_LogFile("Error trying to open TableList: "+exp.Message);
            }
            return;
        }

        //opens browser to select output folder
        private void button_browseOutPath_accdb_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_output_Excel.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; // Directory.GetCurrentDirectory();
            folderBrowserDialog_output_Excel.Description = "Output destination folder";

            if (folderBrowserDialog_output_Excel.ShowDialog() != DialogResult.Cancel)
            {
                textBox_output_accdb.Text = folderBrowserDialog_output_Excel.SelectedPath + "\\";
            }
        }

        //opens file browser to select accdb file file
        private void button_BrowseFile_accdb_Click(object sender, EventArgs e)
        {
            openFileDialog_ExcelFile_Excel.Filter = "Access 2000-2003 (*.mdb)|*.mdb|Access 2007 (*.accdb)|*accdb";
            if (openFileDialog_ExcelFile_Excel.ShowDialog() != DialogResult.Cancel)
            {
                textBox_accdbFile_accdb.Text = openFileDialog_ExcelFile_Excel.FileName;
            }
        }



        #endregion

        //PIConfig tab controls
        #region PIConfig TAB CONFIGS


       
        private void PIConfig_OutPath_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_PIConfig_OutP.Text, @"(.*[\\|\/])([^\\|\/]*)([\\|\/]$)"))
                textBox_PIConfig_OutP.ForeColor = Color.Red;
            else
                textBox_PIConfig_OutP.ForeColor = Color.Black;
            PIConfig_AnyElement_ValueChanged(sender, e);
        }
        

            private void textBox_PIConfig_Hfro_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_PIConfig_Hfro.Text, @"([\+|\-]?.*[d|m|y|mo|h|s])"))
                textBox_PIConfig_Hfro.ForeColor = Color.Red;
            else
                textBox_PIConfig_Hfro.ForeColor = Color.Black;
        }

        private void textBox_PIConfig_Htoo_ValueChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox_PIConfig_Htoo.Text, @"([\+|\-]?.*[d|m|y|mo|h|s])"))
                textBox_PIConfig_Htoo.ForeColor = Color.Red;
            else
                textBox_PIConfig_Htoo.ForeColor = Color.Black;
        }

        //Event when any text box on PIConfig tab value is changed
        private void PIConfig_AnyElement_ValueChanged(object sender, EventArgs e)
        {
            button_PIConfig_Save.Enabled = true;
            button_PIConfig_Save.Text = "Save";
        }


        //when excel config is saved
        private void button_PIConfig_Save_click(object sender, EventArgs e)
        {
            string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
            string tipo = ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type);

            if (tipo == ConfigFile.TypeConfig_PIConfig)
            {
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Host, textBox_PIConfig_Host.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_User, textBox_PIConfig_User.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Pass, textBox_PIConfig_Pass.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_OutP, textBox_PIConfig_OutP.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Peri, textBox_PIConfig_Peri.Text);
                ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Pref, textBox_PIConfig_Pref.Text);
                if (checkBox_PIConfig_Mtrx.Checked == true)
                    ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Mtrx, "1");
                else
                    ConfigFile.write_attribute(configName, ConfigFile.AttribPIConfig_Mtrx, "0");
                button_PIConfig_Save.Enabled = false;
                button_PIConfig_Save.Text = "Saved";
            }

        }

        //Opens TagList file
        private void button_PIConfig_TagList_click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_ConfigList.SelectedIndex != -1)
                {
                    string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                    string TagFilePath = Support.InstalPath + "\\" + Support.SelectedTagFilePrefix + configName + ".txt";

                    Support.CreateFile(Support.SelectedTagFilePrefix + configName + ".txt");
                    Process p = Process.Start(TagFilePath);
                    //p.WaitForExit();
                    return;
                }

            }
            catch (Exception exp)
            {
                LogFile.write_LogFile("Error trying to open Selected Tag List: " + exp.Message);
            }
            
        }

        //opens browser to select output folder
        private void button_PIConfig_browseOutP_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_output_Excel.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; // Directory.GetCurrentDirectory();
            folderBrowserDialog_output_Excel.Description = "Output destination folder";

            if (folderBrowserDialog_output_Excel.ShowDialog() != DialogResult.Cancel)
            {
                textBox_PIConfig_OutP.Text = folderBrowserDialog_output_Excel.SelectedPath + "\\";
            }
        }

        #endregion

        private void button_PIConfig_GetAllTagList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to extract tag list from PI? \r\nThis operation may take a while...", "Fetching PI tag list", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                //fetch pitags from pi
                System.Diagnostics.Process process = new System.Diagnostics.Process();

                string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);

                process.StartInfo.FileName = Support.InstalPath + "\\resources\\piconfig.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                string input = "";
                input = input + "@logi " + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Host) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_User)
                    + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pass) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Port) + "\r\n";
                input = input + "@maxerr 65535" + "\r\n";
                input = input + "@table pipoint" + "\r\n";
                input = input + "@mode list" + "\r\n";
                input = input + "@ostr tag,pointtype" + "\r\n";
                input = input + "@ostr ..." + "\r\n";
                input = input + "@select tag=*" + "\r\n";
                input = input + "@endsection" + "\r\n";
                input = input + "@bye";

                process.StandardInput.Write(input);
                process.StandardInput.Flush();

                process.StandardInput.Close();

                string result = (process.StandardOutput.ReadToEnd());

                string[] results = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string finalresult = "";
                for (int i = 0; i < results.Length; i++)
                {
                    //test to see if line is valid information or not (alert, error, etc)
                    if (results[i].Contains(",") == true)
                        finalresult = finalresult + results[i].Split(',')[0] + "\r\n";
                }


                process.WaitForExit();
                //save on the alltags file
                Support.CreateFile(Support.AllTagFilePrefix + configName + ".txt");
                try
                {
                    Support.WriteOneLine(Support.AllTagFilePrefix + configName + ".txt", finalresult, false);
                }
                catch (Exception exc)
                {
                    LogFile.write_LogFile("Error trying to save list of all PI tags: " + exc.Message);
                }
                //DateTime time = DateTime.Now;             // Use current time.
                //string format = "dd/mm/yyyy hh:mm";   // Use this format.
                
            }
        }

        private void button_PIConfig_AllTagList_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_ConfigList.SelectedIndex != -1)
                {
                    string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                    string TagFilePath = Support.InstalPath + "\\" + Support.AllTagFilePrefix + configName + ".txt";

                    Support.CreateFile(Support.AllTagFilePrefix + configName + ".txt");
                    Process p = Process.Start(TagFilePath);
                    //p.WaitForExit();
                    return;
                }

            }
            catch (Exception exp)
            {
                LogFile.write_LogFile("Error trying to open All Tags List: " + exp.Message);
            }
        }

        //make single historic extraction
        private void button_PIConfig_ExtractDataHistory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to extract history? \r\nThis operation may take a while...", "Extracting historical data", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (textBox_PIConfig_Hfro.Text != "" && textBox_PIConfig_Htoo.Text != "" && textBox_PIConfig_HPer.Text != "")
                {

                    System.Diagnostics.Process process = new System.Diagnostics.Process();

                    process.StartInfo.FileName = Support.InstalPath + "resources\\piconfig.exe";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();

                    string configName = listBox_ConfigList.GetItemText(listBox_ConfigList.SelectedItem);
                    string input = "";
                    input = input + "@logi " + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Host) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_User)
                    + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pass) + "," + ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Port) + "\r\n";
                    input = input + "@maxerr 65535" + "\r\n";
                    input = input + "@table piarc" + "\r\n";
                    input = input + "@mode list" + "\r\n";
                    input = input + "@modify count="+ textBox_PIConfig_HPer.Text + "\r\n";
                    input = input + "@modify starttime = " + textBox_PIConfig_Hfro.Text + "\r\n";
                    input = input + "@modify endtime = " + textBox_PIConfig_Htoo.Text + "\r\n";
                    input = input + "@modify mode = even" + "\r\n";
                    input = input + "@timf 1,F" + "\r\n";
                    input = input + "@ostr tag,time,value" + "\r\n";
                    input = input + "@ostr ..." + "\r\n";
                    input = input + "@istr tag,starttime,endtime" + "\r\n";
                    Support.CreateFile(Support.SelectedTagFilePrefix + configName + ".txt");
                    input = input + "@input " + Support.InstalPath + "\\" + Support.SelectedTagFilePrefix + configName + ".txt" + "\r\n";
                    input = input + "@endsection" + "\r\n";
                    input = input + "@bye";


                    process.StandardInput.Write(input);
                    process.StandardInput.Flush();

                    process.StandardInput.Close();

                    string result = (process.StandardOutput.ReadToEnd());

                    string[] results = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    string finalresult = "";
                    string header = "Timestamp";
                    string dataline = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                    //if (ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Mtrx) == "0")
                    //{
                        for (int i = 0; i < results.Length; i++)
                        {
                            //Selects only usefull info (removes alerts, erros and other messages) 
                            if (results[i].Contains(",") == true)
                                finalresult = finalresult + results[i] + "\r\n";
                        }
                    //}
                    //else
                    //{
                    //    List<string> tags=new List<string>();
                    //    List<string> times = new List<string>();
                        
                    //    for (int i = 0; i < results.Length; i++)
                    //    {
                    //        if (results[i].Contains(",") == true)
                    //        {
                    //            string[] thisline = results[i].Split(',');
                    //            if (thisline.Length == 3)
                    //            {
                    //                if (!tags.Contains(thisline[2]))
                    //                    tags.Add(thisline[2]);
                    //                if (!times.Contains(thisline[1]))
                    //                    times.Add(thisline[1]);
                    //            }
                    //        }
                    //    }
                    //    string[][] table = new string[][]();

                    //    header += ";" + thisline[0].Replace(",", "");
                    //    dataline += ";" + thisline[2].Replace(",", "");




                    //    finalresult = header + "\r\n" + dataline;
                    //}


                    process.WaitForExit();

                    string Outputpath = Regex.Split(ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_OutP), @"(.*[\\|\/])([^\\|\/]*)")[1];
                    try
                    {
                        string Pref = ConfigFile.read_attribute(configName, ConfigFile.AttribPIConfig_Pref);
                        string filename = (Pref == "") ? configName : Pref;
                        File.AppendAllText(Outputpath + filename + "_Historic_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", finalresult);
                    }
                    catch (Exception exx)
                    {
                        LogFile.write_LogFile("Error saving output for config " + configName + " with message: " + exx.Message);
                    }

                    

                }
                else
                    MessageBox.Show("You need first to select a start time, end time, and output folder.");
            }
        }
    }
}
