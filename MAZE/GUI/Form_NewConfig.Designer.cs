using System.Windows.Forms;

namespace MAZE.GUI
{
    partial class Form_NewConfig
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
            this.textBox_ConfigName = new System.Windows.Forms.TextBox();
            this.button_Create = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.comboBox_configTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox_ConfigName
            // 
            this.textBox_ConfigName.Location = new System.Drawing.Point(33, 20);
            this.textBox_ConfigName.Name = "textBox_ConfigName";
            this.textBox_ConfigName.Size = new System.Drawing.Size(185, 20);
            this.textBox_ConfigName.TabIndex = 0;
            this.textBox_ConfigName.Text = "Choose a name";
            this.textBox_ConfigName.TextChanged += new System.EventHandler(this.textBox_ConfigName_TextChanged);
            this.textBox_ConfigName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_Clicked);
            // 
            // button_Create
            // 
            this.button_Create.Location = new System.Drawing.Point(43, 91);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(68, 24);
            this.button_Create.TabIndex = 2;
            this.button_Create.Text = "Create";
            this.button_Create.UseVisualStyleBackColor = true;
            this.button_Create.Click += new System.EventHandler(this.button_Create_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(134, 91);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(68, 24);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // comboBox_configTypes
            // 
            this.comboBox_configTypes.FormattingEnabled = true;
            this.comboBox_configTypes.Location = new System.Drawing.Point(33, 55);
            this.comboBox_configTypes.Name = "comboBox_configTypes";
            this.comboBox_configTypes.Size = new System.Drawing.Size(185, 21);
            this.comboBox_configTypes.TabIndex = 1;
            this.comboBox_configTypes.Text = "Excel File";
            this.comboBox_configTypes.SelectedIndexChanged += new System.EventHandler(this.comboBox_configTypes_SelectedIndexChanged);
            this.comboBox_configTypes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_Create_Click);
            // 
            // Form_NewConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 132);
            this.Controls.Add(this.comboBox_configTypes);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Create);
            this.Controls.Add(this.textBox_ConfigName);
            this.Name = "Form_NewConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Creating New Config";
            this.Load += new System.EventHandler(this.Form_NewConfig_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_Create_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_ConfigName;
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ComboBox comboBox_configTypes;
    }
}