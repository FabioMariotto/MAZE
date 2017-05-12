using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAZE.GUI
{
    public partial class Form_NewConfig : Form
    {

        public string Choosen_Name = "Choose New Name";
        public string Choosen_Config = ConfigFile.TypeConfig_Excel;

        public Form_NewConfig()
        {
            InitializeComponent();

            comboBox_configTypes.Items.Clear();
            comboBox_configTypes.Items.Add(ConfigFile.TypeConfig_Excel);
            comboBox_configTypes.Items.Add(ConfigFile.TypeConfig_ACCDB);
            comboBox_configTypes.Items.Add(ConfigFile.TypeConfig_PIConfig);
            //comboBox_configTypes.Items.Add("Another config option");
            comboBox_configTypes.SelectedItem = 0;
            comboBox_configTypes.Refresh();

            textBox_ConfigName.Text = "Choose Name";
            textBox_ConfigName.SelectionStart = 0;
            textBox_ConfigName.SelectionLength = textBox_ConfigName.Text.Length;

        }

        private void textBox_ConfigName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            
            if (!ConfigFile.ConfigNames().Contains(textBox_ConfigName.Text)) 
            {
                Choosen_Name = textBox_ConfigName.Text;
                DialogResult = DialogResult.OK;
            }                                 
            else
                MessageBox.Show("Config name already in use.");
        }
        
        private void Enter_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                button_Cancel_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
                button_Create_Click(null, null);
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            
          this.DialogResult = DialogResult.Cancel;
            
        }

        private void Form_NewConfig_Load(object sender, EventArgs e)
        {

        }


        private void comboBox_configTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choosen_Config = comboBox_configTypes.GetItemText(comboBox_configTypes.SelectedItem);
        }
    }
}
