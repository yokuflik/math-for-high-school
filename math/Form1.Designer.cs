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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.funcPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(155, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(63, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 38);
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
            this.zoomTrackBar.Location = new System.Drawing.Point(78, 509);
            this.zoomTrackBar.Maximum = 50;
            this.zoomTrackBar.Minimum = 10;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.zoomTrackBar.Size = new System.Drawing.Size(104, 45);
            this.zoomTrackBar.TabIndex = 2;
            this.zoomTrackBar.TickFrequency = -5;
            this.zoomTrackBar.Value = 50;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 587);
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
    }
}

