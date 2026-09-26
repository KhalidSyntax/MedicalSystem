
namespace MedicalSystem.DTOs
{
    public class PrescriptionDTO
    {
        public int PrescriptionID { get; set; }

        public int MedicalRecordID { get; set; }

        public string PatientName { get; set; } = "";

        public string DoctorName { get; set; } = "";

        public string MedicationName { get; set; } = "";

        public string Dosage { get; set; } = "";

        public string Frequency { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? SpecialInstructions { get; set; }
    }

    public class CreatePrescriptionDTO
    {
        public int MedicalRecordID { get; set; }

        public string MedicationName { get; set; } = "";

        public string Dosage { get; set; } = "";

        public string Frequency { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? SpecialInstructions { get; set; }
    }

    public class UpdatePrescriptionDTO
    {
        public string MedicationName { get; set; } = "";

        public string Dosage { get; set; } = "";

        public string Frequency { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? SpecialInstructions { get; set; }
    }
}
