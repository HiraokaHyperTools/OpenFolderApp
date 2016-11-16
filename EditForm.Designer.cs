namespace OpenFolderApp {
    partial class EditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.tbDir = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bRefDir = new System.Windows.Forms.Button();
            this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "リンク先：";
            // 
            // tbDir
            // 
            this.tbDir.Location = new System.Drawing.Point(12, 33);
            this.tbDir.Name = "tbDir";
            this.tbDir.Size = new System.Drawing.Size(646, 28);
            this.tbDir.TabIndex = 1;
            // 
            // bSave
            // 
            this.bSave.AutoSize = true;
            this.bSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bSave.Location = new System.Drawing.Point(12, 152);
            this.bSave.Name = "bSave";
            this.bSave.Padding = new System.Windows.Forms.Padding(10);
            this.bSave.Size = new System.Drawing.Size(82, 51);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "保存";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bRefDir
            // 
            this.bRefDir.BackgroundImage = global::OpenFolderApp.Properties.Resources.browse;
            this.bRefDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bRefDir.Location = new System.Drawing.Point(664, 9);
            this.bRefDir.Name = "bRefDir";
            this.bRefDir.Size = new System.Drawing.Size(75, 52);
            this.bRefDir.TabIndex = 2;
            this.bRefDir.UseVisualStyleBackColor = true;
            this.bRefDir.Click += new System.EventHandler(this.bRefDir_Click);
            // 
            // fbdDir
            // 
            this.fbdDir.HelpRequest += new System.EventHandler(this.fbdDir_HelpRequest);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 215);
            this.Controls.Add(this.bRefDir);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenFolderApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bRefDir;
        private System.Windows.Forms.FolderBrowserDialog fbdDir;
    }
}