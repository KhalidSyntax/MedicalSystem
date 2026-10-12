using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Doctors
{
    public partial class FrmDoctorAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int DoctorID);
        public DataBackEventHandler DataBack;

        private int _DoctorID = -1;
        private DoctorDTO _Doctor;

        private readonly DoctorService _doctorService;

        public FrmDoctorAddEdit()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
        }

        public FrmDoctorAddEdit(int doctorID)
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            _DoctorID = doctorID;
        }

        private void ResetDefaultValues()
        {
            lblTitle.Text = "Add New Doctor";
            this.Text = "Add New Doctor";

            lblPersonName.Visible = false;
            cbPersons.Visible = true;

            cbPersons.SelectedItem = null;
            txtSpecialization.Clear();
        }

        private async Task LoadData()
        {
            try
            {
                _Doctor = await _doctorService.GetDoctorByIDAsync(_DoctorID);

                if (_Doctor == null)
                {
                    MessageBox.Show(
                        $"Doctor with ID = {_DoctorID} was not found.",
                        "Doctor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    this.Close();
                    return;
                }

                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";

                cbPersons.Visible = false;
                lblPersonName.Visible = true;
                lblPersonName.Text = _Doctor.Name;
                txtSpecialization.Text = _Doctor.Specialization;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load doctor.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task LoadAvailablePerson()
        {
            try
            {
                List<AvailablePersonDTO> persons =
                    await _doctorService.GetAvailablePersonsAsync();

                cbPersons.Visible = true;
                cbPersons.DataSource = persons;
                cbPersons.DisplayMember = "Name";
                cbPersons.ValueMember = "PersonID";

                if (persons.Count == 0)
                {
                    MessageBox.Show(
                        "There are no available persons.",
                        "Add Doctor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load doctor.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnSave.Enabled = false;
            }
        }

        private async void FrmDoctorAddEdit_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_DoctorID == -1)
            {
                _Doctor = new DoctorDTO();
                await LoadAvailablePerson();
            }
            else
            {
                await LoadData();
            }
        }

        private bool HasChanges()
        {
            if (_Doctor == null)
                return true;

            if (_DoctorID == -1)
                return true;

            string UI(string s) => (s ?? "").Trim();
            string DB(string s) => (s ?? "").Trim();

            if (UI(txtSpecialization.Text) != DB(_Doctor.Specialization))
                return true;

            return false;
        }

        private void cbPersons_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_DoctorID == -1 && cbPersons.SelectedItem == null)
            {
                e.Cancel = true;
                epValidation.SetError(cbPersons, "Please select a person.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(cbPersons, null);
        }

        private void txtSpecialization_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                e.Cancel = true;
                epValidation.SetError(txtSpecialization, "Specialization is required.");
                return;
            }

            e.Cancel = false;
            epValidation.SetError(txtSpecialization, null);
        }

        private async Task AddDoctor()
        {
            int personID = Convert.ToInt32(cbPersons.SelectedValue);
            string specialization = txtSpecialization.Text.Trim();

            CreateDoctorDTO createDoctor = new CreateDoctorDTO
            {
                PersonID = personID,
                Specialization = specialization
            };

            var response = await _doctorService.AddDoctorAsync(createDoctor);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (response.Doctor != null)
                    DataBack?.Invoke(this, response.Doctor.DoctorID);

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

        private async Task UpdateDoctor()
        {
            UpdateDoctorDTO doctor = new UpdateDoctorDTO
            {
                Specialization = txtSpecialization.Text.Trim()
            };

            var response =
                await _doctorService.UpdateDoctorAsync(
                    _DoctorID,
                    doctor);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _DoctorID);
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
                if (_DoctorID == -1)
                {
                    await AddDoctor();
                }
                else
                {
                    await UpdateDoctor();
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
