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
            this.Gravity = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.GravityUD = new System.Windows.Forms.NumericUpDown();
            this.LoadButton = new System.Windows.Forms.Button();
            this.floorLabel = new System.Windows.Forms.Label();
            this.FloorUD = new System.Windows.Forms.NumericUpDown();
            this.widthUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.heightUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.jumpUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GravityUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpUD)).BeginInit();
            this.SuspendLayout();
            // 
            // Gravity
            // 
            this.Gravity.AutoSize = true;
            this.Gravity.Location = new System.Drawing.Point(21, 13);
            this.Gravity.Name = "Gravity";
            this.Gravity.Size = new System.Drawing.Size(83, 26);
            this.Gravity.TabIndex = 1;
            this.Gravity.Text = "Gravity\r\n(Negative is Up)";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(147, 345);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(59, 37);
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
            this.GravityUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 345);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(54, 37);
            this.LoadButton.TabIndex = 4;
            this.LoadButton.Text = "Load Attributes";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // floorLabel
            // 
            this.floorLabel.AutoSize = true;
            this.floorLabel.Location = new System.Drawing.Point(22, 65);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(82, 26);
            this.floorLabel.TabIndex = 5;
            this.floorLabel.Text = "Floor Height\r\n(Higher is lower)";
            // 
            // FloorUD
            // 
            this.FloorUD.Location = new System.Drawing.Point(25, 94);
            this.FloorUD.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.FloorUD.Name = "FloorUD";
            this.FloorUD.Size = new System.Drawing.Size(80, 20);
            this.FloorUD.TabIndex = 6;
            this.FloorUD.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
            // 
            // widthUD
            // 
            this.widthUD.Location = new System.Drawing.Point(113, 42);
            this.widthUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.widthUD.Name = "widthUD";
            this.widthUD.Size = new System.Drawing.Size(80, 20);
            this.widthUD.TabIndex = 8;
            this.widthUD.Value = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Screen Width\r\n(in pixels)";
            // 
            // heightUD
            // 
            this.heightUD.Location = new System.Drawing.Point(114, 94);
            this.heightUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.heightUD.Name = "heightUD";
            this.heightUD.Size = new System.Drawing.Size(80, 20);
            this.heightUD.TabIndex = 10;
            this.heightUD.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 26);
            this.label2.TabIndex = 9;
            this.label2.Text = "Screen Height\r\n(in pixels)";
            // 
            // jumpUD
            // 
            this.jumpUD.Location = new System.Drawing.Point(26, 146);
            this.jumpUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.jumpUD.Name = "jumpUD";
            this.jumpUD.Size = new System.Drawing.Size(80, 20);
            this.jumpUD.TabIndex = 12;
            this.jumpUD.Value = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 26);
            this.label3.TabIndex = 11;
            this.label3.Text = "Jump Height\r\n(Negative is up)";
            // 
            // DefaultButton
            // 
            this.DefaultButton.Location = new System.Drawing.Point(76, 345);
            this.DefaultButton.Name = "DefaultButton";
            this.DefaultButton.Size = new System.Drawing.Size(63, 37);
            this.DefaultButton.TabIndex = 13;
            this.DefaultButton.Text = "Reset to Default";
            this.DefaultButton.UseVisualStyleBackColor = true;
            this.DefaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // ExtAttEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 394);
            this.Controls.Add(this.DefaultButton);
            this.Controls.Add(this.jumpUD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.heightUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.widthUD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FloorUD);
            this.Controls.Add(this.floorLabel);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.GravityUD);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Gravity);
            this.Name = "ExtAttEdit";
            this.Text = "External Attribute Editor";
            ((System.ComponentModel.ISupportInitialize)(this.GravityUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Gravity;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown GravityUD;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.NumericUpDown FloorUD;
        private System.Windows.Forms.NumericUpDown widthUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown heightUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown jumpUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DefaultButton;
    }
}

