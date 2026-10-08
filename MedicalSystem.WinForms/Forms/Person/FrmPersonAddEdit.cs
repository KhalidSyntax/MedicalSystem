using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalSystem.WinForms.Helpers;

namespace MedicalSystem.WinForms.Forms.Person
{
    public partial class FrmPersonAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public DataBackEventHandler DataBack;

        private int _PersonID = -1;
        private PersonDTO _Person;

        private readonly PersonService _personService;

        public FrmPersonAddEdit()
        {
            InitializeComponent();

            _personService = new PersonService();
        }

        public FrmPersonAddEdit(int personID)
        {
            InitializeComponent();

            _personService = new PersonService();

            _PersonID = personID;
        }

        private void ResetDefaultValues()
        {
            lblTitle.Text = "Add New Person";
            this.Text = "Add New Person";

            lblPersonID.Text = "???";

            txtName.Clear();
            txtPhoneNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            dtpDateOfBirth.Value = DateTime.Today;
            dtpDateOfBirth.Checked = false;

            cbGender.SelectedIndex = 0;
        }

        private async Task LoadData()
        {
            _Person = await _personService.GetPersonByIDAsync(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show(
                    $"Person with ID = {_PersonID} was not found.",
                    "Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                this.Close();
                return;
            }

            lblTitle.Text = "Update Person";
            this.Text = "Update Person";

            lblPersonID.Text = _Person.PersonID.ToString();

            txtName.Text = _Person.Name;
            txtPhoneNumber.Text = _Person.PhoneNumber;
            txtEmail.Text = _Person.Email ?? "";
            txtAddress.Text = _Person.Address ?? "";

            if (_Person.DateOfBirth.HasValue)
            {
                dtpDateOfBirth.Value = _Person.DateOfBirth.Value;
                dtpDateOfBirth.Checked = true;
            }
            else
            {
                dtpDateOfBirth.Checked = false;
            }

            if (_Person.Gender == "M")
                cbGender.SelectedIndex = 1;
            else
                cbGender.SelectedIndex = 2;
        }

        private async void FrmPersonAddEdit_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                _Person = new PersonDTO();
                ResetDefaultValues();
            }
            else
            {
                await LoadData();
            }
        }

        private bool HasChanges()
        {
            if (_Person == null)
                return true;

            if (_PersonID == -1)
                return true;

            string UI(string s) => (s ?? "").Trim();
            string DB(string s) => (s ?? "").Trim();

            if (UI(txtName.Text) != DB(_Person.Name))
                return true;

            if (UI(txtPhoneNumber.Text) != DB(_Person.PhoneNumber))
                return true;

            if (UI(txtEmail.Text) != DB(_Person.Email))
                return true;

            if (UI(txtAddress.Text) != DB(_Person.Address))
                return true;

            DateTime? uiDateOfBirth = dtpDateOfBirth.Checked
                ? dtpDateOfBirth.Value.Date
                : (DateTime?)null;

            if (uiDateOfBirth != _Person.DateOfBirth)
            {
                return true;
            }

            string gender = cbGender.Text == "Male" ? "M" : "F";

            if (gender != _Person.Gender)
                return true;

            return false;
        }

        private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidationHelper.IsValidName(txtName.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtName, "Name is required.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(txtName, null);
        }

        private void dtpDateOfBirth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DateTime? dob = dtpDateOfBirth.Checked
                ? dtpDateOfBirth.Value
                : (DateTime?)null;

            if (!ValidationHelper.IsValidDateOfBirth(dob))
            {
                e.Cancel = true;
                epValidation.SetError(dtpDateOfBirth, "Date of birth cannot be in the future.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(dtpDateOfBirth, null);
        }

        private void cbGender_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidationHelper.IsValidGender(cbGender.Text))
            {
                e.Cancel = true;
                epValidation.SetError(cbGender, "Please select a gender.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbGender, null);
        }

        private void txtPhoneNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidationHelper.IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtPhoneNumber, "Phone number is required.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(txtPhoneNumber, null);
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtEmail, "Please enter a valid email address.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(txtEmail, null);
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =! char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private async Task AddPerson()
        {
            string gender = cbGender.Text == "Male" ? "M" : "F";

            CreatePersonDTO person = new CreatePersonDTO
            {
                Name = txtName.Text.Trim(),

                DateOfBirth = dtpDateOfBirth.Checked
                ? dtpDateOfBirth.Value.Date
                : (DateTime?)null,

                Gender = gender,
                PhoneNumber = txtPhoneNumber.Text.Trim(),

                Email = string.IsNullOrWhiteSpace(txtEmail.Text)
                        ? null
                        : txtEmail.Text.Trim(),

                Address = string.IsNullOrWhiteSpace(txtAddress.Text)
                        ? null
                        : txtAddress.Text.Trim()
            };

            var response =
                await _personService.AddPersonAsync(person);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, response.Person.PersonID);

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

        private async Task UpdatePerson()
        {
            string gender = cbGender.Text == "Male" ? "M" : "F";

            UpdatePersonDTO person = new UpdatePersonDTO
            {
                Name = txtName.Text.Trim(),

                DateOfBirth = dtpDateOfBirth.Checked
                ? dtpDateOfBirth.Value.Date
                : (DateTime?)null,

                Gender = gender,
                PhoneNumber = txtPhoneNumber.Text.Trim(),

                Email = string.IsNullOrWhiteSpace(txtEmail.Text)
                        ? null
                        : txtEmail.Text.Trim(),

                Address = string.IsNullOrWhiteSpace(txtAddress.Text)
                        ? null
                        : txtAddress.Text.Trim()
            };

            var response =
                await _personService.UpdatePersonAsync(
                    _PersonID,
                    person);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PersonID);
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
                if (_PersonID == -1)
                {
                    await AddPerson();
                }
                else
                {
                    await UpdatePerson();
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