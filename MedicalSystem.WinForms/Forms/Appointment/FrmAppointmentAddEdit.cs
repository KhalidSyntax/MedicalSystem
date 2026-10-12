using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Appointment
{
    public partial class FrmAppointmentAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int AppointmentID);
        public DataBackEventHandler DataBack;

        private int _AppointmentID = -1;
        private AppointmentDTO _Appointment;

        private readonly AppointmentService _appointmentService;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public FrmAppointmentAddEdit()
        {
            InitializeComponent();

            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
            _doctorService = new DoctorService();
        }

        public FrmAppointmentAddEdit(int appointmentID)
        {
            InitializeComponent();

            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
            _doctorService = new DoctorService();

            _AppointmentID = appointmentID;
        }

        private void ResetDefaultValues()
        {
            lblTitle.Text = "Add New Appointment";
            this.Text = "Add New Appointment";

            lblAppointmentID.Text = "-1";

            cbPatients.SelectedItem = null;
            cbDoctors.SelectedItem = null;


            cbPatients.Visible = true;
            cbDoctors.Visible = true;
            cbStatus.Visible = true;

            btnSave.Enabled = true;

            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new object[]
            {
                "Pending",
                "Confirmed",
                "Completed",
                "Canceled",
                "Rescheduled",
                "No-Show"
            });

            cbStatus.SelectedIndex = 0;

            dtpAppointmentDateTime.Value = DateTime.Now;
        }

        private async Task LoadAvailableData()
        {
            try
            {
                List<PatientLookupDTO> patients =
                    await _patientService.GetPatientsLookupAsync();

                List<DoctorLookupDTO> doctors =
                    await _doctorService.GetDoctorsLookupAsync();

                cbPatients.DataSource = patients;
                cbPatients.DisplayMember = "Name";
                cbPatients.ValueMember = "PatientID";

                cbDoctors.DataSource = doctors;
                cbDoctors.DisplayMember = "Name";
                cbDoctors.ValueMember = "DoctorID";

                if (patients.Count == 0 || doctors.Count == 0)
                {
                    MessageBox.Show(
                        patients.Count == 0
                            ? "There are no patients."
                            : "There are no doctors.",
                        "Appointment",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load patients and doctors.\n{ex.Message}",
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
                _Appointment =
                    await _appointmentService.GetAppointmentByIDAsync(
                        _AppointmentID);

                if (_Appointment == null)
                {
                    MessageBox.Show(
                        $"Appointment with ID = {_AppointmentID} was not found.",
                        "Appointment",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    this.Close();
                    return;
                }

                lblTitle.Text = "Update Appointment";
                this.Text = "Update Appointment";

                lblAppointmentID.Text = _Appointment.AppointmentID.ToString();

                cbPatients.SelectedValue = _Appointment.PatientID;
                cbDoctors.SelectedValue = _Appointment.DoctorID;

                dtpAppointmentDateTime.Value =
                    _Appointment.AppointmentDateTime;

                if (_Appointment.AppointmentStatus >= 1 && _Appointment.AppointmentStatus <= 6)
                {
                    cbStatus.SelectedIndex = _Appointment.AppointmentStatus - 1;
                }
                else
                {
                    MessageBox.Show("Invalid appointment status.");
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load appointment.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmAppointmentAddEdit_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            await LoadAvailableData();

            if (!btnSave.Enabled)
                return;

            if (_AppointmentID == -1)
            {
                _Appointment = new AppointmentDTO();
            }
            else
            {
                await LoadData();
            }
        }

        private bool HasChanges()
        {
            if (_Appointment == null)
                return true;

            if (_AppointmentID == -1)
                return true;

            if (cbPatients.SelectedValue == null ||
                cbDoctors.SelectedValue == null ||
                cbStatus.SelectedIndex < 0)
                return false;

            if (_Appointment.PatientID != Convert.ToInt32(cbPatients.SelectedValue))
                return true;

            if (_Appointment.DoctorID != Convert.ToInt32(cbDoctors.SelectedValue))
                return true;

            if (_Appointment.AppointmentDateTime.ToString("yyyy-MM-dd HH:mm") !=
                dtpAppointmentDateTime.Value.ToString("yyyy-MM-dd HH:mm"))
                return true;

            if (_Appointment.AppointmentStatus != (byte)(cbStatus.SelectedIndex + 1))
                return true;

            return false;
        }

        private void cbPatients_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbPatients.SelectedValue == null)
            {
                e.Cancel = true;
                epValidation.SetError(cbPatients, "Please select a patient.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbPatients, null);
        }

        private void cbDoctors_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbDoctors.SelectedValue == null)
            {
                e.Cancel = true;
                epValidation.SetError(cbDoctors, "Please select a doctor.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbDoctors, null);
        }

        private void cbStatus_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbStatus.SelectedIndex < 0)
            {
                e.Cancel = true;
                epValidation.SetError(cbStatus, "Please select an appointment status.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbStatus, null);
        }

        private async Task AddAppointment()
        {
            CreateAppointmentDTO createAppointment =
                new CreateAppointmentDTO
                {
                    PatientID = Convert.ToInt32(cbPatients.SelectedValue),
                    DoctorID = Convert.ToInt32(cbDoctors.SelectedValue),
                    AppointmentDateTime = dtpAppointmentDateTime.Value,
                    AppointmentStatus = (byte)(cbStatus.SelectedIndex + 1)
                };

            var response =
                await _appointmentService.AddAppointmentAsync(
                    createAppointment);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (response.Appointment != null)
                {
                    DataBack?.Invoke(
                        this, response.Appointment.AppointmentID);
                }

                this.Close();
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

        private async Task UpdateAppointment()
        {
            UpdateAppointmentDTO appointment =
                new UpdateAppointmentDTO
                {
                    PatientID = Convert.ToInt32(cbPatients.SelectedValue),
                    DoctorID = Convert.ToInt32(cbDoctors.SelectedValue),
                    AppointmentDateTime = dtpAppointmentDateTime.Value,
                    AppointmentStatus = (byte)(cbStatus.SelectedIndex + 1)
                };

            var response =
                await _appointmentService.UpdateAppointmentAsync(
                    _AppointmentID, appointment);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _AppointmentID);
                this.Close();
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
                if (_AppointmentID == -1)
                {
                    await AddAppointment();
                }
                else
                {
                    await UpdateAppointment();
                }
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
            this.Close();
        }
    }
}