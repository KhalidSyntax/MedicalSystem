namespace MedicalSystem.WinForms.Forms.Prescriptions
{
    partial class FrmPrescriptionInfo
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
            this.prescriptionInfoControl1 = new MedicalSystem.WinForms.Forms.Controls.PrescriptionInfoControl();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prescriptionInfoControl1
            // 
            this.prescriptionInfoControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.prescriptionInfoControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prescriptionInfoControl1.Location = new System.Drawing.Point(2, 2);
            this.prescriptionInfoControl1.Margin = new System.Windows.Forms.Padding(4);
            this.prescriptionInfoControl1.Name = "prescriptionInfoControl1";
            this.prescriptionInfoControl1.Size = new System.Drawing.Size(394, 588);
            this.prescriptionInfoControl1.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(241, 598);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(146, 38);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmPrescriptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(398, 648);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.prescriptionInfoControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPrescriptionInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prescription Information";
            this.Load += new System.EventHandler(this.FrmPrescriptionInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PrescriptionInfoControl prescriptionInfoControl1;
        private System.Windows.Forms.Button btnClose;
    }
}