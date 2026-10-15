using MedicalSystem.WinForms.Forms.Appointment;
using MedicalSystem.WinForms.Forms.Doctor;
using MedicalSystem.WinForms.Forms.MedicalRecord;
using MedicalSystem.WinForms.Forms.Patient;
using MedicalSystem.WinForms.Forms.Payments;
using MedicalSystem.WinForms.Forms.Person;
using MedicalSystem.WinForms.Forms.Prescriptions;
using System;
using System.Windows.Forms;

namespace MedicalSystem.WinForms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void OpenForm(Form frm)
        {
            try
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to open form.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                frm.Dispose();
            }
        }

        private void btnPersons_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmPersons());
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmPatients());
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmDoctors());
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmAppointments());
        }

        private void btnMedicalRecords_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmMedicalRecords());
        }

        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmPrescriptions());
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmPayments());
        }
    }
}