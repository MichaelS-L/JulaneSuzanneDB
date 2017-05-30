namespace JulaneSuzanneDB
{
    partial class AddLink
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
            this.btnFindLink = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFindLink
            // 
            this.btnFindLink.Location = new System.Drawing.Point(12, 12);
            this.btnFindLink.Name = "btnFindLink";
            this.btnFindLink.Size = new System.Drawing.Size(98, 33);
            this.btnFindLink.TabIndex = 0;
            this.btnFindLink.Text = "Find Link";
            this.btnFindLink.UseVisualStyleBackColor = true;
            this.btnFindLink.Click += new System.EventHandler(this.btnFindLink_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 75);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbPath
            // 
            this.tbPath.Enabled = false;
            this.tbPath.Location = new System.Drawing.Point(12, 127);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(871, 20);
            this.tbPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 179);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(111, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 261);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btnFindLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddLink";
            this.Text = "Add Link";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFindLink;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}