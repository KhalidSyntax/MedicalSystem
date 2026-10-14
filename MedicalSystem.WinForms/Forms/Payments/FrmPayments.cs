using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Forms.Patient;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Payments
{
    public partial class FrmPayments : Form
    {
        private readonly PaymentService _paymentService;
        private List<PaymentDTO> _allPayments;

        public FrmPayments()
        {
            InitializeComponent();
            _paymentService = new PaymentService();
        }

        private void FormatDataGridView()
        {
            dgvPayments.Columns["PaymentID"].HeaderText = "Payment ID";
            dgvPayments.Columns["PaymentID"].Width = 100;

            dgvPayments.Columns["AppointmentID"].HeaderText = "Appointment ID";
            dgvPayments.Columns["AppointmentID"].Width = 120;

            dgvPayments.Columns["PaymentDate"].HeaderText = "Payment Date";
            dgvPayments.Columns["PaymentDate"].Width = 140;

            dgvPayments.Columns["PaymentMethod"].HeaderText = "Payment Method";
            dgvPayments.Columns["PaymentMethod"].Width = 150;

            dgvPayments.Columns["AmountPaid"].HeaderText = "Amount Paid";
            dgvPayments.Columns["AmountPaid"].Width = 120;

            dgvPayments.Columns["AdditionalNotes"].HeaderText = "Additional Notes";
            dgvPayments.Columns["AdditionalNotes"].Width = 200;
        }

        private async Task LoadPayments()
        {
            try
            {
                _allPayments = await _paymentService.GetAllPaymentsAsync();

                if (_allPayments == null || _allPayments.Count == 0)
                {
                    dgvPayments.DataSource = null;
                    lblRecordCount.Text = "0";

                    MessageBox.Show(
                        "There are no payments.",
                        "Payments",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cbFilterBy.SelectedIndex = 0;
                dgvPayments.DataSource = _allPayments;

                FormatDataGridView();
                lblRecordCount.Text = dgvPayments.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                dgvPayments.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    $"Failed to load payments.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void FrmPayments_Load(object sender, EventArgs e)
        {
            await LoadPayments();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allPayments == null || _allPayments.Count <= 0)
                return;

            string filterColumn = "";

            switch (cbFilterBy.SelectedItem?.ToString())
            {
                case "PaymentID": filterColumn = "PaymentID"; break;
                case "AppointmentID": filterColumn = "AppointmentID"; break;
                default: filterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                dgvPayments.DataSource = _allPayments;
                lblRecordCount.Text = dgvPayments.Rows.Count.ToString();
                return;
            }

            string filterValue = txtFilterValue.Text.Trim();

            if (!int.TryParse(filterValue, out int id))
            {
                dgvPayments.DataSource = _allPayments;
                lblRecordCount.Text = dgvPayments.Rows.Count.ToString();
                return;
            }

            List<PaymentDTO> filteredPayments;

            switch (filterColumn)
            {
                case "PaymentID":
                    filteredPayments = _allPayments
                        .Where(p => p.PaymentID == id)
                        .ToList();
                    break;

                default:
                    filteredPayments = _allPayments
                        .Where(p => p.AppointmentID == id)
                        .ToList();
                    break;
            }

            dgvPayments.DataSource = filteredPayments;
            lblRecordCount.Text = dgvPayments.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            dgvPayments.DataSource = _allPayments;
            lblRecordCount.Text = dgvPayments.Rows.Count.ToString();
        }

        private async void Frm_DataBack(object sender, int paymentID)
        {
            await LoadPayments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPaymentAddEdit frm = new FrmPaymentAddEdit();
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPayments.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a payment first.",
                    "Edit Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int paymentID = Convert.ToInt32(
                dgvPayments.CurrentRow.Cells["PaymentID"].Value);

            FrmPaymentAddEdit frm = new FrmPaymentAddEdit(paymentID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPayments.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a payment first.",
                    "Delete Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int paymentID = Convert.ToInt32(
                dgvPayments.CurrentRow.Cells["PaymentID"].Value);

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete payment with ID {paymentID}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                var result = await _paymentService.DeletePaymentAsync(paymentID);

                MessageBox.Show(
                    result.Message,
                    "Delete Payment",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                    await LoadPayments();
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
            if (cbFilterBy.Text == "PaymentID" ||
                cbFilterBy.Text == "AppointmentID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) &&
                            !char.IsControl(e.KeyChar);
            }
        }
    }
}