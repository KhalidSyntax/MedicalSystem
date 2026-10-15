using MedicalSystem.WinForms.Forms.Appointment;
using MedicalSystem.WinForms.Forms.Doctor;
using MedicalSystem.WinForms.Forms.MedicalRecord;
using MedicalSystem.WinForms.Forms.Patient;
using MedicalSystem.WinForms.Forms.Payments;
using MedicalSystem.WinForms.Forms.Person;
using MedicalSystem.WinForms.Forms.Prescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
