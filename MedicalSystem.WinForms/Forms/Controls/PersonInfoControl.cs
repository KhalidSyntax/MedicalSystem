using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Controls
{
    public partial class PersonInfoControl : UserControl
    {
        private PersonDTO _Person;
        private int _PersonID = -1;

        private readonly PersonService _personService;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public PersonDTO SelectedPersonInfo
        {
            get { return _Person; }
        }

        public PersonInfoControl()
        {
            InitializeComponent();

            _personService = new PersonService();
        }

        public void ResetPersonInfo()
        {
            _Person = null;
            _PersonID = -1;

            lblPersonID.Text = "[???]";
            lblName.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblGender.Text = "[???]";
            lblPhoneNumber.Text = "[???]";
            lblEmail.Text = "[???]";
            txtAddress.Clear();
        }

        public async Task LoadPersonInfo(int personID)
        {
            _Person =
                await _personService.GetPersonByIDAsync(personID);

            if (_Person == null)
            {
                ResetPersonInfo();

                MessageBox.Show(
                    $"No Person With PersonID = {personID}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            FillPersonInfo();
        }

        private void FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            lblPersonID.Text =
                _PersonID.ToString();

            lblName.Text =
                _Person.Name;

            lblDateOfBirth.Text =
                _Person.DateOfBirth.HasValue
                    ? _Person.DateOfBirth.Value.ToShortDateString()
                    : "[???]";

            lblGender.Text =
                _Person.GenderText;

            lblPhoneNumber.Text =
                _Person.PhoneNumber;

            lblEmail.Text =
                _Person.Email ?? "[???]";

            txtAddress.Text =
                _Person.Address ?? "";
        }
    }
}