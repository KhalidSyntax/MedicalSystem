using MedicalSystem.WinForms.Forms.Controls;
using System;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms
{
    public partial class FrmPersonDetails : Form
    {
        private int _PersonID;
        public FrmPersonDetails(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private async void FrmPersonDetails_Load(object sender, EventArgs e)
        {
            await personInfoControl1.LoadPersonInfo(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
