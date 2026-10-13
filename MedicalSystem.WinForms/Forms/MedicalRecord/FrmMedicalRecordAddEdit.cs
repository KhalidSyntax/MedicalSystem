using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.MedicalRecord
{
    public partial class FrmMedicalRecordAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int MedicalRecordID);

        public DataBackEventHandler DataBack;

        private int _MedicalRecordID = -1;
        private MedicalRecordDTO _MedicalRecord;

        private readonly MedicalRecordService _medicalRecordService;
        private readonly AppointmentService _appointmentService;

        public FrmMedicalRecordAddEdit()
        {
            InitializeComponent();

            _medicalRecordService = new MedicalRecordService();
            _appointmentService = new AppointmentService();
        }

        public FrmMedicalRecordAddEdit(int medicalRecordID)
        {
            InitializeComponent();

            _medicalRecordService = new MedicalRecordService();
            _appointmentService = new AppointmentService();

            _MedicalRecordID = medicalRecordID;
        }

        private void ResetDefaultValues()
        {
            lblTitle.Text = "Add New Medical Record";
            Text = "Add New Medical Record";

            lblMedicalRecordID.Text = "-1";

            txtVisitDescription.Clear();
            txtDiagnosis.Clear();
            txtAdditionalNotes.Clear();

            cbAppointments.DataSource = null;
            cbAppointments.Visible = true;
            lblAppointmentName.Visible = false;

            btnSave.Enabled = true;
        }

        private async Task LoadAvailableAppointments()
        {
            try
            {
                List<AppointmentLookupDTO> appointments =
                    await _appointmentService.GetAvailableForMedicalRecordAsync();

                if (appointments == null)
                    appointments = new List<AppointmentLookupDTO>();

                cbAppointments.DataSource = null;
                cbAppointments.DisplayMember = "DisplayText";
                cbAppointments.ValueMember = "AppointmentID";
                cbAppointments.DataSource = appointments;

                if (appointments.Count == 0)
                {
                    MessageBox.Show(
                        "There are no available appointments.",
                        "Medical Record",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load available appointments.\n{ex.Message}",
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
                _MedicalRecord =
                    await _medicalRecordService.GetMedicalRecordByIDAsync(
                        _MedicalRecordID);

                if (_MedicalRecord == null)
                {
                    MessageBox.Show(
                        $"Medical Record with ID = {_MedicalRecordID} was not found.",
                        "Medical Record",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    Close();
                    return;
                }

                lblTitle.Text = "Update Medical Record";
                Text = "Update Medical Record";

                lblMedicalRecordID.Text =
                    _MedicalRecord.MedicalRecordID.ToString();

                txtVisitDescription.Text =
                    _MedicalRecord.VisitDescription ?? "";

                txtDiagnosis.Text =
                    _MedicalRecord.Diagnosis ?? "";

                txtAdditionalNotes.Text =
                    _MedicalRecord.AdditionalNotes ?? "";

                cbAppointments.Visible = false;

                lblAppointmentName.Text =
                    $"Appointment #{_MedicalRecord.AppointmentID}";

                lblAppointmentName.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load medical record.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
            }
        }

        private async void FrmMedicalRecordAddEdit_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_MedicalRecordID == -1)
            {
                await LoadAvailableAppointments();

                if (!btnSave.Enabled)
                    return;

                _MedicalRecord = new MedicalRecordDTO();
            }
            else
            {
                await LoadData();
            }
        }

        private bool HasChanges()
        {
            if (_MedicalRecord == null)
                return true;

            if (_MedicalRecordID == -1)
                return true;

            if (txtVisitDescription.Text.Trim() != (_MedicalRecord.VisitDescription ?? "").Trim())
                return true;

            if (txtDiagnosis.Text.Trim() != (_MedicalRecord.Diagnosis ?? "").Trim())
                return true;

            if (txtAdditionalNotes.Text.Trim() != (_MedicalRecord.AdditionalNotes ?? "").Trim())
                return true;

            return false;
        }

        private void cbAppointments_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_MedicalRecordID == -1 && (cbAppointments.SelectedValue == null || cbAppointments.SelectedIndex == -1))
            {
                e.Cancel = true;

                epValidation.SetError(cbAppointments, "Please select an appointment.");

                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbAppointments, null);
        }

        private async Task AddMedicalRecord()
        {
            if (cbAppointments.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select an appointment.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CreateMedicalRecordDTO medicalRecord =
                new CreateMedicalRecordDTO
                {
                    AppointmentID =
                        Convert.ToInt32(cbAppointments.SelectedValue),

                    VisitDescription =
                        string.IsNullOrWhiteSpace(txtVisitDescription.Text)
                            ? null
                            : txtVisitDescription.Text.Trim(),

                    Diagnosis =
                        string.IsNullOrWhiteSpace(txtDiagnosis.Text)
                            ? null
                            : txtDiagnosis.Text.Trim(),

                    AdditionalNotes =
                        string.IsNullOrWhiteSpace(txtAdditionalNotes.Text)
                            ? null
                            : txtAdditionalNotes.Text.Trim()
                };

            var response =
                await _medicalRecordService.AddMedicalRecordAsync(
                    medicalRecord);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                if (response.MedicalRecord != null)
                {
                    _MedicalRecordID = response.MedicalRecord.MedicalRecordID;
                    lblMedicalRecordID.Text = _MedicalRecordID.ToString();

                    DataBack?.Invoke(this, _MedicalRecordID);
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

        private async Task UpdateMedicalRecord()
        {
            UpdateMedicalRecordDTO medicalRecord =
                new UpdateMedicalRecordDTO
                {
                    VisitDescription =
                        string.IsNullOrWhiteSpace(txtVisitDescription.Text)
                            ? null
                            : txtVisitDescription.Text.Trim(),

                    Diagnosis =
                        string.IsNullOrWhiteSpace(txtDiagnosis.Text)
                            ? null
                            : txtDiagnosis.Text.Trim(),

                    AdditionalNotes =
                        string.IsNullOrWhiteSpace(txtAdditionalNotes.Text)
                            ? null
                            : txtAdditionalNotes.Text.Trim()
                };

            var response =
                await _medicalRecordService.UpdateMedicalRecordAsync(
                    _MedicalRecordID,
                    medicalRecord);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _MedicalRecordID);

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
                if (_MedicalRecordID == -1)
                    await AddMedicalRecord();
                else
                    await UpdateMedicalRecord();
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