namespace JulaneSuzanneDB
{
    partial class Images
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
            this.btnKeywords = new System.Windows.Forms.Button();
            this.DataDisplay = new System.Windows.Forms.SplitContainer();
            this.btnUndock = new System.Windows.Forms.Button();
            this.btnAddLink = new System.Windows.Forms.Button();
            this.btnSelectData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataDisplay)).BeginInit();
            this.DataDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKeywords
            // 
            this.btnKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKeywords.Location = new System.Drawing.Point(786, 12);
            this.btnKeywords.Name = "btnKeywords";
            this.btnKeywords.Size = new System.Drawing.Size(107, 23);
            this.btnKeywords.TabIndex = 1;
            this.btnKeywords.Text = "Hide Keywords";
            this.btnKeywords.UseVisualStyleBackColor = true;
            this.btnKeywords.Click += new System.EventHandler(this.btnKeywords_Click);
            // 
            // DataDisplay
            // 
            this.DataDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataDisplay.Location = new System.Drawing.Point(0, 50);
            this.DataDisplay.Name = "DataDisplay";
            // 
            // DataDisplay.Panel1
            // 
            this.DataDisplay.Panel1.AutoScroll = true;
            // 
            // DataDisplay.Panel2
            // 
            this.DataDisplay.Panel2.AutoScroll = true;
            this.DataDisplay.Size = new System.Drawing.Size(998, 279);
            this.DataDisplay.SplitterDistance = this.DataDisplay.Width;
            this.DataDisplay.TabIndex = 3;
            // 
            // btnUndock
            // 
            this.btnUndock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndock.Location = new System.Drawing.Point(910, 12);
            this.btnUndock.Name = "btnUndock";
            this.btnUndock.Size = new System.Drawing.Size(62, 23);
            this.btnUndock.TabIndex = 4;
            this.btnUndock.Text = "UnDock";
            this.btnUndock.UseVisualStyleBackColor = true;
            this.btnUndock.Visible = false;
            this.btnUndock.Click += new System.EventHandler(this.btnUndock_Click);
            // 
            // btnAddLink
            // 
            this.btnAddLink.Location = new System.Drawing.Point(153, 12);
            this.btnAddLink.Name = "btnAddLink";
            this.btnAddLink.Size = new System.Drawing.Size(110, 23);
            this.btnAddLink.TabIndex = 5;
            this.btnAddLink.Text = "Add Folder Link";
            this.btnAddLink.UseVisualStyleBackColor = true;
            this.btnAddLink.Click += new System.EventHandler(this.btnAddLink_Click);
            // 
            // btnSelectData
            // 
            this.btnSelectData.Location = new System.Drawing.Point(8, 12);
            this.btnSelectData.Name = "btnSelectData";
            this.btnSelectData.Size = new System.Drawing.Size(124, 23);
            this.btnSelectData.TabIndex = 6;
            this.btnSelectData.Text = "Select Different Data";
            this.btnSelectData.UseVisualStyleBackColor = true;
            this.btnSelectData.Click += new System.EventHandler(this.btnSelectData_Click);
            // 
            // Images
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 328);
            this.Controls.Add(this.btnSelectData);
            this.Controls.Add(this.btnAddLink);
            this.Controls.Add(this.btnUndock);
            this.Controls.Add(this.DataDisplay);
            this.Controls.Add(this.btnKeywords);
            this.Name = "Images";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "filename";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Images_FormClosed);
            this.Load += new System.EventHandler(this.Images_Load);
            this.ResizeEnd += new System.EventHandler(this.Images_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.DataDisplay)).EndInit();
            this.DataDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKeywords;
        private System.Windows.Forms.SplitContainer DataDisplay;
        private System.Windows.Forms.Button btnUndock;
        private System.Windows.Forms.Button btnAddLink;
        private System.Windows.Forms.Button btnSelectData;


    }
}

