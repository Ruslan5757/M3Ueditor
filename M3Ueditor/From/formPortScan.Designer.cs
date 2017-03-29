﻿namespace M3Ueditor
{
    partial class formPortScan
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
            this.components = new System.ComponentModel.Container();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblStartIP = new System.Windows.Forms.Label();
            this.lblEndIP = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.ipEnd = new iptb.IPTextBox();
            this.ipStart = new iptb.IPTextBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblEndIP);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.ipEnd);
            this.groupBox4.Controls.Add(this.lblStartIP);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.ipStart);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Controls.Add(this.numericUpDown3);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 133);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scanner settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(144, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "timeout (ms):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(7, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "IP range:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(147, 102);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown2.TabIndex = 16;
            this.numericUpDown2.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(9, 102);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown3.TabIndex = 21;
            this.numericUpDown3.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            // 
            // lblStartIP
            // 
            this.lblStartIP.AutoSize = true;
            this.lblStartIP.Location = new System.Drawing.Point(6, 42);
            this.lblStartIP.Name = "lblStartIP";
            this.lblStartIP.Size = new System.Drawing.Size(45, 13);
            this.lblStartIP.TabIndex = 45;
            this.lblStartIP.Text = "Start IP:";
            // 
            // lblEndIP
            // 
            this.lblEndIP.AutoSize = true;
            this.lblEndIP.Location = new System.Drawing.Point(144, 42);
            this.lblEndIP.Name = "lblEndIP";
            this.lblEndIP.Size = new System.Drawing.Size(42, 13);
            this.lblEndIP.TabIndex = 46;
            this.lblEndIP.Text = "End IP:";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(314, 21);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 44;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            // 
            // ipEnd
            // 
            this.ipEnd.Location = new System.Drawing.Point(147, 58);
            this.ipEnd.Name = "ipEnd";
            this.ipEnd.Size = new System.Drawing.Size(135, 20);
            this.ipEnd.TabIndex = 47;
            this.ipEnd.ToolTipText = "";
            // 
            // ipStart
            // 
            this.ipStart.Location = new System.Drawing.Point(9, 58);
            this.ipStart.Name = "ipStart";
            this.ipStart.Size = new System.Drawing.Size(132, 20);
            this.ipStart.TabIndex = 44;
            this.ipStart.ToolTipText = "";
            // 
            // formPortScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 345);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.groupBox4);
            this.Name = "formPortScan";
            this.Text = "formPortScan";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.ToolTip toolTip1;
        internal iptb.IPTextBox ipStart;
        internal System.Windows.Forms.Label lblEndIP;
        internal iptb.IPTextBox ipEnd;
        internal System.Windows.Forms.Label lblStartIP;
        private System.Windows.Forms.Button btnScan;
    }
}