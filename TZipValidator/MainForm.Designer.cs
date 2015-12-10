namespace TZipValidator
{
    partial class MainForm
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblInDir = new System.Windows.Forms.Label();
            this.btnDirs = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btQuick = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(12, 9);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(66, 15);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Input Dir:";
            // 
            // lblInDir
            // 
            this.lblInDir.AutoSize = true;
            this.lblInDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInDir.Location = new System.Drawing.Point(85, 9);
            this.lblInDir.Name = "lblInDir";
            this.lblInDir.Size = new System.Drawing.Size(97, 15);
            this.lblInDir.TabIndex = 1;
            this.lblInDir.Text = "Directory of scan";
            // 
            // btnDirs
            // 
            this.btnDirs.Location = new System.Drawing.Point(405, 9);
            this.btnDirs.Name = "btnDirs";
            this.btnDirs.Size = new System.Drawing.Size(67, 21);
            this.btnDirs.TabIndex = 2;
            this.btnDirs.Text = "Settings";
            this.btnDirs.UseVisualStyleBackColor = true;
            this.btnDirs.Click += new System.EventHandler(this.btnDirs_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 158);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(470, 300);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // btQuick
            // 
            this.btQuick.Location = new System.Drawing.Point(12, 37);
            this.btQuick.Name = "btQuick";
            this.btQuick.Size = new System.Drawing.Size(271, 23);
            this.btQuick.TabIndex = 4;
            this.btQuick.Text = "Check mode.txt, size and transparency of first image.";
            this.btQuick.UseVisualStyleBackColor = true;
            this.btQuick.Click += new System.EventHandler(this.btQuick_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Check all above, sequence numbers, and all images.  ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(12, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(307, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Check all above, and repeat blank images, takes long time.";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(12, 124);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(384, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Monitor Input Directory, scan TZip files fix or reject and move out of directory";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 470);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btQuick);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnDirs);
            this.Controls.Add(this.lblInDir);
            this.Controls.Add(this.lbl1);
            this.Name = "MainForm";
            this.Text = "TZipValidator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblInDir;
        private System.Windows.Forms.Button btnDirs;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btQuick;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

