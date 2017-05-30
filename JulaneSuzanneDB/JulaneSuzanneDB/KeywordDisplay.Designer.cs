namespace JulaneSuzanneDB
{
    partial class KeywordDisplay
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
            this.tbxFlag = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxFlag
            // 
            this.tbxFlag.Location = new System.Drawing.Point(136, 1);
            this.tbxFlag.Name = "tbxFlag";
            this.tbxFlag.Size = new System.Drawing.Size(39, 20);
            this.tbxFlag.TabIndex = 0;
            this.tbxFlag.Visible = false;
            this.tbxFlag.WordWrap = false;
            this.tbxFlag.TextChanged += new System.EventHandler(this.tbxFlag_TextChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 203);
            this.panel1.TabIndex = 1;
            // 
            // btnDock
            // 
            this.btnDock.Location = new System.Drawing.Point(12, 12);
            this.btnDock.Name = "btnDock";
            this.btnDock.Size = new System.Drawing.Size(68, 23);
            this.btnDock.TabIndex = 2;
            this.btnDock.Tag = "";
            this.btnDock.Text = "Dock";
            this.btnDock.UseVisualStyleBackColor = true;
            this.btnDock.Click += new System.EventHandler(this.btnDock_Click);
            // 
            // KeywordDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 267);
            this.Controls.Add(this.btnDock);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbxFlag);
            this.Name = "KeywordDisplay";
            this.Text = "Keywords";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeywordDisplay_FormClosed);
            this.Load += new System.EventHandler(this.KeywordDisplay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxFlag;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDock;

    }
}