using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Controls
{
    public partial class PrescriptionInfoControl : UserControl
    {
        private PrescriptionDTO _Prescription;
        private int _PrescriptionID = -1;

        private readonly PrescriptionService _prescriptionService;

        public int PrescriptionID
        {
            get { return _PrescriptionID; }
        }

        public PrescriptionDTO SelectedPrescriptionInfo
        {
            get { return _Prescription; }
        }

        public PrescriptionInfoControl()
        {
            InitializeComponent();

            _prescriptionService = new PrescriptionService();
        }

        public void ResetPrescriptionInfo()
        {
            _Prescription = null;
            _PrescriptionID = -1;

            lblPrescriptionID.Text = "[???]";
            lblMedicalRecordID.Text = "[???]";
            lblPatientName.Text = "[???]";
            lblDoctorName.Text = "[???]";
            lblMedicationName.Text = "[???]";
            lblDosage.Text = "[???]";
            lblFrequency.Text = "[???]";
            lblStartDate.Text = "[???]";
            lblEndDate.Text = "[???]";
            lblSpecialInstructions.Text = "[???]";
        }

        public async Task LoadPrescriptionInfo(int prescriptionID)
        {
            try
            {
                _Prescription =
                    await _prescriptionService.GetPrescriptionByIDAsync(
                        prescriptionID);

                if (_Prescription == null)
                {
                    ResetPrescriptionInfo();

                    MessageBox.Show(
                        $"No Prescription With PrescriptionID = {prescriptionID}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                FillPrescriptionInfo();
            }
            catch (Exception ex)
            {
                ResetPrescriptionInfo();

                MessageBox.Show(
                    $"Failed to load prescription: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FillPrescriptionInfo()
        {
            _PrescriptionID = _Prescription.PrescriptionID;

            lblPrescriptionID.Text =
                _PrescriptionID.ToString();

            lblMedicalRecordID.Text =
                _Prescription.MedicalRecordID.ToString();

            lblPatientName.Text =
                _Prescription.PatientName;

            lblDoctorName.Text =
                _Prescription.DoctorName;

            lblMedicationName.Text =
                _Prescription.MedicationName;

            lblDosage.Text =
                _Prescription.Dosage;

            lblFrequency.Text =
                _Prescription.Frequency;

            lblStartDate.Text =
                _Prescription.StartDate.ToShortDateString();

            lblEndDate.Text =
                _Prescription.EndDate.ToShortDateString();

            lblSpecialInstructions.Text =
                _Prescription.SpecialInstructions ?? "[???]";
        }
    }
}