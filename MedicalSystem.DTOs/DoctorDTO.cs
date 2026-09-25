namespace MedicalSystem.DTOs
{
    public class DoctorDTO
    {
        public int DoctorID { get; set; }
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
        public string Specialization { get; set; } = "";

    }

    public class CreateDoctorDTO
    {
        public int PersonID { get; set; }
        public string Specialization { get; set; } = "";
    }

    public class UpdateDoctorDTO
    {
        public string Specialization { get; set; } = "";
    }
}
