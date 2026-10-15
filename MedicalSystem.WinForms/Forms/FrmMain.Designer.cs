namespace MedicalSystem.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnPrescriptions = new System.Windows.Forms.Button();
            this.btnAppointments = new System.Windows.Forms.Button();
            this.btnPatients = new System.Windows.Forms.Button();
            this.btnDoctors = new System.Windows.Forms.Button();
            this.btnMedicalRecords = new System.Windows.Forms.Button();
            this.btnPersons = new System.Windows.Forms.Button();
            this.lblClinicTitle = new System.Windows.Forms.Label();
            this.lblMainMenu = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(54)))), ((int)(((byte)(83)))));
            this.pnlSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSidebar.Controls.Add(this.btnPayments);
            this.pnlSidebar.Controls.Add(this.btnPrescriptions);
            this.pnlSidebar.Controls.Add(this.btnAppointments);
            this.pnlSidebar.Controls.Add(this.btnPatients);
            this.pnlSidebar.Controls.Add(this.btnDoctors);
            this.pnlSidebar.Controls.Add(this.btnMedicalRecords);
            this.pnlSidebar.Controls.Add(this.btnPersons);
            this.pnlSidebar.Controls.Add(this.lblClinicTitle);
            this.pnlSidebar.Controls.Add(this.lblMainMenu);
            this.pnlSidebar.Location = new System.Drawing.Point(0, 95);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(209, 389);
            this.pnlSidebar.TabIndex = 7;
            // 
            // btnPayments
            // 
            this.btnPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayments.ForeColor = System.Drawing.Color.White;
            this.btnPayments.Location = new System.Drawing.Point(2, 342);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Size = new System.Drawing.Size(206, 41);
            this.btnPayments.TabIndex = 9;
            this.btnPayments.Text = "Payments";
            this.btnPayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayments.UseVisualStyleBackColor = false;
            this.btnPayments.Click += new System.EventHandler(this.btnPayments_Click);
            // 
            // btnPrescriptions
            // 
            this.btnPrescriptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnPrescriptions.FlatAppearance.BorderSize = 0;
            this.btnPrescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrescriptions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrescriptions.ForeColor = System.Drawing.Color.White;
            this.btnPrescriptions.Location = new System.Drawing.Point(2, 300);
            this.btnPrescriptions.Name = "btnPrescriptions";
            this.btnPrescriptions.Size = new System.Drawing.Size(206, 41);
            this.btnPrescriptions.TabIndex = 8;
            this.btnPrescriptions.Text = "Prescriptions";
            this.btnPrescriptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrescriptions.UseVisualStyleBackColor = false;
            this.btnPrescriptions.Click += new System.EventHandler(this.btnPrescriptions_Click);
            // 
            // btnAppointments
            // 
            this.btnAppointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnAppointments.FlatAppearance.BorderSize = 0;
            this.btnAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppointments.ForeColor = System.Drawing.Color.White;
            this.btnAppointments.Location = new System.Drawing.Point(2, 216);
            this.btnAppointments.Name = "btnAppointments";
            this.btnAppointments.Size = new System.Drawing.Size(206, 41);
            this.btnAppointments.TabIndex = 7;
            this.btnAppointments.Text = "Appointments";
            this.btnAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppointments.UseVisualStyleBackColor = false;
            this.btnAppointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // btnPatients
            // 
            this.btnPatients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnPatients.FlatAppearance.BorderSize = 0;
            this.btnPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatients.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatients.ForeColor = System.Drawing.Color.White;
            this.btnPatients.Location = new System.Drawing.Point(2, 132);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Size = new System.Drawing.Size(206, 41);
            this.btnPatients.TabIndex = 6;
            this.btnPatients.Text = "Patients";
            this.btnPatients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatients.UseVisualStyleBackColor = false;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // btnDoctors
            // 
            this.btnDoctors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnDoctors.FlatAppearance.BorderSize = 0;
            this.btnDoctors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoctors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoctors.ForeColor = System.Drawing.Color.White;
            this.btnDoctors.Location = new System.Drawing.Point(2, 174);
            this.btnDoctors.Name = "btnDoctors";
            this.btnDoctors.Size = new System.Drawing.Size(206, 41);
            this.btnDoctors.TabIndex = 5;
            this.btnDoctors.Text = "Doctors";
            this.btnDoctors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoctors.UseVisualStyleBackColor = false;
            this.btnDoctors.Click += new System.EventHandler(this.btnDoctors_Click);
            // 
            // btnMedicalRecords
            // 
            this.btnMedicalRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnMedicalRecords.FlatAppearance.BorderSize = 0;
            this.btnMedicalRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicalRecords.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedicalRecords.ForeColor = System.Drawing.Color.White;
            this.btnMedicalRecords.Location = new System.Drawing.Point(2, 258);
            this.btnMedicalRecords.Name = "btnMedicalRecords";
            this.btnMedicalRecords.Size = new System.Drawing.Size(206, 41);
            this.btnMedicalRecords.TabIndex = 4;
            this.btnMedicalRecords.Text = "Medical Records";
            this.btnMedicalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMedicalRecords.UseVisualStyleBackColor = false;
            this.btnMedicalRecords.Click += new System.EventHandler(this.btnMedicalRecords_Click);
            // 
            // btnPersons
            // 
            this.btnPersons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(75)))), ((int)(((byte)(106)))));
            this.btnPersons.FlatAppearance.BorderSize = 0;
            this.btnPersons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPersons.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPersons.ForeColor = System.Drawing.Color.White;
            this.btnPersons.Location = new System.Drawing.Point(2, 90);
            this.btnPersons.Name = "btnPersons";
            this.btnPersons.Size = new System.Drawing.Size(206, 41);
            this.btnPersons.TabIndex = 3;
            this.btnPersons.Text = "Persons";
            this.btnPersons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersons.UseVisualStyleBackColor = false;
            this.btnPersons.Click += new System.EventHandler(this.btnPersons_Click);
            // 
            // lblClinicTitle
            // 
            this.lblClinicTitle.AutoSize = true;
            this.lblClinicTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblClinicTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClinicTitle.ForeColor = System.Drawing.Color.White;
            this.lblClinicTitle.Location = new System.Drawing.Point(28, 58);
            this.lblClinicTitle.Name = "lblClinicTitle";
            this.lblClinicTitle.Size = new System.Drawing.Size(137, 25);
            this.lblClinicTitle.TabIndex = 1;
            this.lblClinicTitle.Text = "MedicalSystem";
            // 
            // lblMainMenu
            // 
            this.lblMainMenu.AutoSize = true;
            this.lblMainMenu.BackColor = System.Drawing.Color.Transparent;
            this.lblMainMenu.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMenu.ForeColor = System.Drawing.Color.White;
            this.lblMainMenu.Location = new System.Drawing.Point(28, 16);
            this.lblMainMenu.Name = "lblMainMenu";
            this.lblMainMenu.Size = new System.Drawing.Size(134, 30);
            this.lblMainMenu.TabIndex = 0;
            this.lblMainMenu.Text = "MAIN MENU";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(54)))), ((int)(((byte)(83)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(846, 95);
            this.lblDashboardTitle.TabIndex = 8;
            this.lblDashboardTitle.Text = "Welcome to MedicalSystem";
            this.lblDashboardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(209, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(637, 388);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(846, 483);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblDashboardTitle);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MedicalSystem - Clinic Management";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblMainMenu;
        private System.Windows.Forms.Label lblClinicTitle;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnDoctors;
        private System.Windows.Forms.Button btnMedicalRecords;
        private System.Windows.Forms.Button btnPersons;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnPrescriptions;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

