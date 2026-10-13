namespace MedicalSystem.WinForms.DTOs
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordID { get; set; }
        public int AppointmentID { get; set; }
        public string PatientName { get; set; } = "";
        public string DoctorName { get; set; } = "";
        public string VisitDescription { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public string AdditionalNotes { get; set; } = "";
    }

    public class CreateMedicalRecordDTO
    {
        public int AppointmentID { get; set; }
        public string VisitDescription { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public string AdditionalNotes { get; set; } = "";
    }

    public class UpdateMedicalRecordDTO
    {
        public string VisitDescription { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public string AdditionalNotes { get; set; } = "";
    }
}