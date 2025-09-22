namespace DrivingLicenseManagement
{
    partial class frmChangePassword
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
            this.components = new System.ComponentModel.Container();
            this.ctrlUserInfo1 = new DrivingLicenseManagement.ctrlUserInfo();
            this.ctrlPersonInfo1 = new DrivingLicenseManagement.ctrlPersonInfo();
            this.txtCurrentPsw = new System.Windows.Forms.TextBox();
            this.txtConfirmPsw = new System.Windows.Forms.TextBox();
            this.txtNewPsw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlUserInfo1
            // 
            this.ctrlUserInfo1.Location = new System.Drawing.Point(12, 392);
            this.ctrlUserInfo1.Name = "ctrlUserInfo1";
            this.ctrlUserInfo1.Size = new System.Drawing.Size(767, 111);
            this.ctrlUserInfo1.TabIndex = 1;
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(757, 374);
            this.ctrlPersonInfo1.TabIndex = 0;
            // 
            // txtCurrentPsw
            // 
            this.txtCurrentPsw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCurrentPsw.Location = new System.Drawing.Point(389, 540);
            this.txtCurrentPsw.Name = "txtCurrentPsw";
            this.txtCurrentPsw.Size = new System.Drawing.Size(195, 28);
            this.txtCurrentPsw.TabIndex = 2;
            this.txtCurrentPsw.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrentPsw_Validating);
            // 
            // txtConfirmPsw
            // 
            this.txtConfirmPsw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtConfirmPsw.Location = new System.Drawing.Point(389, 638);
            this.txtConfirmPsw.Name = "txtConfirmPsw";
            this.txtConfirmPsw.Size = new System.Drawing.Size(195, 28);
            this.txtConfirmPsw.TabIndex = 3;
            this.txtConfirmPsw.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfirmPsw_Validating);
            // 
            // txtNewPsw
            // 
            this.txtNewPsw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNewPsw.Location = new System.Drawing.Point(389, 589);
            this.txtNewPsw.Name = "txtNewPsw";
            this.txtNewPsw.Size = new System.Drawing.Size(195, 28);
            this.txtNewPsw.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(196, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(105, 641);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "New Password Confirmation:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(224, 592);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "New Password:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnClose.Location = new System.Drawing.Point(498, 700);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 39);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSave.Location = new System.Drawing.Point(643, 700);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 39);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmChangePassword
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(783, 751);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewPsw);
            this.Controls.Add(this.txtConfirmPsw);
            this.Controls.Add(this.txtCurrentPsw);
            this.Controls.Add(this.ctrlUserInfo1);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChangePassword";
            this.Text = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPersonInfo ctrlPersonInfo1;
        private ctrlUserInfo ctrlUserInfo1;
        private System.Windows.Forms.TextBox txtCurrentPsw;
        private System.Windows.Forms.TextBox txtConfirmPsw;
        private System.Windows.Forms.TextBox txtNewPsw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}