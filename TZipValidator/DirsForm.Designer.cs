namespace TZipValidator
{
    partial class DirsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirsForm));
            this.lbl1 = new System.Windows.Forms.Label();
            this.tbInDir = new System.Windows.Forms.TextBox();
            this.btnInDir = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRejectDir = new System.Windows.Forms.TextBox();
            this.btnRejectDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbProcessed = new System.Windows.Forms.TextBox();
            this.btnProcessed = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLogging = new System.Windows.Forms.TextBox();
            this.btnLogging = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(12, 9);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(66, 15);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Input Dir:";
            // 
            // tbInDir
            // 
            this.tbInDir.Location = new System.Drawing.Point(85, 6);
            this.tbInDir.Name = "tbInDir";
            this.tbInDir.Size = new System.Drawing.Size(423, 20);
            this.tbInDir.TabIndex = 2;
            // 
            // btnInDir
            // 
            this.btnInDir.Location = new System.Drawing.Point(523, 6);
            this.btnInDir.Name = "btnInDir";
            this.btnInDir.Size = new System.Drawing.Size(47, 23);
            this.btnInDir.TabIndex = 3;
            this.btnInDir.Text = "...";
            this.btnInDir.UseVisualStyleBackColor = true;
            this.btnInDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Lime;
            this.btnSave.Location = new System.Drawing.Point(212, 177);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(358, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Close Save Changes in Direcotries";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rejected:";
            // 
            // tbRejectDir
            // 
            this.tbRejectDir.Location = new System.Drawing.Point(85, 35);
            this.tbRejectDir.Name = "tbRejectDir";
            this.tbRejectDir.Size = new System.Drawing.Size(423, 20);
            this.tbRejectDir.TabIndex = 2;
            // 
            // btnRejectDir
            // 
            this.btnRejectDir.Location = new System.Drawing.Point(523, 35);
            this.btnRejectDir.Name = "btnRejectDir";
            this.btnRejectDir.Size = new System.Drawing.Size(47, 23);
            this.btnRejectDir.TabIndex = 3;
            this.btnRejectDir.Text = "...";
            this.btnRejectDir.UseVisualStyleBackColor = true;
            this.btnRejectDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Processed:";
            // 
            // tbProcessed
            // 
            this.tbProcessed.Location = new System.Drawing.Point(85, 64);
            this.tbProcessed.Name = "tbProcessed";
            this.tbProcessed.Size = new System.Drawing.Size(423, 20);
            this.tbProcessed.TabIndex = 2;
            // 
            // btnProcessed
            // 
            this.btnProcessed.Location = new System.Drawing.Point(523, 64);
            this.btnProcessed.Name = "btnProcessed";
            this.btnProcessed.Size = new System.Drawing.Size(47, 23);
            this.btnProcessed.TabIndex = 3;
            this.btnProcessed.Text = "...";
            this.btnProcessed.UseVisualStyleBackColor = true;
            this.btnProcessed.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Logging:";
            // 
            // tbLogging
            // 
            this.tbLogging.Location = new System.Drawing.Point(85, 93);
            this.tbLogging.Name = "tbLogging";
            this.tbLogging.Size = new System.Drawing.Size(423, 20);
            this.tbLogging.TabIndex = 2;
            // 
            // btnLogging
            // 
            this.btnLogging.Location = new System.Drawing.Point(523, 93);
            this.btnLogging.Name = "btnLogging";
            this.btnLogging.Size = new System.Drawing.Size(47, 23);
            this.btnLogging.TabIndex = 3;
            this.btnLogging.Text = "...";
            this.btnLogging.UseVisualStyleBackColor = true;
            this.btnLogging.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // DirsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 212);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogging);
            this.Controls.Add(this.tbLogging);
            this.Controls.Add(this.btnProcessed);
            this.Controls.Add(this.tbProcessed);
            this.Controls.Add(this.btnRejectDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRejectDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnInDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInDir);
            this.Controls.Add(this.lbl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DirsForm";
            this.Text = "Set the program\'s directory settings";
            this.Shown += new System.EventHandler(this.DirsForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox tbInDir;
        private System.Windows.Forms.Button btnInDir;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRejectDir;
        private System.Windows.Forms.Button btnRejectDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProcessed;
        private System.Windows.Forms.Button btnProcessed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLogging;
        private System.Windows.Forms.Button btnLogging;
    }
}