namespace TestMethodInstructions
{
    partial class Form1
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
            this.methodSpecificInstructionsPage1 = new MethodSpecificInstructionsPage.MethodSpecificInstructionsPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // methodSpecificInstructionsPage1
            // 
            this.methodSpecificInstructionsPage1.AutoScroll = true;
            this.methodSpecificInstructionsPage1.BackColor = System.Drawing.SystemColors.Control;
            this.methodSpecificInstructionsPage1.Dock = System.Windows.Forms.DockStyle.Top;
            this.methodSpecificInstructionsPage1.Location = new System.Drawing.Point(0, 0);
            this.methodSpecificInstructionsPage1.Name = "methodSpecificInstructionsPage1";
            this.methodSpecificInstructionsPage1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.methodSpecificInstructionsPage1.Size = new System.Drawing.Size(880, 795);
            this.methodSpecificInstructionsPage1.TabIndex = 0;
            this.methodSpecificInstructionsPage1.Load += new System.EventHandler(this.methodSpecificInstructionsPage1_Load);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(522, 814);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Form Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 843);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.methodSpecificInstructionsPage1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private MethodSpecificInstructionsPage.MethodSpecificInstructionsPage methodSpecificInstructionsPage1;
        private System.Windows.Forms.Button btnSave;
    }
}

