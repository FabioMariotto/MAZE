namespace MAZE
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.listBox_ConfigList = new System.Windows.Forms.ListBox();
            this.button_NewConfig = new System.Windows.Forms.Button();
            this.button_DeleteConfig = new System.Windows.Forms.Button();
            this.button_RenameConfig = new System.Windows.Forms.Button();
            this.tabControl_mainTabs = new System.Windows.Forms.TabControl();
            this.tabPage_Config_Excel = new System.Windows.Forms.TabPage();
            this.checkBox_modifiedOnly_Excel = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_NamePrefix_Excel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_browseOutPath_Excel = new System.Windows.Forms.Button();
            this.button_BrowseFile_Excel = new System.Windows.Forms.Button();
            this.button_Save_Excel = new System.Windows.Forms.Button();
            this.button_SheetFilters_Excel = new System.Windows.Forms.Button();
            this.textBox_Period_Excel = new System.Windows.Forms.TextBox();
            this.textBox_outPutPath_Excel = new System.Windows.Forms.TextBox();
            this.textBox_File_Excel = new System.Windows.Forms.TextBox();
            this.textBox_Password_Excel = new System.Windows.Forms.TextBox();
            this.textBox_User_Excel = new System.Windows.Forms.TextBox();
            this.textBox_Host_Excel = new System.Windows.Forms.TextBox();
            this.tabPage_Log = new System.Windows.Forms.TabPage();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.tabPage_Config_ACCDB = new System.Windows.Forms.TabPage();
            this.checkBox_modifiedOnly_accdb = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_namePrefix_accdb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button_browseOutputFolder_accdb = new System.Windows.Forms.Button();
            this.button_openAccdb_accdb = new System.Windows.Forms.Button();
            this.button_save_accdb = new System.Windows.Forms.Button();
            this.button_tableFilters_accdb = new System.Windows.Forms.Button();
            this.textBox_period_accdb = new System.Windows.Forms.TextBox();
            this.textBox_output_accdb = new System.Windows.Forms.TextBox();
            this.textBox_accdbFile_accdb = new System.Windows.Forms.TextBox();
            this.textBox_password_accdb = new System.Windows.Forms.TextBox();
            this.textBox_user_accdb = new System.Windows.Forms.TextBox();
            this.textBox_host_accdb = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog_output_Excel = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog_ExcelFile_Excel = new System.Windows.Forms.OpenFileDialog();
            this.button_restartService = new System.Windows.Forms.Button();
            this.label_ServiceStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl_mainTabs.SuspendLayout();
            this.tabPage_Config_Excel.SuspendLayout();
            this.tabPage_Log.SuspendLayout();
            this.tabPage_Config_ACCDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_ConfigList
            // 
            this.listBox_ConfigList.FormattingEnabled = true;
            this.listBox_ConfigList.Location = new System.Drawing.Point(15, 13);
            this.listBox_ConfigList.Name = "listBox_ConfigList";
            this.listBox_ConfigList.Size = new System.Drawing.Size(146, 160);
            this.listBox_ConfigList.TabIndex = 0;
            this.listBox_ConfigList.SelectedIndexChanged += new System.EventHandler(this.listBox_ConfigList_SelectedIndexChanged);
            // 
            // button_NewConfig
            // 
            this.button_NewConfig.Location = new System.Drawing.Point(16, 182);
            this.button_NewConfig.Name = "button_NewConfig";
            this.button_NewConfig.Size = new System.Drawing.Size(146, 23);
            this.button_NewConfig.TabIndex = 1;
            this.button_NewConfig.TabStop = false;
            this.button_NewConfig.Text = "New Config";
            this.button_NewConfig.UseVisualStyleBackColor = true;
            this.button_NewConfig.Click += new System.EventHandler(this.button_NewConfig_Click);
            // 
            // button_DeleteConfig
            // 
            this.button_DeleteConfig.Location = new System.Drawing.Point(15, 240);
            this.button_DeleteConfig.Name = "button_DeleteConfig";
            this.button_DeleteConfig.Size = new System.Drawing.Size(147, 23);
            this.button_DeleteConfig.TabIndex = 1;
            this.button_DeleteConfig.TabStop = false;
            this.button_DeleteConfig.Text = "Delete Config";
            this.button_DeleteConfig.UseVisualStyleBackColor = true;
            this.button_DeleteConfig.Click += new System.EventHandler(this.button_DeleteConfig_Click);
            // 
            // button_RenameConfig
            // 
            this.button_RenameConfig.Location = new System.Drawing.Point(16, 211);
            this.button_RenameConfig.Name = "button_RenameConfig";
            this.button_RenameConfig.Size = new System.Drawing.Size(146, 23);
            this.button_RenameConfig.TabIndex = 1;
            this.button_RenameConfig.TabStop = false;
            this.button_RenameConfig.Text = "Rename Config";
            this.button_RenameConfig.UseVisualStyleBackColor = true;
            this.button_RenameConfig.Click += new System.EventHandler(this.button_RenameConfig_Click);
            // 
            // tabControl_mainTabs
            // 
            this.tabControl_mainTabs.Controls.Add(this.tabPage_Config_Excel);
            this.tabControl_mainTabs.Controls.Add(this.tabPage_Log);
            this.tabControl_mainTabs.Controls.Add(this.tabPage_Config_ACCDB);
            this.tabControl_mainTabs.Location = new System.Drawing.Point(178, 13);
            this.tabControl_mainTabs.Name = "tabControl_mainTabs";
            this.tabControl_mainTabs.SelectedIndex = 0;
            this.tabControl_mainTabs.Size = new System.Drawing.Size(678, 442);
            this.tabControl_mainTabs.TabIndex = 2;
            // 
            // tabPage_Config_Excel
            // 
            this.tabPage_Config_Excel.Controls.Add(this.checkBox_modifiedOnly_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.label8);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_NamePrefix_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.label6);
            this.tabPage_Config_Excel.Controls.Add(this.label5);
            this.tabPage_Config_Excel.Controls.Add(this.label4);
            this.tabPage_Config_Excel.Controls.Add(this.label3);
            this.tabPage_Config_Excel.Controls.Add(this.label2);
            this.tabPage_Config_Excel.Controls.Add(this.label7);
            this.tabPage_Config_Excel.Controls.Add(this.label1);
            this.tabPage_Config_Excel.Controls.Add(this.button_browseOutPath_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.button_BrowseFile_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.button_Save_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.button_SheetFilters_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_Period_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_outPutPath_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_File_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_Password_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_User_Excel);
            this.tabPage_Config_Excel.Controls.Add(this.textBox_Host_Excel);
            this.tabPage_Config_Excel.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Config_Excel.Name = "tabPage_Config_Excel";
            this.tabPage_Config_Excel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Config_Excel.Size = new System.Drawing.Size(670, 416);
            this.tabPage_Config_Excel.TabIndex = 0;
            this.tabPage_Config_Excel.Text = "Config";
            this.tabPage_Config_Excel.UseVisualStyleBackColor = true;
            // 
            // checkBox_modifiedOnly_Excel
            // 
            this.checkBox_modifiedOnly_Excel.AutoSize = true;
            this.checkBox_modifiedOnly_Excel.Location = new System.Drawing.Point(105, 250);
            this.checkBox_modifiedOnly_Excel.Name = "checkBox_modifiedOnly_Excel";
            this.checkBox_modifiedOnly_Excel.Size = new System.Drawing.Size(109, 17);
            this.checkBox_modifiedOnly_Excel.TabIndex = 8;
            this.checkBox_modifiedOnly_Excel.Text = "Modified files only";
            this.checkBox_modifiedOnly_Excel.UseVisualStyleBackColor = true;
            this.checkBox_modifiedOnly_Excel.CheckedChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "File Name Prefix";
            // 
            // textBox_NamePrefix_Excel
            // 
            this.textBox_NamePrefix_Excel.Location = new System.Drawing.Point(102, 160);
            this.textBox_NamePrefix_Excel.Name = "textBox_NamePrefix_Excel";
            this.textBox_NamePrefix_Excel.Size = new System.Drawing.Size(118, 20);
            this.textBox_NamePrefix_Excel.TabIndex = 5;
            this.textBox_NamePrefix_Excel.TextChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "[seconds]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Extraction Period";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Host IP";
            this.label2.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Output path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File";
            // 
            // button_browseOutPath_Excel
            // 
            this.button_browseOutPath_Excel.Location = new System.Drawing.Point(609, 186);
            this.button_browseOutPath_Excel.Name = "button_browseOutPath_Excel";
            this.button_browseOutPath_Excel.Size = new System.Drawing.Size(25, 20);
            this.button_browseOutPath_Excel.TabIndex = 3;
            this.button_browseOutPath_Excel.TabStop = false;
            this.button_browseOutPath_Excel.Text = "...";
            this.button_browseOutPath_Excel.UseVisualStyleBackColor = true;
            this.button_browseOutPath_Excel.Click += new System.EventHandler(this.button_browseOutPath_Excel_Click);
            // 
            // button_BrowseFile_Excel
            // 
            this.button_BrowseFile_Excel.Location = new System.Drawing.Point(609, 30);
            this.button_BrowseFile_Excel.Name = "button_BrowseFile_Excel";
            this.button_BrowseFile_Excel.Size = new System.Drawing.Size(25, 20);
            this.button_BrowseFile_Excel.TabIndex = 3;
            this.button_BrowseFile_Excel.TabStop = false;
            this.button_BrowseFile_Excel.Text = "...";
            this.button_BrowseFile_Excel.UseVisualStyleBackColor = true;
            this.button_BrowseFile_Excel.Click += new System.EventHandler(this.button_BrowseFile_Excel_Click);
            // 
            // button_Save_Excel
            // 
            this.button_Save_Excel.Enabled = false;
            this.button_Save_Excel.Location = new System.Drawing.Point(528, 381);
            this.button_Save_Excel.Name = "button_Save_Excel";
            this.button_Save_Excel.Size = new System.Drawing.Size(134, 29);
            this.button_Save_Excel.TabIndex = 9;
            this.button_Save_Excel.Text = "Saved";
            this.button_Save_Excel.UseVisualStyleBackColor = true;
            this.button_Save_Excel.Click += new System.EventHandler(this.button_Save_Excel_Click);
            // 
            // button_SheetFilters_Excel
            // 
            this.button_SheetFilters_Excel.Location = new System.Drawing.Point(102, 319);
            this.button_SheetFilters_Excel.Name = "button_SheetFilters_Excel";
            this.button_SheetFilters_Excel.Size = new System.Drawing.Size(177, 34);
            this.button_SheetFilters_Excel.TabIndex = 0;
            this.button_SheetFilters_Excel.TabStop = false;
            this.button_SheetFilters_Excel.Text = "Sheet Filters";
            this.toolTip1.SetToolTip(this.button_SheetFilters_Excel, "Excel File; Sheet Name; 1st tag name[optional] (use * or ? as wild cards)");
            this.button_SheetFilters_Excel.UseVisualStyleBackColor = true;
            this.button_SheetFilters_Excel.Click += new System.EventHandler(this.button_SheetFilters_Excel_Click);
            // 
            // textBox_Period_Excel
            // 
            this.textBox_Period_Excel.Location = new System.Drawing.Point(102, 212);
            this.textBox_Period_Excel.Name = "textBox_Period_Excel";
            this.textBox_Period_Excel.Size = new System.Drawing.Size(118, 20);
            this.textBox_Period_Excel.TabIndex = 7;
            this.textBox_Period_Excel.TextChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // textBox_outPutPath_Excel
            // 
            this.textBox_outPutPath_Excel.Location = new System.Drawing.Point(102, 186);
            this.textBox_outPutPath_Excel.Name = "textBox_outPutPath_Excel";
            this.textBox_outPutPath_Excel.Size = new System.Drawing.Size(501, 20);
            this.textBox_outPutPath_Excel.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBox_outPutPath_Excel, "Ex: C:\\Folder\\Subfolder\\ (use * or ? as wild cards)");
            this.textBox_outPutPath_Excel.TextChanged += new System.EventHandler(this.ExcelTab_excelOutPath_ValueChanged);
            // 
            // textBox_File_Excel
            // 
            this.textBox_File_Excel.Location = new System.Drawing.Point(102, 30);
            this.textBox_File_Excel.Name = "textBox_File_Excel";
            this.textBox_File_Excel.Size = new System.Drawing.Size(501, 20);
            this.textBox_File_Excel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_File_Excel, "Ex: C:\\Folder\\Subfolder\\FileName.extension (use * or ? as wild cards)");
            this.textBox_File_Excel.TextChanged += new System.EventHandler(this.ExcelTab_excelFile_ValueChanged);
            // 
            // textBox_Password_Excel
            // 
            this.textBox_Password_Excel.Location = new System.Drawing.Point(102, 108);
            this.textBox_Password_Excel.Name = "textBox_Password_Excel";
            this.textBox_Password_Excel.Size = new System.Drawing.Size(177, 20);
            this.textBox_Password_Excel.TabIndex = 4;
            this.textBox_Password_Excel.Visible = false;
            this.textBox_Password_Excel.TextChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // textBox_User_Excel
            // 
            this.textBox_User_Excel.Location = new System.Drawing.Point(102, 82);
            this.textBox_User_Excel.Name = "textBox_User_Excel";
            this.textBox_User_Excel.Size = new System.Drawing.Size(177, 20);
            this.textBox_User_Excel.TabIndex = 3;
            this.textBox_User_Excel.Visible = false;
            this.textBox_User_Excel.TextChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // textBox_Host_Excel
            // 
            this.textBox_Host_Excel.Location = new System.Drawing.Point(102, 56);
            this.textBox_Host_Excel.Name = "textBox_Host_Excel";
            this.textBox_Host_Excel.Size = new System.Drawing.Size(177, 20);
            this.textBox_Host_Excel.TabIndex = 2;
            this.textBox_Host_Excel.Visible = false;
            this.textBox_Host_Excel.TextChanged += new System.EventHandler(this.ExcelTab_AnyElement_ValueChanged);
            // 
            // tabPage_Log
            // 
            this.tabPage_Log.Controls.Add(this.richTextBox_Log);
            this.tabPage_Log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Log.Name = "tabPage_Log";
            this.tabPage_Log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Log.Size = new System.Drawing.Size(670, 416);
            this.tabPage_Log.TabIndex = 1;
            this.tabPage_Log.Text = "Log";
            this.tabPage_Log.UseVisualStyleBackColor = true;
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox_Log.Location = new System.Drawing.Point(0, 2);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_Log.Size = new System.Drawing.Size(668, 414);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            // 
            // tabPage_Config_ACCDB
            // 
            this.tabPage_Config_ACCDB.Controls.Add(this.checkBox_modifiedOnly_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.label9);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_namePrefix_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.label10);
            this.tabPage_Config_ACCDB.Controls.Add(this.label11);
            this.tabPage_Config_ACCDB.Controls.Add(this.label12);
            this.tabPage_Config_ACCDB.Controls.Add(this.label13);
            this.tabPage_Config_ACCDB.Controls.Add(this.label14);
            this.tabPage_Config_ACCDB.Controls.Add(this.label15);
            this.tabPage_Config_ACCDB.Controls.Add(this.label16);
            this.tabPage_Config_ACCDB.Controls.Add(this.button_browseOutputFolder_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.button_openAccdb_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.button_save_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.button_tableFilters_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_period_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_output_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_accdbFile_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_password_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_user_accdb);
            this.tabPage_Config_ACCDB.Controls.Add(this.textBox_host_accdb);
            this.tabPage_Config_ACCDB.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Config_ACCDB.Name = "tabPage_Config_ACCDB";
            this.tabPage_Config_ACCDB.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Config_ACCDB.Size = new System.Drawing.Size(670, 416);
            this.tabPage_Config_ACCDB.TabIndex = 2;
            this.tabPage_Config_ACCDB.Text = "Config";
            this.tabPage_Config_ACCDB.UseVisualStyleBackColor = true;
            // 
            // checkBox_modifiedOnly_accdb
            // 
            this.checkBox_modifiedOnly_accdb.AutoSize = true;
            this.checkBox_modifiedOnly_accdb.Location = new System.Drawing.Point(105, 250);
            this.checkBox_modifiedOnly_accdb.Name = "checkBox_modifiedOnly_accdb";
            this.checkBox_modifiedOnly_accdb.Size = new System.Drawing.Size(109, 17);
            this.checkBox_modifiedOnly_accdb.TabIndex = 8;
            this.checkBox_modifiedOnly_accdb.Text = "Modified files only";
            this.checkBox_modifiedOnly_accdb.UseVisualStyleBackColor = true;
            this.checkBox_modifiedOnly_accdb.CheckedChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "File Name Prefix";
            // 
            // textBox_namePrefix_accdb
            // 
            this.textBox_namePrefix_accdb.Location = new System.Drawing.Point(102, 160);
            this.textBox_namePrefix_accdb.Name = "textBox_namePrefix_accdb";
            this.textBox_namePrefix_accdb.Size = new System.Drawing.Size(118, 20);
            this.textBox_namePrefix_accdb.TabIndex = 5;
            this.textBox_namePrefix_accdb.TextChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "[seconds]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Extraction Period";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Password";
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "User";
            this.label13.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Host IP";
            this.label14.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 192);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Output path";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "ACCDB File";
            // 
            // button_browseOutputFolder_accdb
            // 
            this.button_browseOutputFolder_accdb.Location = new System.Drawing.Point(609, 186);
            this.button_browseOutputFolder_accdb.Name = "button_browseOutputFolder_accdb";
            this.button_browseOutputFolder_accdb.Size = new System.Drawing.Size(25, 20);
            this.button_browseOutputFolder_accdb.TabIndex = 3;
            this.button_browseOutputFolder_accdb.TabStop = false;
            this.button_browseOutputFolder_accdb.Text = "...";
            this.button_browseOutputFolder_accdb.UseVisualStyleBackColor = true;
            this.button_browseOutputFolder_accdb.Click += new System.EventHandler(this.button_browseOutPath_accdb_Click);
            // 
            // button_openAccdb_accdb
            // 
            this.button_openAccdb_accdb.Location = new System.Drawing.Point(609, 30);
            this.button_openAccdb_accdb.Name = "button_openAccdb_accdb";
            this.button_openAccdb_accdb.Size = new System.Drawing.Size(25, 20);
            this.button_openAccdb_accdb.TabIndex = 3;
            this.button_openAccdb_accdb.TabStop = false;
            this.button_openAccdb_accdb.Text = "...";
            this.button_openAccdb_accdb.UseVisualStyleBackColor = true;
            this.button_openAccdb_accdb.Click += new System.EventHandler(this.button_BrowseFile_accdb_Click);
            // 
            // button_save_accdb
            // 
            this.button_save_accdb.Enabled = false;
            this.button_save_accdb.Location = new System.Drawing.Point(528, 381);
            this.button_save_accdb.Name = "button_save_accdb";
            this.button_save_accdb.Size = new System.Drawing.Size(134, 29);
            this.button_save_accdb.TabIndex = 9;
            this.button_save_accdb.Text = "Saved";
            this.button_save_accdb.UseVisualStyleBackColor = true;
            this.button_save_accdb.Click += new System.EventHandler(this.button_Save_accdb_click);
            // 
            // button_tableFilters_accdb
            // 
            this.button_tableFilters_accdb.Location = new System.Drawing.Point(102, 319);
            this.button_tableFilters_accdb.Name = "button_tableFilters_accdb";
            this.button_tableFilters_accdb.Size = new System.Drawing.Size(177, 34);
            this.button_tableFilters_accdb.TabIndex = 0;
            this.button_tableFilters_accdb.TabStop = false;
            this.button_tableFilters_accdb.Text = "Table Filters";
            this.button_tableFilters_accdb.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolTip1.SetToolTip(this.button_tableFilters_accdb, "ACCDB file name; table Name; tag for filter[optional]; range[optional]\r\n(use * or" +
        " ? as wild cards)");
            this.button_tableFilters_accdb.UseVisualStyleBackColor = true;
            this.button_tableFilters_accdb.Click += new System.EventHandler(this.button_tableFilters_accdb_Click);
            // 
            // textBox_period_accdb
            // 
            this.textBox_period_accdb.Location = new System.Drawing.Point(102, 212);
            this.textBox_period_accdb.Name = "textBox_period_accdb";
            this.textBox_period_accdb.Size = new System.Drawing.Size(118, 20);
            this.textBox_period_accdb.TabIndex = 7;
            this.textBox_period_accdb.TextChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // textBox_output_accdb
            // 
            this.textBox_output_accdb.Location = new System.Drawing.Point(102, 186);
            this.textBox_output_accdb.Name = "textBox_output_accdb";
            this.textBox_output_accdb.Size = new System.Drawing.Size(501, 20);
            this.textBox_output_accdb.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBox_output_accdb, "Ex: C:\\Folder\\Subfolder\\ (use * or ? as wild cards)");
            this.textBox_output_accdb.TextChanged += new System.EventHandler(this.accdbTab_OutPath_ValueChanged);
            // 
            // textBox_accdbFile_accdb
            // 
            this.textBox_accdbFile_accdb.Location = new System.Drawing.Point(102, 30);
            this.textBox_accdbFile_accdb.Name = "textBox_accdbFile_accdb";
            this.textBox_accdbFile_accdb.Size = new System.Drawing.Size(501, 20);
            this.textBox_accdbFile_accdb.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_accdbFile_accdb, "Ex: C:\\Folder\\Subfolder\\FileName.extension (use * or ? as wild cards)");
            this.textBox_accdbFile_accdb.TextChanged += new System.EventHandler(this.accdbTab_accdbFile_ValueChanged);
            // 
            // textBox_password_accdb
            // 
            this.textBox_password_accdb.Location = new System.Drawing.Point(102, 108);
            this.textBox_password_accdb.Name = "textBox_password_accdb";
            this.textBox_password_accdb.Size = new System.Drawing.Size(177, 20);
            this.textBox_password_accdb.TabIndex = 4;
            this.textBox_password_accdb.Visible = false;
            this.textBox_password_accdb.TextChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // textBox_user_accdb
            // 
            this.textBox_user_accdb.Location = new System.Drawing.Point(102, 82);
            this.textBox_user_accdb.Name = "textBox_user_accdb";
            this.textBox_user_accdb.Size = new System.Drawing.Size(177, 20);
            this.textBox_user_accdb.TabIndex = 3;
            this.textBox_user_accdb.Visible = false;
            this.textBox_user_accdb.TextChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // textBox_host_accdb
            // 
            this.textBox_host_accdb.Location = new System.Drawing.Point(102, 56);
            this.textBox_host_accdb.Name = "textBox_host_accdb";
            this.textBox_host_accdb.Size = new System.Drawing.Size(177, 20);
            this.textBox_host_accdb.TabIndex = 2;
            this.textBox_host_accdb.Visible = false;
            this.textBox_host_accdb.TextChanged += new System.EventHandler(this.accdbTab_AnyElement_ValueChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // button_restartService
            // 
            this.button_restartService.Enabled = false;
            this.button_restartService.Location = new System.Drawing.Point(17, 421);
            this.button_restartService.Name = "button_restartService";
            this.button_restartService.Size = new System.Drawing.Size(145, 23);
            this.button_restartService.TabIndex = 1;
            this.button_restartService.TabStop = false;
            this.button_restartService.Text = "Start Service";
            this.button_restartService.UseVisualStyleBackColor = true;
            this.button_restartService.Click += new System.EventHandler(this.button_restartService_Click);
            // 
            // label_ServiceStatus
            // 
            this.label_ServiceStatus.AutoSize = true;
            this.label_ServiceStatus.Location = new System.Drawing.Point(41, 403);
            this.label_ServiceStatus.Name = "label_ServiceStatus";
            this.label_ServiceStatus.Size = new System.Drawing.Size(86, 13);
            this.label_ServiceStatus.TabIndex = 3;
            this.label_ServiceStatus.Text = "Service Stopped";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MAZE.Properties.Resources.maze_logo;
            this.pictureBox1.InitialImage = global::MAZE.Properties.Resources.maze_logo;
            this.pictureBox1.Location = new System.Drawing.Point(35, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_ServiceStatus);
            this.Controls.Add(this.tabControl_mainTabs);
            this.Controls.Add(this.button_RenameConfig);
            this.Controls.Add(this.button_restartService);
            this.Controls.Add(this.button_DeleteConfig);
            this.Controls.Add(this.button_NewConfig);
            this.Controls.Add(this.listBox_ConfigList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "MAZE - Data Interface";
            this.tabControl_mainTabs.ResumeLayout(false);
            this.tabPage_Config_Excel.ResumeLayout(false);
            this.tabPage_Config_Excel.PerformLayout();
            this.tabPage_Log.ResumeLayout(false);
            this.tabPage_Config_ACCDB.ResumeLayout(false);
            this.tabPage_Config_ACCDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_ConfigList;
        private System.Windows.Forms.Button button_NewConfig;
        private System.Windows.Forms.Button button_DeleteConfig;
        private System.Windows.Forms.Button button_RenameConfig;
        private System.Windows.Forms.TabControl tabControl_mainTabs;
        private System.Windows.Forms.TabPage tabPage_Config_Excel;
        private System.Windows.Forms.TabPage tabPage_Log;
        private System.Windows.Forms.Button button_Save_Excel;
        private System.Windows.Forms.Button button_SheetFilters_Excel;
        private System.Windows.Forms.TextBox textBox_Period_Excel;
        private System.Windows.Forms.TextBox textBox_File_Excel;
        private System.Windows.Forms.TextBox textBox_Password_Excel;
        private System.Windows.Forms.TextBox textBox_User_Excel;
        private System.Windows.Forms.TextBox textBox_Host_Excel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_BrowseFile_Excel;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_browseOutPath_Excel;
        private System.Windows.Forms.TextBox textBox_outPutPath_Excel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_NamePrefix_Excel;
        private System.Windows.Forms.CheckBox checkBox_modifiedOnly_Excel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_output_Excel;
        private System.Windows.Forms.OpenFileDialog openFileDialog_ExcelFile_Excel;
        private System.Windows.Forms.TabPage tabPage_Config_ACCDB;
        private System.Windows.Forms.CheckBox checkBox_modifiedOnly_accdb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_namePrefix_accdb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_browseOutputFolder_accdb;
        private System.Windows.Forms.Button button_openAccdb_accdb;
        private System.Windows.Forms.Button button_save_accdb;
        private System.Windows.Forms.Button button_tableFilters_accdb;
        private System.Windows.Forms.TextBox textBox_period_accdb;
        private System.Windows.Forms.TextBox textBox_output_accdb;
        private System.Windows.Forms.TextBox textBox_accdbFile_accdb;
        private System.Windows.Forms.TextBox textBox_password_accdb;
        private System.Windows.Forms.TextBox textBox_user_accdb;
        private System.Windows.Forms.TextBox textBox_host_accdb;
        private System.Windows.Forms.Button button_restartService;
        private System.Windows.Forms.Label label_ServiceStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

