namespace math
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.funcPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resultsPanel = new System.Windows.Forms.Panel();
            this.AsymatotWithYWayLabel = new System.Windows.Forms.Label();
            this.DomainDefinitionWayLabel = new System.Windows.Forms.Label();
            this.AsymatotsWithYShowWayButton = new System.Windows.Forms.PictureBox();
            this.DomainDefintionShowWayButton = new System.Windows.Forms.PictureBox();
            this.AsymatotWithYLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.AsymatotsWithXLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.DomainDefinitionLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.showWayCutsWithXButton = new System.Windows.Forms.PictureBox();
            this.showWayCuttecdFuncButton = new System.Windows.Forms.PictureBox();
            this.minAndMaxWayLabel = new System.Windows.Forms.Label();
            this.CutsWithXWayLabel = new System.Windows.Forms.Label();
            this.CuttedFuncLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.MaximumPointsLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MinimumPointsLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CutsTheYLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CutsTheXLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.funcPictureBox)).BeginInit();
            this.resultsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AsymatotsWithYShowWayButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DomainDefintionShowWayButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWayCutsWithXButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWayCuttecdFuncButton)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(155, 574);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.calcButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(71, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(366, 38);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zoomTrackBar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.funcPictureBox);
            this.panel1.Location = new System.Drawing.Point(499, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 563);
            this.panel1.TabIndex = 2;
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.LargeChange = 1;
            this.zoomTrackBar.Location = new System.Drawing.Point(78, 509);
            this.zoomTrackBar.Maximum = 5;
            this.zoomTrackBar.Minimum = 1;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.zoomTrackBar.Size = new System.Drawing.Size(92, 45);
            this.zoomTrackBar.TabIndex = 2;
            this.zoomTrackBar.Value = 5;
            this.zoomTrackBar.ValueChanged += new System.EventHandler(this.zoomTrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zoom:";
            // 
            // funcPictureBox
            // 
            this.funcPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.funcPictureBox.Location = new System.Drawing.Point(3, 3);
            this.funcPictureBox.Name = "funcPictureBox";
            this.funcPictureBox.Size = new System.Drawing.Size(500, 500);
            this.funcPictureBox.TabIndex = 0;
            this.funcPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "f(x)=";
            // 
            // resultsPanel
            // 
            this.resultsPanel.AutoScroll = true;
            this.resultsPanel.Controls.Add(this.AsymatotWithYWayLabel);
            this.resultsPanel.Controls.Add(this.DomainDefinitionWayLabel);
            this.resultsPanel.Controls.Add(this.AsymatotsWithYShowWayButton);
            this.resultsPanel.Controls.Add(this.DomainDefintionShowWayButton);
            this.resultsPanel.Controls.Add(this.AsymatotWithYLabel);
            this.resultsPanel.Controls.Add(this.label13);
            this.resultsPanel.Controls.Add(this.AsymatotsWithXLabel);
            this.resultsPanel.Controls.Add(this.label11);
            this.resultsPanel.Controls.Add(this.DomainDefinitionLabel);
            this.resultsPanel.Controls.Add(this.label9);
            this.resultsPanel.Controls.Add(this.showWayCutsWithXButton);
            this.resultsPanel.Controls.Add(this.showWayCuttecdFuncButton);
            this.resultsPanel.Controls.Add(this.minAndMaxWayLabel);
            this.resultsPanel.Controls.Add(this.CutsWithXWayLabel);
            this.resultsPanel.Controls.Add(this.CuttedFuncLabel);
            this.resultsPanel.Controls.Add(this.label8);
            this.resultsPanel.Controls.Add(this.MaximumPointsLabel);
            this.resultsPanel.Controls.Add(this.label7);
            this.resultsPanel.Controls.Add(this.MinimumPointsLabel);
            this.resultsPanel.Controls.Add(this.label6);
            this.resultsPanel.Controls.Add(this.CutsTheYLabel);
            this.resultsPanel.Controls.Add(this.label5);
            this.resultsPanel.Controls.Add(this.CutsTheXLabel);
            this.resultsPanel.Controls.Add(this.label3);
            this.resultsPanel.Location = new System.Drawing.Point(15, 105);
            this.resultsPanel.Name = "resultsPanel";
            this.resultsPanel.Size = new System.Drawing.Size(481, 461);
            this.resultsPanel.TabIndex = 4;
            // 
            // AsymatotWithYWayLabel
            // 
            this.AsymatotWithYWayLabel.AutoSize = true;
            this.AsymatotWithYWayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AsymatotWithYWayLabel.Location = new System.Drawing.Point(178, 393);
            this.AsymatotWithYWayLabel.Name = "AsymatotWithYWayLabel";
            this.AsymatotWithYWayLabel.Size = new System.Drawing.Size(54, 17);
            this.AsymatotWithYWayLabel.TabIndex = 37;
            this.AsymatotWithYWayLabel.Text = "label10";
            this.AsymatotWithYWayLabel.Visible = false;
            // 
            // DomainDefinitionWayLabel
            // 
            this.DomainDefinitionWayLabel.AutoSize = true;
            this.DomainDefinitionWayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DomainDefinitionWayLabel.Location = new System.Drawing.Point(85, 365);
            this.DomainDefinitionWayLabel.Name = "DomainDefinitionWayLabel";
            this.DomainDefinitionWayLabel.Size = new System.Drawing.Size(0, 17);
            this.DomainDefinitionWayLabel.TabIndex = 36;
            this.DomainDefinitionWayLabel.Visible = false;
            // 
            // AsymatotsWithYShowWayButton
            // 
            this.AsymatotsWithYShowWayButton.Image = global::math.Properties.Resources._62866_page_with_curl_icon;
            this.AsymatotsWithYShowWayButton.Location = new System.Drawing.Point(28, 192);
            this.AsymatotsWithYShowWayButton.Name = "AsymatotsWithYShowWayButton";
            this.AsymatotsWithYShowWayButton.Size = new System.Drawing.Size(25, 25);
            this.AsymatotsWithYShowWayButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AsymatotsWithYShowWayButton.TabIndex = 35;
            this.AsymatotsWithYShowWayButton.TabStop = false;
            this.AsymatotsWithYShowWayButton.Click += new System.EventHandler(this.AsymatotsWithYShowWayButton_Click);
            // 
            // DomainDefintionShowWayButton
            // 
            this.DomainDefintionShowWayButton.Image = global::math.Properties.Resources._62866_page_with_curl_icon;
            this.DomainDefintionShowWayButton.Location = new System.Drawing.Point(28, 108);
            this.DomainDefintionShowWayButton.Name = "DomainDefintionShowWayButton";
            this.DomainDefintionShowWayButton.Size = new System.Drawing.Size(25, 25);
            this.DomainDefintionShowWayButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DomainDefintionShowWayButton.TabIndex = 34;
            this.DomainDefintionShowWayButton.TabStop = false;
            this.DomainDefintionShowWayButton.Click += new System.EventHandler(this.DomainDefintionShowWayButton_Click);
            // 
            // AsymatotWithYLabel
            // 
            this.AsymatotWithYLabel.AutoSize = true;
            this.AsymatotWithYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AsymatotWithYLabel.Location = new System.Drawing.Point(236, 192);
            this.AsymatotWithYLabel.Name = "AsymatotWithYLabel";
            this.AsymatotWithYLabel.Size = new System.Drawing.Size(59, 25);
            this.AsymatotWithYLabel.TabIndex = 33;
            this.AsymatotWithYLabel.Text = "None";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(60, 192);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(164, 25);
            this.label13.TabIndex = 32;
            this.label13.Text = "Asymatots with y:";
            // 
            // AsymatotsWithXLabel
            // 
            this.AsymatotsWithXLabel.AutoSize = true;
            this.AsymatotsWithXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AsymatotsWithXLabel.Location = new System.Drawing.Point(199, 151);
            this.AsymatotsWithXLabel.Name = "AsymatotsWithXLabel";
            this.AsymatotsWithXLabel.Size = new System.Drawing.Size(59, 25);
            this.AsymatotsWithXLabel.TabIndex = 31;
            this.AsymatotsWithXLabel.Text = "None";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(164, 25);
            this.label11.TabIndex = 30;
            this.label11.Text = "Asymatots with x:";
            // 
            // DomainDefinitionLabel
            // 
            this.DomainDefinitionLabel.AutoSize = true;
            this.DomainDefinitionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DomainDefinitionLabel.Location = new System.Drawing.Point(235, 109);
            this.DomainDefinitionLabel.Name = "DomainDefinitionLabel";
            this.DomainDefinitionLabel.Size = new System.Drawing.Size(59, 25);
            this.DomainDefinitionLabel.TabIndex = 29;
            this.DomainDefinitionLabel.Text = "None";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(59, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 25);
            this.label9.TabIndex = 28;
            this.label9.Text = "Domain definition:";
            // 
            // showWayCutsWithXButton
            // 
            this.showWayCutsWithXButton.Image = global::math.Properties.Resources._62866_page_with_curl_icon;
            this.showWayCutsWithXButton.Location = new System.Drawing.Point(28, 22);
            this.showWayCutsWithXButton.Name = "showWayCutsWithXButton";
            this.showWayCutsWithXButton.Size = new System.Drawing.Size(25, 25);
            this.showWayCutsWithXButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showWayCutsWithXButton.TabIndex = 27;
            this.showWayCutsWithXButton.TabStop = false;
            this.showWayCutsWithXButton.Click += new System.EventHandler(this.showWayCutsWithXButton_Click);
            // 
            // showWayCuttecdFuncButton
            // 
            this.showWayCuttecdFuncButton.Image = global::math.Properties.Resources._62866_page_with_curl_icon;
            this.showWayCuttecdFuncButton.Location = new System.Drawing.Point(28, 235);
            this.showWayCuttecdFuncButton.Name = "showWayCuttecdFuncButton";
            this.showWayCuttecdFuncButton.Size = new System.Drawing.Size(25, 25);
            this.showWayCuttecdFuncButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showWayCuttecdFuncButton.TabIndex = 26;
            this.showWayCuttecdFuncButton.TabStop = false;
            this.showWayCuttecdFuncButton.Click += new System.EventHandler(this.showWayCuttecdFuncButton_Click);
            // 
            // minAndMaxWayLabel
            // 
            this.minAndMaxWayLabel.AutoSize = true;
            this.minAndMaxWayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minAndMaxWayLabel.Location = new System.Drawing.Point(56, 246);
            this.minAndMaxWayLabel.Name = "minAndMaxWayLabel";
            this.minAndMaxWayLabel.Size = new System.Drawing.Size(0, 17);
            this.minAndMaxWayLabel.TabIndex = 25;
            this.minAndMaxWayLabel.Visible = false;
            // 
            // CutsWithXWayLabel
            // 
            this.CutsWithXWayLabel.AutoSize = true;
            this.CutsWithXWayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CutsWithXWayLabel.Location = new System.Drawing.Point(332, 289);
            this.CutsWithXWayLabel.Name = "CutsWithXWayLabel";
            this.CutsWithXWayLabel.Size = new System.Drawing.Size(0, 17);
            this.CutsWithXWayLabel.TabIndex = 24;
            this.CutsWithXWayLabel.Visible = false;
            // 
            // CuttedFuncLabel
            // 
            this.CuttedFuncLabel.AutoSize = true;
            this.CuttedFuncLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CuttedFuncLabel.Location = new System.Drawing.Point(128, 233);
            this.CuttedFuncLabel.Name = "CuttedFuncLabel";
            this.CuttedFuncLabel.Size = new System.Drawing.Size(23, 25);
            this.CuttedFuncLabel.TabIndex = 23;
            this.CuttedFuncLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(59, 233);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "f \'(x)=";
            // 
            // MaximumPointsLabel
            // 
            this.MaximumPointsLabel.AutoSize = true;
            this.MaximumPointsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumPointsLabel.Location = new System.Drawing.Point(181, 317);
            this.MaximumPointsLabel.Name = "MaximumPointsLabel";
            this.MaximumPointsLabel.Size = new System.Drawing.Size(59, 25);
            this.MaximumPointsLabel.TabIndex = 21;
            this.MaximumPointsLabel.Text = "None";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 25);
            this.label7.TabIndex = 20;
            this.label7.Text = "Maximum points:";
            // 
            // MinimumPointsLabel
            // 
            this.MinimumPointsLabel.AutoSize = true;
            this.MinimumPointsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumPointsLabel.Location = new System.Drawing.Point(181, 274);
            this.MinimumPointsLabel.Name = "MinimumPointsLabel";
            this.MinimumPointsLabel.Size = new System.Drawing.Size(59, 25);
            this.MinimumPointsLabel.TabIndex = 19;
            this.MinimumPointsLabel.Text = "None";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 25);
            this.label6.TabIndex = 18;
            this.label6.Text = "Minimum points:";
            // 
            // CutsTheYLabel
            // 
            this.CutsTheYLabel.AutoSize = true;
            this.CutsTheYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CutsTheYLabel.Location = new System.Drawing.Point(138, 65);
            this.CutsTheYLabel.Name = "CutsTheYLabel";
            this.CutsTheYLabel.Size = new System.Drawing.Size(59, 25);
            this.CutsTheYLabel.TabIndex = 17;
            this.CutsTheYLabel.Text = "None";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Cuts the y:";
            // 
            // CutsTheXLabel
            // 
            this.CutsTheXLabel.AutoSize = true;
            this.CutsTheXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CutsTheXLabel.Location = new System.Drawing.Point(173, 22);
            this.CutsTheXLabel.Name = "CutsTheXLabel";
            this.CutsTheXLabel.Size = new System.Drawing.Size(59, 25);
            this.CutsTheXLabel.TabIndex = 15;
            this.CutsTheXLabel.Text = "None";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cuts the x:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1019, 641);
            this.Controls.Add(this.resultsPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.funcPictureBox)).EndInit();
            this.resultsPanel.ResumeLayout(false);
            this.resultsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AsymatotsWithYShowWayButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DomainDefintionShowWayButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWayCutsWithXButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWayCuttecdFuncButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox funcPictureBox;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel resultsPanel;
        private System.Windows.Forms.Label CuttedFuncLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label MaximumPointsLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label MinimumPointsLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label CutsTheYLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label CutsTheXLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CutsWithXWayLabel;
        private System.Windows.Forms.Label minAndMaxWayLabel;
        private System.Windows.Forms.PictureBox showWayCuttecdFuncButton;
        private System.Windows.Forms.PictureBox showWayCutsWithXButton;
        private System.Windows.Forms.Label AsymatotWithYWayLabel;
        private System.Windows.Forms.Label DomainDefinitionWayLabel;
        private System.Windows.Forms.PictureBox AsymatotsWithYShowWayButton;
        private System.Windows.Forms.PictureBox DomainDefintionShowWayButton;
        private System.Windows.Forms.Label AsymatotWithYLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label AsymatotsWithXLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label DomainDefinitionLabel;
        private System.Windows.Forms.Label label9;
    }
}

