namespace MAZE.GUI
{
    partial class Form_PIConfig_HistExtract
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
            this.progressBar_extr = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar_extr
            // 
            this.progressBar_extr.Location = new System.Drawing.Point(12, 47);
            this.progressBar_extr.Name = "progressBar_extr";
            this.progressBar_extr.Size = new System.Drawing.Size(300, 20);
            this.progressBar_extr.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Running historical Extraction, pelase wait...";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form_PIConfig_HistExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 89);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar_extr);
            this.Name = "Form_PIConfig_HistExtract";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Doing Historical Extraction";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_PIConfig_HistExtract_FormClosed);
            this.Load += new System.EventHandler(this.Form_PIConfig_HistExtract_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_extr;
        private System.Windows.Forms.Label label1;
    }
}