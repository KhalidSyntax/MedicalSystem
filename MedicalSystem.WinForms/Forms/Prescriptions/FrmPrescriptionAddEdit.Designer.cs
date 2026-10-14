namespace MedicalSystem.WinForms.Forms.Prescriptions
{
    partial class FrmPrescriptionAddEdit
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
            this.cbMedicalRecords = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.epValidation = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblMedicalRecord = new System.Windows.Forms.Label();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.txtMedicationName = new System.Windows.Forms.TextBox();
            this.txtSpecialInstructions = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPrescriptionID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDateTime.Location = new System.Drawing.Point(19, 217);
            this.lblDateTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(71, 21);
            this.lblDateTime.TabIndex = 92;
            this.lblDateTime.Text = "Dosage:";
            // 
            // cbMedicalRecords
            // 
            this.cbMedicalRecords.BackColor = System.Drawing.Color.White;
            this.cbMedicalRecords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMedicalRecords.DropDownWidth = 204;
            this.cbMedicalRecords.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMedicalRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.cbMedicalRecords.FormattingEnabled = true;
            this.cbMedicalRecords.Items.AddRange(new object[] {
            "Select Gender",
            "Male",
            "Female"});
            this.cbMedicalRecords.Location = new System.Drawing.Point(208, 127);
            this.cbMedicalRecords.Name = "cbMedicalRecords";
            this.cbMedicalRecords.Size = new System.Drawing.Size(238, 25);
            this.cbMedicalRecords.TabIndex = 86;
            this.cbMedicalRecords.Validating += new System.ComponentModel.CancelEventHandler(this.cbMedicalRecords_Validating);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPatient.Location = new System.Drawing.Point(19, 129);
            this.lblPatient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(132, 21);
            this.lblPatient.TabIndex = 91;
            this.lblPatient.Text = "Medical Record:";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(467, 49);
            this.lblTitle.TabIndex = 89;
            this.lblTitle.Text = "Add New Prescription";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // epValidation
            // 
            this.epValidation.ContainerControl = this;
            // 
            // lblMedicalRecord
            // 
            this.lblMedicalRecord.AutoSize = true;
            this.lblMedicalRecord.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicalRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMedicalRecord.Location = new System.Drawing.Point(211, 129);
            this.lblMedicalRecord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMedicalRecord.Name = "lblMedicalRecord";
            this.lblMedicalRecord.Size = new System.Drawing.Size(41, 21);
            this.lblMedicalRecord.TabIndex = 98;
            this.lblMedicalRecord.Text = "N/A";
            // 
            // txtDosage
            // 
            this.txtDosage.BackColor = System.Drawing.Color.White;
            this.txtDosage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDosage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDosage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtDosage.Location = new System.Drawing.Point(208, 215);
            this.txtDosage.Margin = new System.Windows.Forms.Padding(4);
            this.txtDosage.MaxLength = 50;
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(238, 25);
            this.txtDosage.TabIndex = 97;
            this.txtDosage.Validating += new System.ComponentModel.CancelEventHandler(this.txtDosage_Validating);
            // 
            // txtMedicationName
            // 
            this.txtMedicationName.BackColor = System.Drawing.Color.White;
            this.txtMedicationName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMedicationName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMedicationName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtMedicationName.Location = new System.Drawing.Point(208, 171);
            this.txtMedicationName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMedicationName.MaxLength = 50;
            this.txtMedicationName.Name = "txtMedicationName";
            this.txtMedicationName.Size = new System.Drawing.Size(238, 25);
            this.txtMedicationName.TabIndex = 96;
            this.txtMedicationName.Validating += new System.ComponentModel.CancelEventHandler(this.txtMedicationName_Validating);
            // 
            // txtSpecialInstructions
            // 
            this.txtSpecialInstructions.BackColor = System.Drawing.Color.White;
            this.txtSpecialInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecialInstructions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecialInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtSpecialInstructions.Location = new System.Drawing.Point(208, 393);
            this.txtSpecialInstructions.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpecialInstructions.MaxLength = 50;
            this.txtSpecialInstructions.Multiline = true;
            this.txtSpecialInstructions.Name = "txtSpecialInstructions";
            this.txtSpecialInstructions.Size = new System.Drawing.Size(238, 70);
            this.txtSpecialInstructions.TabIndex = 95;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(19, 261);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(93, 21);
            this.lblStatus.TabIndex = 94;
            this.lblStatus.Text = "Frequency:";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDoctor.Location = new System.Drawing.Point(19, 173);
            this.lblDoctor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(151, 21);
            this.lblDoctor.TabIndex = 93;
            this.lblDoctor.Text = "Medication Name:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(58, 498);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 33);
            this.btnSave.TabIndex = 87;
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
            this.btnCancel.Location = new System.Drawing.Point(256, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 33);
            this.btnCancel.TabIndex = 88;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPrescriptionID
            // 
            this.lblPrescriptionID.AutoSize = true;
            this.lblPrescriptionID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrescriptionID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblPrescriptionID.Location = new System.Drawing.Point(313, 85);
            this.lblPrescriptionID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrescriptionID.Name = "lblPrescriptionID";
            this.lblPrescriptionID.Size = new System.Drawing.Size(19, 21);
            this.lblPrescriptionID.TabIndex = 85;
            this.lblPrescriptionID.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(19, 85);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 21);
            this.label6.TabIndex = 90;
            this.label6.Text = "Prescription ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(22, 305);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 99;
            this.label1.Text = "Start Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(22, 349);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 100;
            this.label2.Text = "End Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(22, 393);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 21);
            this.label3.TabIndex = 101;
            this.label3.Text = "Special Instructions:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy-MM-dd";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(208, 303);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(238, 25);
            this.dtpStartDate.TabIndex = 102;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy-MM-dd";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(208, 347);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(238, 25);
            this.dtpEndDate.TabIndex = 103;
            this.dtpEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtpEndDate_Validating);
            // 
            // txtFrequency
            // 
            this.txtFrequency.BackColor = System.Drawing.Color.White;
            this.txtFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFrequency.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrequency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtFrequency.Location = new System.Drawing.Point(208, 259);
            this.txtFrequency.Margin = new System.Windows.Forms.Padding(4);
            this.txtFrequency.MaxLength = 50;
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(238, 25);
            this.txtFrequency.TabIndex = 104;
            this.txtFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtFrequency_Validating);
            // 
            // FrmPrescriptionAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(467, 546);
            this.Controls.Add(this.txtFrequency);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.cbMedicalRecords);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMedicalRecord);
            this.Controls.Add(this.txtDosage);
            this.Controls.Add(this.txtMedicationName);
            this.Controls.Add(this.txtSpecialInstructions);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPrescriptionID);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmPrescriptionAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Prescription";
            this.Load += new System.EventHandler(this.FrmPrescriptionAddEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.ComboBox cbMedicalRecords;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ErrorProvider epValidation;
        private System.Windows.Forms.Label lblMedicalRecord;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.TextBox txtMedicationName;
        private System.Windows.Forms.TextBox txtSpecialInstructions;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPrescriptionID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrequency;
    }
}