namespace MedicalSystem.WinForms.Forms.MedicalRecord
{
    partial class FrmMedicalRecordAddEdit
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
            this.lblDateTime = new System.Windows.Forms.Label();
            this.cbAppointments = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.epValidation = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMedicalRecordID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdditionalNotes = new System.Windows.Forms.TextBox();
            this.txtVisitDescription = new System.Windows.Forms.TextBox();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblAppointmentName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDateTime.Location = new System.Drawing.Point(19, 327);
            this.lblDateTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(89, 21);
            this.lblDateTime.TabIndex = 78;
            this.lblDateTime.Text = "Diagnosis:";
            // 
            // cbAppointments
            // 
            this.cbAppointments.BackColor = System.Drawing.Color.White;
            this.cbAppointments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAppointments.DropDownWidth = 204;
            this.cbAppointments.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.cbAppointments.FormattingEnabled = true;
            this.cbAppointments.Items.AddRange(new object[] {
            "Select Gender",
            "Male",
            "Female"});
            this.cbAppointments.Location = new System.Drawing.Point(207, 173);
            this.cbAppointments.Name = "cbAppointments";
            this.cbAppointments.Size = new System.Drawing.Size(238, 25);
            this.cbAppointments.TabIndex = 69;
            this.cbAppointments.Validating += new System.ComponentModel.CancelEventHandler(this.cbAppointments_Validating);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPatient.Location = new System.Drawing.Point(19, 175);
            this.lblPatient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(123, 21);
            this.lblPatient.TabIndex = 77;
            this.lblPatient.Text = "Appointments:";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(467, 49);
            this.lblTitle.TabIndex = 75;
            this.lblTitle.Text = "Add Medical Record";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // epValidation
            // 
            this.epValidation.ContainerControl = this;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(19, 403);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(144, 21);
            this.lblStatus.TabIndex = 80;
            this.lblStatus.Text = "Additional Notes:";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDoctor.Location = new System.Drawing.Point(19, 251);
            this.lblDoctor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(140, 21);
            this.lblDoctor.TabIndex = 79;
            this.lblDoctor.Text = "Visit Description:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(58, 489);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 33);
            this.btnSave.TabIndex = 73;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.btnCancel.Location = new System.Drawing.Point(256, 489);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 33);
            this.btnCancel.TabIndex = 74;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMedicalRecordID
            // 
            this.lblMedicalRecordID.AutoSize = true;
            this.lblMedicalRecordID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicalRecordID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblMedicalRecordID.Location = new System.Drawing.Point(313, 99);
            this.lblMedicalRecordID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMedicalRecordID.Name = "lblMedicalRecordID";
            this.lblMedicalRecordID.Size = new System.Drawing.Size(19, 21);
            this.lblMedicalRecordID.TabIndex = 68;
            this.lblMedicalRecordID.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(19, 99);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 21);
            this.label6.TabIndex = 76;
            this.label6.Text = "Medical Record ID:";
            // 
            // txtAdditionalNotes
            // 
            this.txtAdditionalNotes.BackColor = System.Drawing.Color.White;
            this.txtAdditionalNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdditionalNotes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtAdditionalNotes.Location = new System.Drawing.Point(207, 379);
            this.txtAdditionalNotes.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdditionalNotes.MaxLength = 50;
            this.txtAdditionalNotes.Multiline = true;
            this.txtAdditionalNotes.Name = "txtAdditionalNotes";
            this.txtAdditionalNotes.Size = new System.Drawing.Size(238, 68);
            this.txtAdditionalNotes.TabIndex = 81;
            // 
            // txtVisitDescription
            // 
            this.txtVisitDescription.BackColor = System.Drawing.Color.White;
            this.txtVisitDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVisitDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVisitDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtVisitDescription.Location = new System.Drawing.Point(207, 227);
            this.txtVisitDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtVisitDescription.MaxLength = 50;
            this.txtVisitDescription.Multiline = true;
            this.txtVisitDescription.Name = "txtVisitDescription";
            this.txtVisitDescription.Size = new System.Drawing.Size(238, 68);
            this.txtVisitDescription.TabIndex = 82;
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.BackColor = System.Drawing.Color.White;
            this.txtDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiagnosis.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtDiagnosis.Location = new System.Drawing.Point(207, 303);
            this.txtDiagnosis.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiagnosis.MaxLength = 50;
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(238, 68);
            this.txtDiagnosis.TabIndex = 83;
            // 
            // lblAppointmentName
            // 
            this.lblAppointmentName.AutoSize = true;
            this.lblAppointmentName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAppointmentName.Location = new System.Drawing.Point(204, 175);
            this.lblAppointmentName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppointmentName.Name = "lblAppointmentName";
            this.lblAppointmentName.Size = new System.Drawing.Size(41, 21);
            this.lblAppointmentName.TabIndex = 84;
            this.lblAppointmentName.Text = "N/A";
            // 
            // FrmMedicalRecordAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(467, 541);
            this.Controls.Add(this.lblAppointmentName);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.txtVisitDescription);
            this.Controls.Add(this.txtAdditionalNotes);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.cbAppointments);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMedicalRecordID);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMedicalRecordAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Medical Record";
            this.Load += new System.EventHandler(this.FrmMedicalRecordAddEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.ComboBox cbAppointments;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ErrorProvider epValidation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMedicalRecordID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdditionalNotes;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.TextBox txtVisitDescription;
        private System.Windows.Forms.Label lblAppointmentName;
    }
}