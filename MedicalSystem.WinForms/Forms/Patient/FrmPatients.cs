using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Forms.Person;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Patient
{
    public partial class FrmPatients : Form
    {
        private readonly PatientService _patientService;
        private List<PatientDTO> _allPatients;

        public FrmPatients()
        {
            InitializeComponent();
            _patientService = new PatientService();
        }

        private void FormatDataGridView()
        {
            dgvPatients.Columns["PatientID"].HeaderText = "PatientID";
            dgvPatients.Columns["PatientID"].Width = 70;

            dgvPatients.Columns["PersonID"].HeaderText = "PersonID";
            dgvPatients.Columns["PersonID"].Width = 70;

            dgvPatients.Columns["Name"].HeaderText = "Name";
            dgvPatients.Columns["Name"].Width = 220;

            dgvPatients.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvPatients.Columns["DateOfBirth"].Width = 140;

            dgvPatients.Columns["GenderText"].HeaderText = "Gender";
            dgvPatients.Columns["GenderText"].Width = 100;

            dgvPatients.Columns["PhoneNumber"].HeaderText = "Phone";
            dgvPatients.Columns["PhoneNumber"].Width = 140;

            dgvPatients.Columns["Email"].HeaderText = "Email";
            dgvPatients.Columns["Email"].Width = 250;

            dgvPatients.Columns["Gender"].Visible = false;
            dgvPatients.Columns["Address"].Visible = false;
        }

        private async Task LoadPatients()
        {
            try
            {
                _allPatients = await _patientService.GetAllPatientsAsync();

                if (_allPatients == null || _allPatients.Count == 0)
                {
                    dgvPatients.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no patients.",
                        "Patients",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cbFilterBy.SelectedIndex = 0;
                dgvPatients.DataSource = _allPatients;

                FormatDataGridView();
                lblRecordCount.Text = dgvPatients.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                dgvPatients.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load patients.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmPatients_Load(object sender, EventArgs e)
        {
            await LoadPatients();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allPatients == null || _allPatients.Count <= 0)
                return;

            string filterColumn = "";

            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "PatientID": filterColumn = "PatientID"; break;
                case "Name": filterColumn = "Name"; break;
                case "Phone": filterColumn = "PhoneNumber"; break;
                case "Email": filterColumn = "Email"; break;
                default: filterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                dgvPatients.DataSource = _allPatients;
                lblRecordCount.Text = dgvPatients.Rows.Count.ToString();
                return;
            }

            string filterValue = txtFilterValue.Text.Trim();

            if (filterColumn == "PatientID" &&
                !int.TryParse(filterValue, out _))
            {
                dgvPatients.DataSource = _allPatients;
                lblRecordCount.Text = dgvPatients.Rows.Count.ToString();
                return;
            }

            List<PatientDTO> filteredPatients;

            switch (filterColumn)
            {
                case "PatientID":
                    filteredPatients = _allPatients
                        .Where(p => p.PatientID.ToString() == filterValue)
                        .ToList();
                    break;

                case "Name":
                    filteredPatients = _allPatients
                        .Where(p => p.Name.StartsWith(
                            filterValue,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "PhoneNumber":
                    filteredPatients = _allPatients
                        .Where(p => p.PhoneNumber.StartsWith(
                            filterValue,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                default:
                    filteredPatients = _allPatients
                        .Where(p => p.Email != null &&
                            p.Email.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
            }

            dgvPatients.DataSource = filteredPatients;
            lblRecordCount.Text = dgvPatients.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private async void Frm_DataBack(object sender, int patientID)
        {
            await LoadPatients();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPatientAdd frm = new FrmPatientAdd();
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a patient first.",
                    "Edit Patient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int personID = Convert.ToInt32(
                dgvPatients.CurrentRow.Cells["PersonID"].Value);

            FrmPersonAddEdit frm = new FrmPersonAddEdit(personID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a patient first.",
                    "Delete Patient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int patientID = Convert.ToInt32(
                dgvPatients.CurrentRow.Cells["PatientID"].Value);

            string patientName = dgvPatients.CurrentRow
                .Cells["Name"].Value?.ToString() ?? "";

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete '{patientName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result = await _patientService.DeletePatientAsync(patientID);

                MessageBox.Show(
                    result.Message,
                    "Delete Patient",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadPatients();
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
            if (cbFilterBy.Text == "PatientID")
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
        }

        private void dgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int personID = Convert.ToInt32(dgvPatients.CurrentRow.Cells["PersonID"].Value);

            FrmPersonDetails frm = new FrmPersonDetails(personID);
            frm.ShowDialog();
        }
    }
}