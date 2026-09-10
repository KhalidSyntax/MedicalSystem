using MedicalSystem.Common;
using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class PersonBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string? Email { get; set; }
        public string? Address { get; set; }

        public PersonDTO PDTO
        {
            get
            {
                return new PersonDTO
                {
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

        public CreatePersonDTO CreateDTO
        {
            get
            {
                return new CreatePersonDTO
                {
                    Name = this.Name,
                    DateOfBirth = this.DateOfBirth,
                    Gender = this.Gender,
                    PhoneNumber = this.PhoneNumber,
                    Email = this.Email,
                    Address = this.Address
                };
            }
        }

        public UpdatePersonDTO UpdateDTO
        {
            get
            {
                return new UpdatePersonDTO
                {
                    Name = this.Name,
                    DateOfBirth = this.DateOfBirth,
                    Gender = this.Gender,
                    PhoneNumber = this.PhoneNumber,
                    Email = this.Email,
                    Address = this.Address
                };
            }
        }

        public PersonBusiness()
        {
            this.PersonID = -1;
            this.Name = "";
            this.DateOfBirth = null;
            this.Gender = false;
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";

            Mode = enMode.AddNew;
        }

        public PersonBusiness(PersonDTO personDTO, enMode cMode = enMode.AddNew)
        {
            this.PersonID = personDTO.PersonID;
            this.Name = personDTO.Name;
            this.DateOfBirth = personDTO.DateOfBirth;
            this.Gender = personDTO.Gender;
            this.PhoneNumber = personDTO.PhoneNumber;
            this.Email = personDTO.Email;
            this.Address = personDTO.Address;

            Mode = cMode;
        }

        private int IsValid()
        {
            if (!ValidationHelper.IsValidName(Name))
                return -10;

            if (!ValidationHelper.IsValidPhoneNumber(PhoneNumber))
                return -11;

            if (!ValidationHelper.IsValidEmail(Email))
                return -12;

            if (!ValidationHelper.IsValidDateOfBirth(DateOfBirth))
                return -13;

            return 1;
        }

        public static PersonBusiness? Find(int PersonID)
        {
            PersonDTO? personDTO = PersonData.GetPersonByID(PersonID);

            if (personDTO != null)
                return new PersonBusiness(personDTO, enMode.Update);
            else
                return null;
        }

        public static List<PersonDTO> GetAllPersons()
        {
            return PersonData.GetAllPersons();
        }

        private bool _AddNewPerson()
        {
            this.PersonID = PersonData.AddNewPerson(CreateDTO);
            return (this.PersonID != -1);
        }

        private int _UpdatePerson()
        {
            return PersonData.UpdatePerson(
                this.PersonID,
                this.UpdateDTO);
        }

        public int Save()
        {
            int validationResult = IsValid();

            if (validationResult != 1)
                return validationResult;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }
            return 0;
        }

        public static int DeletePerson(int PersonID)
        {
            return PersonData.DeletePerson(PersonID);
        }
    }
}