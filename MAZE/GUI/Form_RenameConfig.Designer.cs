using System.Windows.Forms;

namespace MAZE.GUI
{
    partial class Form_RenameConfig
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Rename = new System.Windows.Forms.Button();
            this.textBox_ConfigName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.KeyDown += new KeyEventHandler(this.button_Rename_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(134, 51);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(68, 24);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Rename
            // 
            this.button_Rename.Location = new System.Drawing.Point(43, 51);
            this.button_Rename.Name = "button_Rename";
            this.button_Rename.Size = new System.Drawing.Size(68, 24);
            this.button_Rename.TabIndex = 4;
            this.button_Rename.Text = "Rename";
            this.button_Rename.UseVisualStyleBackColor = true;
            this.button_Rename.Click += new System.EventHandler(this.button_Rename_Click);
            // 
            // textBox_ConfigName
            // 
            this.textBox_ConfigName.Location = new System.Drawing.Point(33, 13);
            this.textBox_ConfigName.Name = "textBox_ConfigName";
            this.textBox_ConfigName.Size = new System.Drawing.Size(185, 20);
            this.textBox_ConfigName.TabIndex = 3;
            this.textBox_ConfigName.Text = "Choose a new name";
            this.textBox_ConfigName.KeyDown += new KeyEventHandler(this.Enter_Clicked);
            // 
            // Form_RenameConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 97);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Rename);
            this.Controls.Add(this.textBox_ConfigName);
            this.Name = "Form_RenameConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Renaming Config";
            this.Load += new System.EventHandler(this.Form_RenameConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Rename;
        private System.Windows.Forms.TextBox textBox_ConfigName;
    }
}