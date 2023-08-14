namespace LEDWizDemo
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwDevices = new System.Windows.Forms.ListView();
            this.colDeviceType = new System.Windows.Forms.ColumnHeader();
            this.colVendorId = new System.Windows.Forms.ColumnHeader();
            this.colProductId = new System.Windows.Forms.ColumnHeader();
            this.colVersionNumber = new System.Windows.Forms.ColumnHeader();
            this.colVendorName = new System.Windows.Forms.ColumnHeader();
            this.colProductName = new System.Windows.Forms.ColumnHeader();
            this.colSerialNumber = new System.Windows.Forms.ColumnHeader();
            this.colDevicePath = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.butAllLEDsOff = new System.Windows.Forms.Button();
            this.butAllLEDsOn = new System.Windows.Forms.Button();
            this.butAllLEDsRandom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.nudPulseSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblPulseSpeed = new System.Windows.Forms.Label();
            this.butPulse = new System.Windows.Forms.Button();
            this.rdoRampUpOn = new System.Windows.Forms.RadioButton();
            this.rdoOnRampDown = new System.Windows.Forms.RadioButton();
            this.rdoOnOff = new System.Windows.Forms.RadioButton();
            this.lblAutoPulse = new System.Windows.Forms.Label();
            this.rdoRampUpRampDown = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.trkIntensity = new System.Windows.Forms.TrackBar();
            this.nudLEDNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLEDNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvwDevices);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(481, 405);
            this.splitContainer1.SplitterDistance = 121;
            this.splitContainer1.TabIndex = 6;
            // 
            // lvwDevices
            // 
            this.lvwDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDeviceType,
            this.colVendorId,
            this.colProductId,
            this.colVersionNumber,
            this.colVendorName,
            this.colProductName,
            this.colSerialNumber,
            this.colDevicePath});
            this.lvwDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwDevices.FullRowSelect = true;
            this.lvwDevices.GridLines = true;
            this.lvwDevices.HideSelection = false;
            this.lvwDevices.Location = new System.Drawing.Point(0, 0);
            this.lvwDevices.Name = "lvwDevices";
            this.lvwDevices.Size = new System.Drawing.Size(481, 121);
            this.lvwDevices.TabIndex = 2;
            this.lvwDevices.UseCompatibleStateImageBehavior = false;
            this.lvwDevices.View = System.Windows.Forms.View.Details;
            // 
            // colDeviceType
            // 
            this.colDeviceType.Text = "Device Type";
            this.colDeviceType.Width = 83;
            // 
            // colVendorId
            // 
            this.colVendorId.Text = "Vendor Id";
            this.colVendorId.Width = 100;
            // 
            // colProductId
            // 
            this.colProductId.Text = "Product Id";
            this.colProductId.Width = 100;
            // 
            // colVersionNumber
            // 
            this.colVersionNumber.Text = "Version Number";
            this.colVersionNumber.Width = 100;
            // 
            // colVendorName
            // 
            this.colVendorName.Text = "Vendor Name";
            this.colVendorName.Width = 100;
            // 
            // colProductName
            // 
            this.colProductName.Text = "Product Name";
            this.colProductName.Width = 100;
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.Text = "Serial Number";
            this.colSerialNumber.Width = 100;
            // 
            // colDevicePath
            // 
            this.colDevicePath.Text = "Device Path";
            this.colDevicePath.Width = 100;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(481, 280);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.butAllLEDsOff);
            this.tabPage1.Controls.Add(this.butAllLEDsOn);
            this.tabPage1.Controls.Add(this.butAllLEDsRandom);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "State";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // butAllLEDsOff
            // 
            this.butAllLEDsOff.Location = new System.Drawing.Point(232, 32);
            this.butAllLEDsOff.Name = "butAllLEDsOff";
            this.butAllLEDsOff.Size = new System.Drawing.Size(106, 32);
            this.butAllLEDsOff.TabIndex = 24;
            this.butAllLEDsOff.Text = "All LEDs Off";
            this.butAllLEDsOff.UseVisualStyleBackColor = true;
            this.butAllLEDsOff.Click += new System.EventHandler(this.butAllLEDsOff_Click);
            // 
            // butAllLEDsOn
            // 
            this.butAllLEDsOn.Location = new System.Drawing.Point(123, 32);
            this.butAllLEDsOn.Name = "butAllLEDsOn";
            this.butAllLEDsOn.Size = new System.Drawing.Size(106, 32);
            this.butAllLEDsOn.TabIndex = 23;
            this.butAllLEDsOn.Text = "All LEDs On";
            this.butAllLEDsOn.UseVisualStyleBackColor = true;
            this.butAllLEDsOn.Click += new System.EventHandler(this.butAllLEDsOn_Click);
            // 
            // butAllLEDsRandom
            // 
            this.butAllLEDsRandom.Location = new System.Drawing.Point(123, 70);
            this.butAllLEDsRandom.Name = "butAllLEDsRandom";
            this.butAllLEDsRandom.Size = new System.Drawing.Size(215, 32);
            this.butAllLEDsRandom.TabIndex = 22;
            this.butAllLEDsRandom.Text = "All LEDs Random";
            this.butAllLEDsRandom.UseVisualStyleBackColor = true;
            this.butAllLEDsRandom.Click += new System.EventHandler(this.butAllLEDsRandom_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(42, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 85);
            this.panel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.nudPulseSpeed);
            this.tabPage2.Controls.Add(this.lblPulseSpeed);
            this.tabPage2.Controls.Add(this.butPulse);
            this.tabPage2.Controls.Add(this.rdoRampUpOn);
            this.tabPage2.Controls.Add(this.rdoOnRampDown);
            this.tabPage2.Controls.Add(this.rdoOnOff);
            this.tabPage2.Controls.Add(this.lblAutoPulse);
            this.tabPage2.Controls.Add(this.rdoRampUpRampDown);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.trkIntensity);
            this.tabPage2.Controls.Add(this.nudLEDNumber);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(473, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Intensity";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // nudPulseSpeed
            // 
            this.nudPulseSpeed.Location = new System.Drawing.Point(149, 208);
            this.nudPulseSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPulseSpeed.Name = "nudPulseSpeed";
            this.nudPulseSpeed.ReadOnly = true;
            this.nudPulseSpeed.Size = new System.Drawing.Size(71, 20);
            this.nudPulseSpeed.TabIndex = 25;
            this.nudPulseSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPulseSpeed.ValueChanged += new System.EventHandler(this.nudPulseSpeed_ValueChanged);
            // 
            // lblPulseSpeed
            // 
            this.lblPulseSpeed.AutoSize = true;
            this.lblPulseSpeed.Location = new System.Drawing.Point(64, 210);
            this.lblPulseSpeed.Name = "lblPulseSpeed";
            this.lblPulseSpeed.Size = new System.Drawing.Size(70, 13);
            this.lblPulseSpeed.TabIndex = 26;
            this.lblPulseSpeed.Text = "Pulse Speed:";
            // 
            // butPulse
            // 
            this.butPulse.Location = new System.Drawing.Point(323, 159);
            this.butPulse.Name = "butPulse";
            this.butPulse.Size = new System.Drawing.Size(106, 32);
            this.butPulse.TabIndex = 24;
            this.butPulse.Text = "Pulse";
            this.butPulse.UseVisualStyleBackColor = true;
            this.butPulse.Click += new System.EventHandler(this.butPulse_Click);
            // 
            // rdoRampUpOn
            // 
            this.rdoRampUpOn.AutoSize = true;
            this.rdoRampUpOn.Location = new System.Drawing.Point(149, 185);
            this.rdoRampUpOn.Name = "rdoRampUpOn";
            this.rdoRampUpOn.Size = new System.Drawing.Size(95, 17);
            this.rdoRampUpOn.TabIndex = 20;
            this.rdoRampUpOn.Text = "Ramp Up / On";
            this.rdoRampUpOn.UseVisualStyleBackColor = true;
            // 
            // rdoOnRampDown
            // 
            this.rdoOnRampDown.AutoSize = true;
            this.rdoOnRampDown.Location = new System.Drawing.Point(149, 166);
            this.rdoOnRampDown.Name = "rdoOnRampDown";
            this.rdoOnRampDown.Size = new System.Drawing.Size(109, 17);
            this.rdoOnRampDown.TabIndex = 19;
            this.rdoOnRampDown.Text = "On / Ramp Down";
            this.rdoOnRampDown.UseVisualStyleBackColor = true;
            // 
            // rdoOnOff
            // 
            this.rdoOnOff.AutoSize = true;
            this.rdoOnOff.Location = new System.Drawing.Point(149, 147);
            this.rdoOnOff.Name = "rdoOnOff";
            this.rdoOnOff.Size = new System.Drawing.Size(64, 17);
            this.rdoOnOff.TabIndex = 18;
            this.rdoOnOff.Text = "On / Off";
            this.rdoOnOff.UseVisualStyleBackColor = true;
            // 
            // lblAutoPulse
            // 
            this.lblAutoPulse.AutoSize = true;
            this.lblAutoPulse.Location = new System.Drawing.Point(73, 130);
            this.lblAutoPulse.Name = "lblAutoPulse";
            this.lblAutoPulse.Size = new System.Drawing.Size(61, 13);
            this.lblAutoPulse.TabIndex = 17;
            this.lblAutoPulse.Text = "Auto Pulse:";
            // 
            // rdoRampUpRampDown
            // 
            this.rdoRampUpRampDown.AutoSize = true;
            this.rdoRampUpRampDown.Checked = true;
            this.rdoRampUpRampDown.Location = new System.Drawing.Point(149, 128);
            this.rdoRampUpRampDown.Name = "rdoRampUpRampDown";
            this.rdoRampUpRampDown.Size = new System.Drawing.Size(140, 17);
            this.rdoRampUpRampDown.TabIndex = 16;
            this.rdoRampUpRampDown.TabStop = true;
            this.rdoRampUpRampDown.Text = "Ramp Up / Ramp Down";
            this.rdoRampUpRampDown.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Intensity:";
            // 
            // trkIntensity
            // 
            this.trkIntensity.AutoSize = false;
            this.trkIntensity.LargeChange = 4;
            this.trkIntensity.Location = new System.Drawing.Point(99, 80);
            this.trkIntensity.Maximum = 49;
            this.trkIntensity.Name = "trkIntensity";
            this.trkIntensity.Size = new System.Drawing.Size(330, 31);
            this.trkIntensity.TabIndex = 13;
            this.trkIntensity.Value = 49;
            this.trkIntensity.Scroll += new System.EventHandler(this.trkIntensity_Scroll);
            // 
            // nudLEDNumber
            // 
            this.nudLEDNumber.Location = new System.Drawing.Point(250, 34);
            this.nudLEDNumber.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudLEDNumber.Name = "nudLEDNumber";
            this.nudLEDNumber.ReadOnly = true;
            this.nudLEDNumber.Size = new System.Drawing.Size(71, 20);
            this.nudLEDNumber.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "LED Number (0 = All):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 405);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEDWiz Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLEDNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView lvwDevices;
		private System.Windows.Forms.ColumnHeader colDeviceType;
		private System.Windows.Forms.ColumnHeader colVendorId;
		private System.Windows.Forms.ColumnHeader colProductId;
		private System.Windows.Forms.ColumnHeader colVersionNumber;
		private System.Windows.Forms.ColumnHeader colVendorName;
		private System.Windows.Forms.ColumnHeader colProductName;
		private System.Windows.Forms.ColumnHeader colSerialNumber;
		private System.Windows.Forms.ColumnHeader colDevicePath;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button butAllLEDsRandom;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.RadioButton rdoRampUpOn;
		private System.Windows.Forms.RadioButton rdoOnRampDown;
		private System.Windows.Forms.RadioButton rdoOnOff;
		private System.Windows.Forms.Label lblAutoPulse;
		private System.Windows.Forms.RadioButton rdoRampUpRampDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trkIntensity;
		private System.Windows.Forms.NumericUpDown nudLEDNumber;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butAllLEDsOff;
		private System.Windows.Forms.Button butAllLEDsOn;
		private System.Windows.Forms.Button butPulse;
		private System.Windows.Forms.NumericUpDown nudPulseSpeed;
		private System.Windows.Forms.Label lblPulseSpeed;
    }
}

