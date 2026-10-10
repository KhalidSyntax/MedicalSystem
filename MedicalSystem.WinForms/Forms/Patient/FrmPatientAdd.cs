using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MedicalSystem.WinForms.Forms.Patient
{
    public partial class FrmPatientAdd : Form
    {
        private readonly PatientService _patientService;

        public delegate void DataBackEventHandler(object sender, int PersonID);
        public DataBackEventHandler DataBack;

        public FrmPatientAdd()
        {
            InitializeComponent();

            _patientService = new PatientService();

        }

        private async Task LoadAvailablePerson()
        {
            try
            {
                List<AvailablePersonDTO> persons = 
                    await _patientService.GetAvailablePersonsAsync();

                cbPersons.DataSource = persons;
                cbPersons.DisplayMember = "Name";
                cbPersons.ValueMember = "PersonID";

                if(persons.Count == 0)
                {
                    MessageBox.Show(
                        "There are no available persons.",
                        "Add Patient",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load persons.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnAdd.Enabled = false;
            }
        }

        private async void FrmPatientAdd_Load(object sender, EventArgs e)
        {
            await LoadAvailablePerson();
        }

        private async Task AddPatient()
        {
            int personID = Convert.ToInt32(cbPersons.SelectedValue);

            var response = await _patientService.AddPatientAsync(personID);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show(
                    response.Message,
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (response.patient != null)
                    DataBack?.Invoke(this, response.patient.PersonID);

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

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if(cbPersons.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a person.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            await AddPatient();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
