using System;

namespace MedicalSystem.WinForms.DTOs
{
    public class PersonDTO
    {
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
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class CreatePersonDTO
    {
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class UpdatePersonDTO
    {
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
