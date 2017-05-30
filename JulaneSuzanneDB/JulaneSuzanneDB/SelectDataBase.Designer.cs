namespace JulaneSuzanneDB
{
    partial class SelectDataBase
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
            this.btnAddDB = new System.Windows.Forms.Button();
            this.tbxFlag = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddDB
            // 
            this.btnAddDB.Location = new System.Drawing.Point(0, 0);
            this.btnAddDB.Name = "btnAddDB";
            this.btnAddDB.Size = new System.Drawing.Size(99, 23);
            this.btnAddDB.TabIndex = 0;
            this.btnAddDB.Text = "Add Database";
            this.btnAddDB.UseVisualStyleBackColor = true;
            this.btnAddDB.Click += new System.EventHandler(this.btnAddDB_Click);
            // 
            // tbxFlag
            // 
            this.tbxFlag.Location = new System.Drawing.Point(307, 0);
            this.tbxFlag.Name = "tbxFlag";
            this.tbxFlag.Size = new System.Drawing.Size(100, 20);
            this.tbxFlag.TabIndex = 1;
            this.tbxFlag.Visible = false;
            this.tbxFlag.TextChanged += new System.EventHandler(this.tbxFlag_TextChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(120, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(78, 21);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(223, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 261);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbxFlag);
            this.Controls.Add(this.btnAddDB);
            this.Name = "SelectDataBase";
            this.Text = "SelectDataBase";
            this.Shown += new System.EventHandler(this.SelectDataBase_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddDB;
        private System.Windows.Forms.TextBox tbxFlag;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;


    }
}