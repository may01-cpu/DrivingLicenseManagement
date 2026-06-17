namespace DrivingLicenseManagement.Applications.LocalDrivingLicense
{
    partial class frmShowApplicationDetails
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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlApplicationInfo1 = new DrivingLicenseManagement.Applications.Controls.ctrlApplicationInfo();
            this.ctrlDLApplicationInfo1 = new DrivingLicenseManagement.Applications.LocalDrivingLicense.ctrlDLApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(821, 553);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 40);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell Extra Bold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(269, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 50, 0, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 42);
            this.label1.TabIndex = 30;
            this.label1.Text = "Application Details";
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(35, 290);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(789, 253);
            this.ctrlApplicationInfo1.TabIndex = 16;
            // 
            // ctrlDLApplicationInfo1
            // 
            this.ctrlDLApplicationInfo1.Location = new System.Drawing.Point(32, 93);
            this.ctrlDLApplicationInfo1.Name = "ctrlDLApplicationInfo1";
            this.ctrlDLApplicationInfo1.Size = new System.Drawing.Size(792, 166);
            this.ctrlDLApplicationInfo1.TabIndex = 32;
            // 
            // frmShowApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 605);
            this.Controls.Add(this.ctrlDLApplicationInfo1);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Name = "frmShowApplicationDetails";
            this.Text = "frmShowApplicationDetails";
            this.Load += new System.EventHandler(this.frmShowApplicationDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private Controls.ctrlApplicationInfo ctrlApplicationInfo1;
        private ctrlDLApplicationInfo ctrlDLApplicationInfo1;
    }
}