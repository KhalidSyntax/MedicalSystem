using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.MedicalRecord
{
    public partial class FrmMedicalRecords : Form
    {
        private readonly MedicalRecordService _medicalRecordService;
        private List<MedicalRecordDTO> _allMedicalRecords;

        public FrmMedicalRecords()
        {
            InitializeComponent();
            _medicalRecordService = new MedicalRecordService();
        }

        private void FormatDataGridView()
        {
            dgvMedicalRecords.Columns["MedicalRecordID"].HeaderText = "Medical Record ID";
            dgvMedicalRecords.Columns["MedicalRecordID"].Width = 120;

            dgvMedicalRecords.Columns["AppointmentID"].HeaderText = "Appointment ID";
            dgvMedicalRecords.Columns["AppointmentID"].Width = 110;

            dgvMedicalRecords.Columns["PatientName"].HeaderText = "Patient Name";
            dgvMedicalRecords.Columns["PatientName"].Width = 200;

            dgvMedicalRecords.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvMedicalRecords.Columns["DoctorName"].Width = 200;

            dgvMedicalRecords.Columns["VisitDescription"].HeaderText = "Visit Description";
            dgvMedicalRecords.Columns["VisitDescription"].Width = 250;

            dgvMedicalRecords.Columns["Diagnosis"].HeaderText = "Diagnosis";
            dgvMedicalRecords.Columns["Diagnosis"].Width = 200;

            dgvMedicalRecords.Columns["AdditionalNotes"].HeaderText = "Additional Notes";
            dgvMedicalRecords.Columns["AdditionalNotes"].Width = 250;
        }

        private async Task LoadMedicalRecords()
        {
            try
            {
                _allMedicalRecords =
                    await _medicalRecordService.GetAllMedicalRecordsAsync();

                if (_allMedicalRecords == null || _allMedicalRecords.Count == 0)
                {
                    dgvMedicalRecords.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no medical records.",
                        "Medical Records",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cbFilterBy.SelectedIndex = 0;
                dgvMedicalRecords.DataSource = _allMedicalRecords;

                FormatDataGridView();
                lblRecordCount.Text = dgvMedicalRecords.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                dgvMedicalRecords.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load medical records.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmMedicalRecords_Load(object sender, EventArgs e)
        {
            await LoadMedicalRecords();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allMedicalRecords == null || _allMedicalRecords.Count <= 0)
                return;

            string filterColumn = "";

            switch (cbFilterBy.SelectedItem?.ToString())
            {
                case "Medical Record ID": filterColumn = "MedicalRecordID"; break;
                case "Patient Name": filterColumn = "PatientName"; break;
                case "Doctor Name": filterColumn = "DoctorName"; break;
                case "Appointment ID": filterColumn = "AppointmentID"; break;
                case "Diagnosis": filterColumn = "Diagnosis"; break;
                default: filterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                dgvMedicalRecords.DataSource = _allMedicalRecords;
                lblRecordCount.Text = dgvMedicalRecords.Rows.Count.ToString();
                return;
            }

            string filterValue = txtFilterValue.Text.Trim();

            if ((filterColumn == "MedicalRecordID" ||
                 filterColumn == "AppointmentID") &&
                !int.TryParse(filterValue, out _))
            {
                dgvMedicalRecords.DataSource = _allMedicalRecords;
                lblRecordCount.Text = dgvMedicalRecords.Rows.Count.ToString();
                return;
            }

            List<MedicalRecordDTO> filteredMedicalRecords;

            switch (filterColumn)
            {
                case "MedicalRecordID":
                    filteredMedicalRecords = _allMedicalRecords
                        .Where(m => m.MedicalRecordID.ToString() == filterValue)
                        .ToList();
                    break;

                case "AppointmentID":
                    filteredMedicalRecords = _allMedicalRecords
                        .Where(m => m.AppointmentID.ToString() == filterValue)
                        .ToList();
                    break;

                case "PatientName":
                    filteredMedicalRecords = _allMedicalRecords
                        .Where(m => m.PatientName != null &&
                            m.PatientName.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "DoctorName":
                    filteredMedicalRecords = _allMedicalRecords
                        .Where(m => m.DoctorName != null &&
                            m.DoctorName.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                default:
                    filteredMedicalRecords = _allMedicalRecords
                        .Where(m => m.Diagnosis != null &&
                            m.Diagnosis.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
            }

            dgvMedicalRecords.DataSource = filteredMedicalRecords;
            lblRecordCount.Text = dgvMedicalRecords.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            dgvMedicalRecords.DataSource = _allMedicalRecords;
            lblRecordCount.Text = dgvMedicalRecords.Rows.Count.ToString();
        }

        private async void Frm_DataBack(object sender, int medicalRecordID)
        {
            await LoadMedicalRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmMedicalRecordAddEdit frm = new FrmMedicalRecordAddEdit(-1);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a medical record first.",
                    "Edit Medical Record",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int medicalRecordID = Convert.ToInt32(
                dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value);

            FrmMedicalRecordAddEdit frm =
                new FrmMedicalRecordAddEdit(medicalRecordID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a medical record first.",
                    "Delete Medical Record",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int medicalRecordID = Convert.ToInt32(
                dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value);

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete Medical Record ID {medicalRecordID}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result =
                    await _medicalRecordService.DeleteMedicalRecordAsync(
                        medicalRecordID);

                MessageBox.Show(
                    result.Message,
                    "Delete Medical Record",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadMedicalRecords();
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
            if (cbFilterBy.Text == "MedicalRecordID" ||
                cbFilterBy.Text == "Appointment ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
            }
        }
    }
}