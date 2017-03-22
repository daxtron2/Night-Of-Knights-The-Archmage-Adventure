﻿namespace ExternalAttributeEditor
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
            this.Gravity = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.GravityUD = new System.Windows.Forms.NumericUpDown();
            this.LoadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GravityUD)).BeginInit();
            this.SuspendLayout();
            // 
            // Gravity
            // 
            this.Gravity.AutoSize = true;
            this.Gravity.Location = new System.Drawing.Point(21, 13);
            this.Gravity.Name = "Gravity";
            this.Gravity.Size = new System.Drawing.Size(83, 26);
            this.Gravity.TabIndex = 1;
            this.Gravity.Text = "Jump Height\r\n(Negative is Up)";
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
            // GravityUD
            // 
            this.GravityUD.Location = new System.Drawing.Point(24, 42);
            this.GravityUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.GravityUD.Name = "GravityUD";
            this.GravityUD.Size = new System.Drawing.Size(80, 20);
            this.GravityUD.TabIndex = 3;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(282, 340);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(83, 37);
            this.LoadButton.TabIndex = 4;
            this.LoadButton.Text = "Load Attributes";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // ExtAttEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 389);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.GravityUD);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Gravity);
            this.Name = "ExtAttEdit";
            this.Text = "External Attribute Editor";
            ((System.ComponentModel.ISupportInitialize)(this.GravityUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Gravity;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown GravityUD;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label label1;
    }
}

