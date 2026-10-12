using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Forms.Doctors;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Doctor
{
    public partial class FrmDoctors : Form
    {
        private readonly DoctorService _doctorService;
        private List<DoctorDTO> _allDoctors;

        public FrmDoctors()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
        }

        private void FormatDataGridView()
        {
            dgvDoctors.Columns["DoctorID"].HeaderText = "DoctorID";
            dgvDoctors.Columns["DoctorID"].Width = 70;

            dgvDoctors.Columns["PersonID"].HeaderText = "PersonID";
            dgvDoctors.Columns["PersonID"].Width = 70;

            dgvDoctors.Columns["Name"].HeaderText = "Name";
            dgvDoctors.Columns["Name"].Width = 220;

            dgvDoctors.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvDoctors.Columns["DateOfBirth"].Width = 140;

            dgvDoctors.Columns["GenderText"].HeaderText = "Gender";
            dgvDoctors.Columns["GenderText"].Width = 100;

            dgvDoctors.Columns["PhoneNumber"].HeaderText = "Phone";
            dgvDoctors.Columns["PhoneNumber"].Width = 140;

            dgvDoctors.Columns["Email"].HeaderText = "Email";
            dgvDoctors.Columns["Email"].Width = 250;

            dgvDoctors.Columns["Specialization"].HeaderText = "Specialization";
            dgvDoctors.Columns["Specialization"].Width = 180;

            dgvDoctors.Columns["Gender"].Visible = false;
            dgvDoctors.Columns["Address"].Visible = false;
        }

        private async Task LoadDoctors()
        {
            try
            {
                _allDoctors = await _doctorService.GetAllDoctorsAsync();

                if (_allDoctors == null || _allDoctors.Count == 0)
                {
                    dgvDoctors.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no doctors.",
                        "Doctors",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cbFilterBy.SelectedIndex = 0;
                dgvDoctors.DataSource = _allDoctors;

                FormatDataGridView();
                lblRecordCount.Text = dgvDoctors.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                dgvDoctors.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load doctors.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmDoctors_Load(object sender, EventArgs e)
        {
            await LoadDoctors();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allDoctors == null || _allDoctors.Count <= 0)
                return;

            string filterColumn = "";

            switch (cbFilterBy.SelectedItem?.ToString())
            {
                case "DoctorID": filterColumn = "DoctorID"; break;
                case "Name": filterColumn = "Name"; break;
                case "Phone": filterColumn = "PhoneNumber"; break;
                case "Email": filterColumn = "Email"; break;
                case "Specialization": filterColumn = "Specialization"; break;
                default: filterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                dgvDoctors.DataSource = _allDoctors;
                lblRecordCount.Text = dgvDoctors.Rows.Count.ToString();
                return;
            }

            string filterValue = txtFilterValue.Text.Trim();

            if (filterColumn == "DoctorID" &&
                !int.TryParse(filterValue, out _))
            {
                dgvDoctors.DataSource = _allDoctors;
                lblRecordCount.Text = dgvDoctors.Rows.Count.ToString();
                return;
            }

            List<DoctorDTO> filteredDoctors;

            switch (filterColumn)
            {
                case "DoctorID":
                    filteredDoctors = _allDoctors
                        .Where(d => d.DoctorID.ToString() == filterValue)
                        .ToList();
                    break;

                case "Name":
                    filteredDoctors = _allDoctors
                        .Where(d => d.Name.StartsWith(
                            filterValue,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "PhoneNumber":
                    filteredDoctors = _allDoctors
                        .Where(d => d.PhoneNumber.StartsWith(
                            filterValue,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "Specialization":
                    filteredDoctors = _allDoctors
                        .Where(d => d.Specialization != null &&
                            d.Specialization.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                default:
                    filteredDoctors = _allDoctors
                        .Where(d => d.Email != null &&
                            d.Email.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
            }

            dgvDoctors.DataSource = filteredDoctors;
            lblRecordCount.Text = dgvDoctors.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            dgvDoctors.DataSource = _allDoctors;
            lblRecordCount.Text = dgvDoctors.Rows.Count.ToString();
        }

        private async void Frm_DataBack(object sender, int doctorID)
        {
            await LoadDoctors();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmDoctorAddEdit frm = new FrmDoctorAddEdit(-1);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a doctor first.",
                    "Edit Doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int doctorID = Convert.ToInt32(
                dgvDoctors.CurrentRow.Cells["DoctorID"].Value);

            FrmDoctorAddEdit frm = new FrmDoctorAddEdit(doctorID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a doctor first.",
                    "Delete Doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int doctorID = Convert.ToInt32(
                dgvDoctors.CurrentRow.Cells["DoctorID"].Value);

            string doctorName = dgvDoctors.CurrentRow
                .Cells["Name"].Value?.ToString() ?? "";

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete '{doctorName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result = await _doctorService.DeleteDoctorAsync(doctorID);

                MessageBox.Show(
                    result.Message,
                    "Delete Doctor",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadDoctors();
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
            if (cbFilterBy.Text == "DoctorID")
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
        }

        private void dgvDoctors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int personID = Convert.ToInt32(dgvDoctors.CurrentRow.Cells["PersonID"].Value);

            FrmPersonDetails frm = new FrmPersonDetails(personID);
            frm.ShowDialog();
        }
    }
}