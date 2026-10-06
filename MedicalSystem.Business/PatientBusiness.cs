using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class PatientBusiness
    {
        public int PatientID { get; set; }
        public int PersonID { get; set; }
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string? Email { get; set; }
        public string? Address { get; set; }

        public PatientDTO PDTO
        {
            get
            {
                return new PatientDTO
                {
                    PatientID = this.PatientID,
                    PersonID = this.PersonID,
                    Name = this.Name,
                    DateOfBirth = this.DateOfBirth,
                    Gender = this.Gender,
                    PhoneNumber = this.PhoneNumber,
                    Email = this.Email,
                    Address = this.Address
                };
            }
        }

        public CreatePatientDTO CreateDTO
        {
            get
            {
                return new CreatePatientDTO
                {
                    PersonID = this.PersonID
                };
            }
        }

        public PatientBusiness()
        {
            this.PatientID = -1;
            this.PersonID = -1;

            this.Name = "";
            this.DateOfBirth = null;
            this.Gender = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";
        }

        public PatientBusiness(PatientDTO patientDTO)
        {
            this.PatientID = patientDTO.PatientID;
            this.PersonID = patientDTO.PersonID;
            this.Name = patientDTO.Name;
            this.DateOfBirth = patientDTO.DateOfBirth;
            this.Gender = patientDTO.Gender;
            this.PhoneNumber = patientDTO.PhoneNumber;
            this.Email = patientDTO.Email;
            this.Address = patientDTO.Address;
        }

        public static PatientBusiness? Find(int PatientID)
        {
            PatientDTO? patientDTO = PatientData.GetPatientByID(PatientID);

            if (patientDTO != null)
                return new PatientBusiness(patientDTO);
            else
                return null;
        }

        public static List<PatientDTO> GetAllPatients()
        {
            return PatientData.GetAllPatients();
        }

        public int AddNewPatient()
        {
            this.PatientID = PatientData.AddNewPatient(CreateDTO);
            return this.PatientID;
        }

        public static int DeletePatient(int PatientID)
        {
            return PatientData.DeletePatient(PatientID);
        }
    }
}