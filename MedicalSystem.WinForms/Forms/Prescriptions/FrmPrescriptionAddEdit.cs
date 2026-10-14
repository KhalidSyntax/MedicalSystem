using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Prescriptions
{
    public partial class FrmPrescriptionAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int PrescriptionID);

        public DataBackEventHandler DataBack;

        private int _PrescriptionID = -1;
        private PrescriptionDTO _Prescription;

        private readonly PrescriptionService _prescriptionService;
        private readonly MedicalRecordService _medicalRecordService;

        private List<MedicalRecordDTO> _allMedicalRecords;

        public FrmPrescriptionAddEdit()
        {
            InitializeComponent();

            _prescriptionService = new PrescriptionService();
            _medicalRecordService = new MedicalRecordService();
        }

        public FrmPrescriptionAddEdit(int prescriptionID)
        {
            InitializeComponent();

            _prescriptionService = new PrescriptionService();
            _medicalRecordService = new MedicalRecordService();

            _PrescriptionID = prescriptionID;
        }

        private void ResetDefaultValues()
        {
            lblPrescriptionID.Text = "-1";

            cbMedicalRecords.DataSource = null;
            cbMedicalRecords.Enabled = true;

            txtMedicationName.Clear();
            txtDosage.Clear();
            txtFrequency.Clear();
            txtSpecialInstructions.Clear();

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            btnSave.Enabled = true;

            if (_PrescriptionID == -1)
            {
                Text = "Add New Prescription";
            }
            else
            {
                Text = "Update Prescription";
            }
        }

        private async Task LoadMedicalRecords()
        {
            try
            {
                _allMedicalRecords =
                    await _medicalRecordService.GetAllMedicalRecordsAsync();

                if (_allMedicalRecords == null)
                    _allMedicalRecords = new List<MedicalRecordDTO>();

                cbMedicalRecords.DataSource = null;
                cbMedicalRecords.DisplayMember = "DisplayText";
                cbMedicalRecords.ValueMember = "MedicalRecordID";
                cbMedicalRecords.DataSource = _allMedicalRecords;

                if (_allMedicalRecords.Count == 0)
                {
                    MessageBox.Show(
                        "There are no medical records available.",
                        "Prescription",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load medical records.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnSave.Enabled = false;
            }
        }

        private async Task LoadData()
        {
            try
            {
                _Prescription =
                    await _prescriptionService.GetPrescriptionByIDAsync(
                        _PrescriptionID);

                if (_Prescription == null)
                {
                    MessageBox.Show(
                        $"Prescription with ID = {_PrescriptionID} was not found.",
                        "Prescription",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    Close();
                    return;
                }

                Text = "Update Prescription";

                lblPrescriptionID.Text =
                    _Prescription.PrescriptionID.ToString();

                txtMedicationName.Text =
                    _Prescription.MedicationName;

                txtDosage.Text =
                    _Prescription.Dosage;

                txtFrequency.Text =
                    _Prescription.Frequency;

                txtSpecialInstructions.Text =
                    _Prescription.SpecialInstructions ?? "";

                dtpStartDate.Value = _Prescription.StartDate.Date;
                dtpEndDate.Value = _Prescription.EndDate.Date;

                cbMedicalRecords.SelectedValue =
                    _Prescription.MedicalRecordID;

                cbMedicalRecords.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load prescription.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
            }
        }

        private async void FrmPrescriptionAddEdit_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            await LoadMedicalRecords();

            if (!btnSave.Enabled)
                return;

            if (_PrescriptionID == -1)
            {
                _Prescription = new PrescriptionDTO();
            }
            else
            {
                await LoadData();
            }
        }

        private bool HasChanges()
        {
            if (_Prescription == null)
                return true;

            if (_PrescriptionID == -1)
                return true;

            if (txtMedicationName.Text.Trim() !=
                (_Prescription.MedicationName ?? "").Trim())
                return true;

            if (txtDosage.Text.Trim() !=
                (_Prescription.Dosage ?? "").Trim())
                return true;

            if (txtFrequency.Text.Trim() !=
                (_Prescription.Frequency ?? "").Trim())
                return true;

            if (dtpStartDate.Value.Date != _Prescription.StartDate.Date)
                return true;

            if (dtpEndDate.Value.Date != _Prescription.EndDate.Date)
                return true;

            if (txtSpecialInstructions.Text.Trim() !=
                (_Prescription.SpecialInstructions ?? "").Trim())
                return true;

            return false;
        }

        private void txtMedicationName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicationName.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtMedicationName, "Medication name is required.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(txtMedicationName, "");
            }
        }

        private void txtDosage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDosage.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtDosage, "Dosage is required.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(txtDosage, "");
            }
        }

        private void txtFrequency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFrequency.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtFrequency, "Frequency is required.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(txtFrequency, "");
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.Date >= dtpStartDate.Value.Date)
                epValidation.SetError(dtpEndDate, "");
            else
                epValidation.SetError(dtpEndDate, "End date cannot be before start date.");
        }

        private void dtpEndDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                e.Cancel = true;
                epValidation.SetError(dtpEndDate, "End date cannot be before start date.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(dtpEndDate, "");
            }
        }

        private void cbMedicalRecords_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_PrescriptionID == -1 &&
                (cbMedicalRecords.SelectedValue == null ||
                 cbMedicalRecords.SelectedIndex == -1))
            {
                e.Cancel = true;
                epValidation.SetError(cbMedicalRecords, "Please select a medical record.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbMedicalRecords, "");
        }

        private async Task AddPrescription()
        {
            if (cbMedicalRecords.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a medical record.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CreatePrescriptionDTO prescription =
                new CreatePrescriptionDTO
                {
                    MedicalRecordID =
                        Convert.ToInt32(cbMedicalRecords.SelectedValue),

                    MedicationName = txtMedicationName.Text.Trim(),
                    Dosage = txtDosage.Text.Trim(),
                    Frequency = txtFrequency.Text.Trim(),

                    StartDate = dtpStartDate.Value.Date,
                    EndDate = dtpEndDate.Value.Date,

                    SpecialInstructions =
                        string.IsNullOrWhiteSpace(txtSpecialInstructions.Text)
                            ? null
                            : txtSpecialInstructions.Text.Trim()
                };

            var response =
                await _prescriptionService.AddPrescriptionAsync(prescription);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                if (response.Prescription != null)
                {
                    _PrescriptionID =
                        response.Prescription.PrescriptionID;

                    lblPrescriptionID.Text =
                        _PrescriptionID.ToString();

                    DataBack?.Invoke(this, _PrescriptionID);
                }

                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            else
            {
                MessageBox.Show(
                    response.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task UpdatePrescription()
        {
            UpdatePrescriptionDTO prescription =
                new UpdatePrescriptionDTO
                {
                    MedicationName = txtMedicationName.Text.Trim(),
                    Dosage = txtDosage.Text.Trim(),
                    Frequency = txtFrequency.Text.Trim(),

                    StartDate = dtpStartDate.Value.Date,
                    EndDate = dtpEndDate.Value.Date,

                    SpecialInstructions =
                        string.IsNullOrWhiteSpace(txtSpecialInstructions.Text)
                            ? null
                            : txtSpecialInstructions.Text.Trim()
                };

            var response =
                await _prescriptionService.UpdatePrescriptionAsync(
                    _PrescriptionID,
                    prescription);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PrescriptionID);

                Close();
            }
            else
            {
                MessageBox.Show(
                    response.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show(
                    "Please fix the errors.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!HasChanges())
            {
                MessageBox.Show(
                    "No changes to save.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Do you want to save data?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                if (_PrescriptionID == -1)
                    await AddPrescription();
                else
                    await UpdatePrescription();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}