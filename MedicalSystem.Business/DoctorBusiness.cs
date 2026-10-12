using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class DoctorBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DoctorID { get; set; }
        public int PersonID { get; set; }
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string Specialization { get; set; } = "";

        public DoctorDTO DDTO
        {
            get
            {
                return new DoctorDTO
                {
                    DoctorID = this.DoctorID,
                    PersonID = this.PersonID,
                    Name = this.Name,
                    DateOfBirth = this.DateOfBirth,
                    Gender = this.Gender,
                    PhoneNumber = this.PhoneNumber,
                    Email = this.Email,
                    Address = this.Address,
                    Specialization = this.Specialization
                };
            }
        }

        public CreateDoctorDTO CreateDTO
        {
            get
            {
                return new CreateDoctorDTO
                {
                    PersonID = this.PersonID,
                    Specialization = this.Specialization
                };
            }
        }

        public UpdateDoctorDTO UpdateDTO
        {
            get
            {
                return new UpdateDoctorDTO
                {
                    Specialization = this.Specialization
                };
            }
        }

        public DoctorBusiness()
        {
            this.DoctorID = -1;
            this.PersonID = -1;
            this.Name = "";
            this.DateOfBirth = null;
            this.Gender = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";
            this.Specialization = "";

            Mode = enMode.AddNew;
        }

        public DoctorBusiness(DoctorDTO doctorDTO, enMode cMode = enMode.AddNew)
        {
            this.DoctorID = doctorDTO.DoctorID;
            this.PersonID = doctorDTO.PersonID;
            this.Name = doctorDTO.Name;
            this.DateOfBirth = doctorDTO.DateOfBirth;
            this.Gender = doctorDTO.Gender;
            this.PhoneNumber = doctorDTO.PhoneNumber;
            this.Email = doctorDTO.Email;
            this.Address = doctorDTO.Address;
            this.Specialization = doctorDTO.Specialization;

            Mode = cMode;
        }

        public static DoctorBusiness? Find(int DoctorID)
        {
            DoctorDTO? doctorDTO = DoctorData.GetDoctorByID(DoctorID);

            if (doctorDTO != null)
                return new DoctorBusiness(doctorDTO, enMode.Update);
            else
                return null;
        }

        public static List<DoctorDTO> GetAllDoctors()
        {
            return DoctorData.GetAllDoctors();
        }

        public static List<AvailablePersonDTO> GetAvailablePersons()
        {
            return DoctorData.GetAvailablePersons();
        }

        private bool _AddNewDoctor()
        {
            int result = DoctorData.AddNewDoctor(CreateDTO);

            if (result > 0)
            {
                this.DoctorID = result;
                return true;
            }

            return false;
        }

        private int _UpdateDoctor()
        {
            return DoctorData.UpdateDoctor(
                this.DoctorID,
                this.UpdateDTO);
        }

        private int IsValid()
        {
            if (string.IsNullOrWhiteSpace(Specialization))
                return -10;

            return 1;
        }

        public int Save()
        {
            int validationResult = IsValid();

            if (validationResult != 1)
                return validationResult;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDoctor())
                    {
                        Mode = enMode.Update;
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                case enMode.Update:

                    return _UpdateDoctor();
            }

            return 0;
        }

        public static int DeleteDoctor(int DoctorID)
        {
            return DoctorData.DeleteDoctor(DoctorID);
        }
    }
}