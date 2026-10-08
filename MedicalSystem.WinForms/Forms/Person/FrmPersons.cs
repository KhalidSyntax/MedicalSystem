using MedicalSystem.WinForms.DTOs;
using MedicalSystem.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Person
{
    public partial class FrmPersons : Form
    {
        private readonly PersonService _personService;
        private List<PersonDTO> _allPersons;

        public FrmPersons()
        {
            InitializeComponent();

            _personService = new PersonService();
        }

        private void FormatDataGridView()
        {
            dgvPersons.Columns["PersonID"].HeaderText = "ID";
            dgvPersons.Columns["PersonID"].Width = 70;

            dgvPersons.Columns["Name"].HeaderText = "Name";
            dgvPersons.Columns["Name"].Width = 220;

            dgvPersons.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvPersons.Columns["DateOfBirth"].Width = 140;

            dgvPersons.Columns["GenderText"].HeaderText = "Gender";
            dgvPersons.Columns["GenderText"].Width = 100;

            dgvPersons.Columns["PhoneNumber"].HeaderText = "Phone";
            dgvPersons.Columns["PhoneNumber"].Width = 140;

            dgvPersons.Columns["Email"].HeaderText = "Email";
            dgvPersons.Columns["Email"].Width = 250;

            dgvPersons.Columns["Gender"].Visible = false;
            dgvPersons.Columns["Address"].Visible = false;
        }

        private async Task LoadPersons()
        {
            _allPersons = await _personService.GetAllPersonsAsync();

            if (_allPersons == null || _allPersons.Count == 0)
            {
                dgvPersons.DataSource = null;
                lblRecordCount.Text = "0";

                MessageBox.Show(
                    "There are no persons.",
                    "Persons",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            cbFilterBy.SelectedIndex = 0;
            dgvPersons.DataSource = _allPersons;

            FormatDataGridView();

            lblRecordCount.Text = dgvPersons.Rows.Count.ToString();
        }

        private async void FrmPersons_Load(object sender, EventArgs e)
        {
            await LoadPersons();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_allPersons == null || _allPersons.Count <= 0)
            {
                return;
            }

            string FilterColumn = "";

            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "ID": FilterColumn = "PersonID"; break;
                case "Name": FilterColumn = "Name"; break;
                case "Phone": FilterColumn = "PhoneNumber"; break;
                case "Email": FilterColumn = "Email"; break;
                default: FilterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dgvPersons.DataSource = _allPersons;
                lblRecordCount.Text = dgvPersons.Rows.Count.ToString();
                return;
            }

            string FilterValue = txtFilterValue.Text.Trim();
            List<PersonDTO> FilteredPersons;

            switch(FilterColumn)
            {
                case "PersonID":
                    FilteredPersons = _allPersons
                        .Where(p => p.PersonID.ToString() == FilterValue)
                        .ToList();
                    break;

                case "Name":
                    FilteredPersons = _allPersons
                        .Where(p => p.Name.StartsWith(FilterValue, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case "PhoneNumber":
                    FilteredPersons = _allPersons
                        .Where(p => p.PhoneNumber.StartsWith(FilterValue, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                default:
                    FilteredPersons = _allPersons
                        .Where(p => p.Email != null && p.Email.StartsWith(FilterValue, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
            }

            dgvPersons.DataSource = FilteredPersons;
            lblRecordCount.Text = dgvPersons.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private async void Frm_DataBack(object sender, int personID)
        {
            await LoadPersons();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPersonAddEdit frm = new FrmPersonAddEdit();

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPersons.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a person first.",
                    "Edit Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int personID = Convert.ToInt32(dgvPersons.CurrentRow.Cells["PersonID"].Value);
            FrmPersonAddEdit frm = new FrmPersonAddEdit(personID);

            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvPersons.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a person first.",
                    "Delete Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int personID = Convert.ToInt32(dgvPersons.CurrentRow.Cells["PersonID"].Value);
            string personName = dgvPersons.CurrentRow.Cells["Name"].Value.ToString();

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete '{personName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var result =
                    await _personService.DeletePersonAsync(personID);

                MessageBox.Show(
                    result.Message,
                    "Delete Person",
                    MessageBoxButtons.OK,
                    result.StatusCode == HttpStatusCode.OK
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await LoadPersons();
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

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgvPersons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int personID = Convert.ToInt32(dgvPersons.CurrentRow.Cells["PersonID"].Value);

            FrmPersonDetails frm = new FrmPersonDetails(personID);
            frm.ShowDialog();
        }
    }
}
