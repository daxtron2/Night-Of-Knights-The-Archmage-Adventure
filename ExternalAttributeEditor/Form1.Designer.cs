namespace ExternalAttributeEditor
{
    partial class ExtAttEdit
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Gravity = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(21, 32);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // Gravity
            // 
            this.Gravity.AutoSize = true;
            this.Gravity.Location = new System.Drawing.Point(21, 13);
            this.Gravity.Name = "Gravity";
            this.Gravity.Size = new System.Drawing.Size(35, 13);
            this.Gravity.TabIndex = 1;
            this.Gravity.Text = "label1";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(371, 340);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(83, 37);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save Attributes";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExtAttEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 389);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Gravity);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "ExtAttEdit";
            this.Text = "External Attribute Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label Gravity;
        private System.Windows.Forms.Button SaveButton;
    }
}

