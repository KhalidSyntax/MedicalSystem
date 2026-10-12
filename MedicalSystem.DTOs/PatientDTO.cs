namespace MedicalSystem.DTOs
{
    public class PatientDTO
    {
        public int PatientID { get; set; }
        public int PersonID { get; set; }
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string GenderText
        {
            get
            {
                return Gender == "F" ? "Female" : "Male";
            }
        }
        public string PhoneNumber { get; set; } = "";
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    public class CreatePatientDTO
    {
        public int PersonID { get; set; }
    }

    public class AvailablePersonDTO
    {
        public int PersonID { get; set; }
        public string Name { get; set; } = "";
    }

    public class PatientLookupDTO
    {
        public int PatientID { get; set; }
        public string Name { get; set; } = "";
    }
}