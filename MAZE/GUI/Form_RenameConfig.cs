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
    public partial class Form_RenameConfig : Form
    {
        string Oldname = "";
        public string Choosen_Name = "";

        public Form_RenameConfig(string m_oldName)
        {
            InitializeComponent();
            Oldname = m_oldName;
            textBox_ConfigName.Text = Oldname;
            textBox_ConfigName.SelectionStart = 0;
            textBox_ConfigName.SelectionLength = textBox_ConfigName.Text.Length;
        }

        private void Form_RenameConfig_Load(object sender, EventArgs e)
        {

        }

        private void Enter_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                button_Cancel_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
                button_Rename_Click(null, null);
        }

        private void button_Rename_Click(object sender, EventArgs e)
        {
            if (!ConfigFile.ConfigNames().Contains(textBox_ConfigName.Text) && textBox_ConfigName.Text != Oldname) 
            {
                Choosen_Name = textBox_ConfigName.Text;
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Config name already in use.");
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
