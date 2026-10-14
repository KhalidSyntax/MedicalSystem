using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Payments
{
    public partial class FrmPaymentAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int PaymentID);

        public DataBackEventHandler DataBack;

        private int _PaymentID = -1;
        private PaymentDTO _Payment;

        private readonly PaymentService _paymentService;
        private readonly AppointmentService _appointmentService;

        private List<AppointmentDTO> _allAppointments;
        private List<PaymentDTO> _allPayments;

        public FrmPaymentAddEdit()
        {
            InitializeComponent();

            _paymentService = new PaymentService();
            _appointmentService = new AppointmentService();
        }

        public FrmPaymentAddEdit(int paymentID)
        {
            InitializeComponent();

            _paymentService = new PaymentService();
            _appointmentService = new AppointmentService();

            _PaymentID = paymentID;
        }

        private void ResetDefaultValues()
        {
            lblTitle.Text = _PaymentID == -1
                ? "Add Payment"
                : "Update Payment";

            cbAppointment.DataSource = null;
            cbAppointment.Enabled = true;

            cbPaymentMethod.Items.Clear();
            cbPaymentMethod.Items.AddRange(new object[]
            {
                "Cash",
                "Mada",
                "Credit Card",
                "Insurance"
            });

            cbPaymentMethod.SelectedIndex = -1;

            txtAmountPaid.Clear();
            txtAdditionalNotes.Clear();

            btnSave.Enabled = true;

            if (_PaymentID == -1)
                this.Text = "Add New Payment";
            else
                this.Text = "Update Payment";
        }

        private async Task LoadAppointments()
        {
            try
            {
                _allAppointments =
                    await _appointmentService.GetAllAppointmentsAsync();

                if (_allAppointments == null)
                    _allAppointments = new List<AppointmentDTO>();

                cbAppointment.DisplayMember = "AppointmentID";
                cbAppointment.ValueMember = "AppointmentID";
                cbAppointment.DataSource = _allAppointments;

                if (_allAppointments.Count == 0 && _PaymentID == -1)
                {
                    MessageBox.Show(
                        "There are no appointments available.",
                        "Payment",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load appointments.\n{ex.Message}",
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
                _Payment =
                    await _paymentService.GetPaymentByIDAsync(_PaymentID);

                if (_Payment == null)
                {
                    MessageBox.Show(
                        $"Payment with ID = {_PaymentID} was not found.",
                        "Payment",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    Close();
                    return;
                }

                Text = "Update Payment";
                lblTitle.Text = "Update Payment";

                lblPaymentID.Text = _PaymentID.ToString();

                cbAppointment.SelectedValue = _Payment.AppointmentID;
                cbAppointment.Enabled = false;

                dtpPaymentDate.Value = _Payment.PaymentDate;

                cbPaymentMethod.SelectedItem = _Payment.PaymentMethod;

                txtAmountPaid.Text =
                    _Payment.AmountPaid.ToString();

                txtAdditionalNotes.Text =
                    _Payment.AdditionalNotes ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load payment.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
            }
        }

        private async void FrmPaymentAddEdit_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_PaymentID != -1)
            {
                await LoadData();

                if (IsDisposed || _Payment == null)
                    return;
            }

            await LoadAppointments();

            if (_PaymentID == -1 && !btnSave.Enabled)
                return;

            if (_PaymentID == -1)
            {
                _Payment = new PaymentDTO();
            }
            else
            {
                cbAppointment.SelectedValue = _Payment.AppointmentID;
                cbAppointment.Enabled = false;
            }
        }

        private bool HasChanges()
        {
            if (_Payment == null)
                return true;

            if (_PaymentID == -1)
                return true;

            if (dtpPaymentDate.Value.Date != _Payment.PaymentDate.Date)
                return true;

            if (cbPaymentMethod.Text != _Payment.PaymentMethod)
                return true;

            if (!decimal.TryParse(
                    txtAmountPaid.Text.Trim(), out decimal amount) ||
                amount != _Payment.AmountPaid)
                return true;

            if (txtAdditionalNotes.Text.Trim() !=
                (_Payment.AdditionalNotes ?? "").Trim())
                return true;

            return false;
        }

        private void cbAppointment_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_PaymentID == -1 &&
                (cbAppointment.SelectedValue == null ||
                 cbAppointment.SelectedIndex == -1))
            {
                e.Cancel = true;
                epValidation.SetError(
                    cbAppointment, "Please select an appointment.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbAppointment, "");
        }

        private void cbPaymentMethod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbPaymentMethod.Text))
            {
                e.Cancel = true;
                epValidation.SetError(cbPaymentMethod, "Payment method is required.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(cbPaymentMethod, "");
            }
        }

        private void txtAmountPaid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal amount) || amount <= 0)
            {
                e.Cancel = true;
                epValidation.SetError(txtAmountPaid, "Amount paid must be greater than 0.");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(txtAmountPaid, "");
            }
        }

        private async Task AddPayment()
        {
            if (cbAppointment.SelectedValue == null)
                return;

            CreatePaymentDTO payment = new CreatePaymentDTO
            {
                AppointmentID =
                    Convert.ToInt32(cbAppointment.SelectedValue),

                PaymentDate = dtpPaymentDate.Value.Date,
                PaymentMethod = cbPaymentMethod.Text,
                AmountPaid = decimal.Parse(txtAmountPaid.Text.Trim()),

                AdditionalNotes =
                    string.IsNullOrWhiteSpace(txtAdditionalNotes.Text)
                        ? null
                        : txtAdditionalNotes.Text.Trim()
            };

            var response =
                await _paymentService.AddPaymentAsync(payment);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                if (response.Payment != null)
                {
                    _PaymentID = response.Payment.PaymentID;
                    lblPaymentID.Text = _PaymentID.ToString();
                    DataBack?.Invoke(this, _PaymentID);
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

        private async Task UpdatePayment()
        {
            UpdatePaymentDTO payment = new UpdatePaymentDTO
            {
                AppointmentID = _Payment.AppointmentID,
                PaymentDate = dtpPaymentDate.Value.Date,
                PaymentMethod = cbPaymentMethod.Text,
                AmountPaid = decimal.Parse(txtAmountPaid.Text.Trim()),

                AdditionalNotes =
                    string.IsNullOrWhiteSpace(txtAdditionalNotes.Text)
                        ? null
                        : txtAdditionalNotes.Text.Trim()
            };

            var response =
                await _paymentService.UpdatePaymentAsync(
                    _PaymentID, payment);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PaymentID);
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
                if (_PaymentID == -1)
                    await AddPayment();
                else
                    await UpdatePayment();
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