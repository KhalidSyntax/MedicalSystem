using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Appointment
{
    public partial class FrmAppointments : Form
    {
        private readonly AppointmentService _appointmentService;
        private List<AppointmentDTO> _allAppointments;

        private readonly DoctorService _doctorService;
        private List<DoctorDTO> _allDoctors;

        private readonly PatientService _patientService;
        private List<PatientDTO> _allPatients;

        public FrmAppointments()
        {
            InitializeComponent();

            _appointmentService = new AppointmentService();
            _doctorService = new DoctorService();
            _patientService = new PatientService();
        }

        private void FormatDataGridView()
        {
            if (dgvAppointments.Columns.Count == 0)
                return;

            dgvAppointments.Columns["AppointmentID"].HeaderText = "Appointment ID";
            dgvAppointments.Columns["AppointmentID"].Width = 100;

            dgvAppointments.Columns["PatientName"].HeaderText = "Patient Name";
            dgvAppointments.Columns["PatientName"].Width = 200;

            dgvAppointments.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvAppointments.Columns["DoctorName"].Width = 200;

            dgvAppointments.Columns["AppointmentDateTime"].HeaderText =
                "Appointment Date & Time";
            dgvAppointments.Columns["AppointmentDateTime"].Width = 180;
            dgvAppointments.Columns["AppointmentDateTime"].DefaultCellStyle.Format =
                "yyyy-MM-dd HH:mm";

            dgvAppointments.Columns["StatusText"].HeaderText = "Status";
            dgvAppointments.Columns["StatusText"].Width = 120;

            dgvAppointments.Columns["PatientID"].Visible = false;
            dgvAppointments.Columns["DoctorID"].Visible = false;
            dgvAppointments.Columns["AppointmentStatus"].Visible = false;
        }

        private async Task LoadAppointments()
        {
            try
            {
                _allAppointments =
                    await _appointmentService.GetAllAppointmentsAsync();

                if (_allAppointments == null)
                    _allAppointments = new List<AppointmentDTO>();

                dgvAppointments.DataSource = null;
                dgvAppointments.DataSource = _allAppointments;

                FormatDataGridView();

                lblRecordCount.Text = dgvAppointments.Rows.Count.ToString();

                if (_allAppointments.Count == 0)
                {
                    MessageBox.Show(
                        "There are no appointments.",
                        "Appointments",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                dgvAppointments.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load appointments.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task LoadAllDoctors()
        {
            try
            {
                _allDoctors = await _doctorService.GetAllDoctorsAsync();

                if (_allDoctors == null)
                    _allDoctors = new List<DoctorDTO>();

                cbDoctors.DataSource = null;
                cbDoctors.DisplayMember = "Name";
                cbDoctors.ValueMember = "DoctorID";
                cbDoctors.DataSource = _allDoctors;

                if (_allDoctors.Count == 0)
                {
                    MessageBox.Show(
                        "There are no available doctors.",
                        "Doctors",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cbDoctors.DataSource = null;

                MessageBox.Show(
                    $"Failed to load doctors.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task LoadAllPatients()
        {
            try
            {
                _allPatients = await _patientService.GetAllPatientsAsync();

                if (_allPatients == null)
                    _allPatients = new List<PatientDTO>();

                cbPatients.DataSource = null;
                cbPatients.DisplayMember = "Name";
                cbPatients.ValueMember = "PatientID";
                cbPatients.DataSource = _allPatients;

                if (_allPatients.Count == 0)
                {
                    MessageBox.Show(
                        "There are no available patients.",
                        "Patients",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cbPatients.DataSource = null;

                MessageBox.Show(
                    $"Failed to load patients.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmAppointments_Load(object sender, EventArgs e)
        {
            cbViewBy.SelectedItem = "All Appointments";
            cbFilterBy.SelectedItem = "None";

            cbDoctors.Visible = false;
            cbPatients.Visible = false;
            lblSelectDoctor.Visible = false;
            lblSelectPatient.Visible = false;

            txtFilterValue.Visible = false;

            await LoadAppointments();
        }

        private async void cbViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbViewBy.SelectedItem?.ToString())
            {
                case "All Appointments":
                    cbDoctors.Visible = false;
                    cbPatients.Visible = false;

                    lblSelectDoctor.Visible = false;
                    lblSelectPatient.Visible = false;

                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    break;

                case "By Doctor":
                    cbDoctors.Visible = true;
                    cbPatients.Visible = false;

                    lblSelectDoctor.Visible = true;
                    lblSelectPatient.Visible = false;

                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    await LoadAllDoctors();
                    break;

                case "By Patient":
                    cbPatients.Visible = true;
                    cbDoctors.Visible = false;

                    lblSelectPatient.Visible = true;
                    lblSelectDoctor.Visible = false;

                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    await LoadAllPatients();
                    break;
            }
        }

        private void SearchAppointments()
        {
            if (_allAppointments == null)
                return;

            string filterValue = txtFilterValue.Text.Trim();
            string filterColumn = cbFilterBy.SelectedItem?.ToString() ?? "None";

            List<AppointmentDTO> filteredAppointments;

            if (filterColumn == "None" || string.IsNullOrWhiteSpace(filterValue))
            {
                filteredAppointments = _allAppointments;
            }
            else
            {
                switch (filterColumn)
                {
                    case "AppointmentID":
                        if (!int.TryParse(filterValue, out int appointmentID))
                        {
                            MessageBox.Show("Please enter a valid Appointment ID.");
                            return;
                        }

                        filteredAppointments = _allAppointments
                            .Where(a => a.AppointmentID == appointmentID)
                            .ToList();
                        break;

                    case "PatientName":
                        filteredAppointments = _allAppointments
                            .Where(a => !string.IsNullOrEmpty(a.PatientName) &&
                                a.PatientName.StartsWith(
                                    filterValue,
                                    StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;

                    case "DoctorName":
                        filteredAppointments = _allAppointments
                            .Where(a => !string.IsNullOrEmpty(a.DoctorName) &&
                                a.DoctorName.StartsWith(
                                    filterValue,
                                    StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;

                    case "AppointmentDateTime":
                        filteredAppointments = _allAppointments
                            .Where(a => a.AppointmentDateTime
                                .ToString("yyyy-MM-dd HH:mm")
                                .StartsWith(
                                    filterValue,
                                    StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;

                    case "Status":
                        filteredAppointments = _allAppointments
                            .Where(a => !string.IsNullOrEmpty(a.StatusText) &&
                                a.StatusText.StartsWith(
                                    filterValue,
                                    StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;

                    default:
                        filteredAppointments = _allAppointments;
                        break;
                }
            }

            dgvAppointments.DataSource = null;
            dgvAppointments.DataSource = filteredAppointments;

            FormatDataGridView();

            lblRecordCount.Text = dgvAppointments.Rows.Count.ToString();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cbViewBy.SelectedItem?.ToString())
                {
                    case "All Appointments":
                        _allAppointments =
                            await _appointmentService.GetAllAppointmentsAsync();
                        break;

                    case "By Doctor":
                        {
                            if (cbDoctors.SelectedValue == null ||
                                !int.TryParse(
                                    cbDoctors.SelectedValue.ToString(),
                                    out int doctorID))
                            {
                                MessageBox.Show("Please select a doctor.");
                                return;
                            }

                            _allAppointments =
                                await _appointmentService.GetAppointmentsByDoctorAsync(
                                    doctorID);

                            break;
                        }

                    case "By Patient":
                        {
                            if (cbPatients.SelectedValue == null ||
                                !int.TryParse(
                                    cbPatients.SelectedValue.ToString(),
                                    out int patientID))
                            {
                                MessageBox.Show("Please select a patient.");
                                return;
                            }

                            _allAppointments =
                                await _appointmentService.GetAppointmentsByPatientAsync(
                                    patientID);

                            break;
                        }

                    default:
                        MessageBox.Show("Please select a view option.");
                        return;
                }

                if (_allAppointments == null)
                    _allAppointments = new List<AppointmentDTO>();

                if (_allAppointments.Count == 0)
                {
                    dgvAppointments.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no appointments matching your search.",
                        "Appointments",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                SearchAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Search failed.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task Reset()
        {
            txtFilterValue.Clear();

            cbFilterBy.SelectedItem = "None";
            cbViewBy.SelectedItem = "All Appointments";

            cbDoctors.Visible = false;
            cbPatients.Visible = false;

            lblSelectDoctor.Visible = false;
            lblSelectPatient.Visible = false;

            txtFilterValue.Visible = false;

            btnSearch.Visible = true;
            btnSearch.Enabled = true;

            await LoadAppointments();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            await Reset();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            txtFilterValue.Clear();

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
            }
            else
            {
                dgvAppointments.DataSource = null;
                dgvAppointments.DataSource = _allAppointments;

                FormatDataGridView();

                lblRecordCount.Text = dgvAppointments.Rows.Count.ToString();
            }
        }

        private async void Frm_DataBack(object sender, int appointmentID)
        {
            await LoadAppointments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAppointmentAddEdit frm = new FrmAppointmentAddEdit(-1);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select an appointment first.",
                    "Edit Appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int appointmentID = Convert.ToInt32(
                dgvAppointments.CurrentRow.Cells["AppointmentID"].Value);

            FrmAppointmentAddEdit frm =
                new FrmAppointmentAddEdit(appointmentID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select an appointment first.",
                    "Delete Appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int appointmentID = Convert.ToInt32(
                dgvAppointments.CurrentRow.Cells["AppointmentID"].Value);

            string patientName = Convert.ToString(
                dgvAppointments.CurrentRow.Cells["PatientName"].Value);

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the appointment for '{patientName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result =
                    await _appointmentService.DeleteAppointmentAsync(appointmentID);

                MessageBox.Show(
                    result.Message,
                    "Delete Appointment",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadAppointments();
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

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "AppointmentID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
            }
        }
    }
}