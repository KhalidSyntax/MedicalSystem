using System;
using System.Windows.Forms;

namespace MedicalSystem.WinForms.Forms.Prescriptions
{
    public partial class FrmPrescriptionInfo : Form
    {
        private int _PrescriptionID;

        public FrmPrescriptionInfo(int prescriptionID)
        {
            InitializeComponent();
            _PrescriptionID = prescriptionID;
        }

        private async void FrmPrescriptionInfo_Load(object sender, EventArgs e)
        {
            await prescriptionInfoControl1.LoadPrescriptionInfo(_PrescriptionID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
