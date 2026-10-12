using System;

namespace MedicalSystem.WinForms.DTOs
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }

        public int PatientID { get; set; }
        public string PatientName { get; set; } = "";

        public int DoctorID { get; set; }
        public string DoctorName { get; set; } = "";

        public DateTime AppointmentDateTime { get; set; }

        public byte AppointmentStatus { get; set; }

        public string StatusText
        {
            get
            {
                switch (AppointmentStatus)
                {
                    case 1: return "Pending";
                    case 2: return "Confirmed";
                    case 3: return "Completed";
                    case 4: return "Canceled";
                    case 5: return "Rescheduled";
                    case 6: return "No-Show";
                    default: return "Unknown";
                }
            }
        }
    }

    public class CreateAppointmentDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public byte AppointmentStatus { get; set; }
    }

    public class UpdateAppointmentDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public byte AppointmentStatus { get; set; }
    }
}
