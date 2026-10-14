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
    public partial class FrmPrescriptions : Form
    {
        private readonly PrescriptionService _prescriptionService;
        private List<PrescriptionDTO> _allPrescriptions;

        public FrmPrescriptions()
        {
            InitializeComponent();
            _prescriptionService = new PrescriptionService();
        }

        private void FormatDataGridView()
        {
            dgvPrescriptions.Columns["PrescriptionID"].HeaderText = "Prescription ID";
            dgvPrescriptions.Columns["PrescriptionID"].Width = 110;

            dgvPrescriptions.Columns["PatientName"].HeaderText = "Patient Name";
            dgvPrescriptions.Columns["PatientName"].Width = 180;

            dgvPrescriptions.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvPrescriptions.Columns["DoctorName"].Width = 180;

            dgvPrescriptions.Columns["MedicationName"].HeaderText = "Medication Name";
            dgvPrescriptions.Columns["MedicationName"].Width = 200;

            dgvPrescriptions.Columns["Dosage"].HeaderText = "Dosage";
            dgvPrescriptions.Columns["Dosage"].Width = 120;

            dgvPrescriptions.Columns["StartDate"].HeaderText = "Start Date";
            dgvPrescriptions.Columns["StartDate"].Width = 120;
            dgvPrescriptions.Columns["StartDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dgvPrescriptions.Columns["EndDate"].HeaderText = "End Date";
            dgvPrescriptions.Columns["EndDate"].Width = 120;
            dgvPrescriptions.Columns["EndDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dgvPrescriptions.Columns["MedicalRecordID"].Visible = false;
            dgvPrescriptions.Columns["Frequency"].Visible = false;
            dgvPrescriptions.Columns["SpecialInstructions"].Visible = false;
        }

        private async Task LoadPrescriptions()
        {
            try
            {
                _allPrescriptions =
                    await _prescriptionService.GetAllPrescriptionsAsync();

                if (_allPrescriptions == null || _allPrescriptions.Count == 0)
                {
                    dgvPrescriptions.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no prescriptions.",
                        "Prescriptions",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cbFilterBy.SelectedIndex = 0;
                dgvPrescriptions.DataSource = _allPrescriptions;

                FormatDataGridView();
                lblRecordCount.Text = dgvPrescriptions.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                dgvPrescriptions.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load prescriptions.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmPrescriptions_Load(object sender, EventArgs e)
        {
            await LoadPrescriptions();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allPrescriptions == null || _allPrescriptions.Count <= 0)
                return;

            string filterColumn = "";

            switch (cbFilterBy.SelectedItem?.ToString())
            {
                case "Prescription ID":
                    filterColumn = "PrescriptionID";
                    break;

                case "Patient Name":
                    filterColumn = "PatientName";
                    break;

                case "Doctor Name":
                    filterColumn = "DoctorName";
                    break;

                case "Medication Name":
                    filterColumn = "MedicationName";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                dgvPrescriptions.DataSource = _allPrescriptions;
                lblRecordCount.Text = dgvPrescriptions.Rows.Count.ToString();
                return;
            }

            string filterValue = txtFilterValue.Text.Trim();

            if (filterColumn == "PrescriptionID" &&
                !int.TryParse(filterValue, out _))
            {
                dgvPrescriptions.DataSource = _allPrescriptions;
                lblRecordCount.Text = dgvPrescriptions.Rows.Count.ToString();
                return;
            }

            List<PrescriptionDTO> filteredPrescriptions;

            switch (filterColumn)
            {
                case "PrescriptionID":
                    filteredPrescriptions = _allPrescriptions
                        .Where(p => p.PrescriptionID.ToString() == filterValue)
                        .ToList();
                    break;

                case "PatientName":
                    filteredPrescriptions = _allPrescriptions
                        .Where(p => p.PatientName != null &&
                            p.PatientName.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "DoctorName":
                    filteredPrescriptions = _allPrescriptions
                        .Where(p => p.DoctorName != null &&
                            p.DoctorName.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                default:
                    filteredPrescriptions = _allPrescriptions
                        .Where(p => p.MedicationName != null &&
                            p.MedicationName.StartsWith(
                                filterValue,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
            }

            dgvPrescriptions.DataSource = filteredPrescriptions;
            lblRecordCount.Text = dgvPrescriptions.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            dgvPrescriptions.DataSource = _allPrescriptions;
            lblRecordCount.Text = dgvPrescriptions.Rows.Count.ToString();
        }

        private async void Frm_DataBack(object sender, int prescriptionID)
        {
            await LoadPrescriptions();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPrescriptionAddEdit frm = new FrmPrescriptionAddEdit();
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a prescription first.",
                    "Edit Prescription",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int prescriptionID = Convert.ToInt32(
                dgvPrescriptions.CurrentRow.Cells["PrescriptionID"].Value);

            FrmPrescriptionAddEdit frm =
                new FrmPrescriptionAddEdit(prescriptionID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a prescription first.",
                    "Delete Prescription",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int prescriptionID = Convert.ToInt32(
                dgvPrescriptions.CurrentRow.Cells["PrescriptionID"].Value);

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete Prescription ID {prescriptionID}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result =
                    await _prescriptionService.DeletePrescriptionAsync(
                        prescriptionID);

                MessageBox.Show(
                    result.Message,
                    "Delete Prescription",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadPrescriptions();
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
            if (cbFilterBy.Text == "Prescription ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
            }
        }

        private void dgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int prescriptionID = Convert.ToInt32(
                dgvPrescriptions.Rows[e.RowIndex]
                    .Cells["PrescriptionID"].Value);

            FrmPrescriptionInfo frm =
                new FrmPrescriptionInfo(prescriptionID);

            frm.ShowDialog();
        }
    }
}