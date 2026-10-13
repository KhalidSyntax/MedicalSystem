using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class MedicalRecordBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MedicalRecordID { get; set; }

        public int AppointmentID { get; set; }

        public string PatientName { get; set; } = "";
        public string DoctorName { get; set; } = "";

        public string? VisitDescription { get; set; }
        public string? Diagnosis { get; set; }
        public string? AdditionalNotes { get; set; }

        public MedicalRecordDTO MRDTO
        {
            get
            {
                return new MedicalRecordDTO
                {
                    MedicalRecordID = this.MedicalRecordID,

                    AppointmentID = this.AppointmentID,

                    PatientName = this.PatientName,
                    DoctorName = this.DoctorName,

                    VisitDescription = this.VisitDescription,
                    Diagnosis = this.Diagnosis,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public CreateMedicalRecordDTO CreateDTO
        {
            get
            {
                return new CreateMedicalRecordDTO
                {
                    AppointmentID = this.AppointmentID,

                    VisitDescription = this.VisitDescription,
                    Diagnosis = this.Diagnosis,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public UpdateMedicalRecordDTO UpdateDTO
        {
            get
            {
                return new UpdateMedicalRecordDTO
                {
                    VisitDescription = this.VisitDescription,
                    Diagnosis = this.Diagnosis,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public MedicalRecordBusiness()
        {
            this.MedicalRecordID = -1;

            this.AppointmentID = -1;

            this.PatientName = "";
            this.DoctorName = "";

            this.VisitDescription = null;
            this.Diagnosis = null;
            this.AdditionalNotes = null;

            Mode = enMode.AddNew;
        }

        public MedicalRecordBusiness(
            MedicalRecordDTO medicalRecordDTO,
            enMode cMode = enMode.AddNew)
        {
            this.MedicalRecordID = medicalRecordDTO.MedicalRecordID;

            this.AppointmentID = medicalRecordDTO.AppointmentID;

            this.PatientName = medicalRecordDTO.PatientName;
            this.DoctorName = medicalRecordDTO.DoctorName;

            this.VisitDescription = medicalRecordDTO.VisitDescription;
            this.Diagnosis = medicalRecordDTO.Diagnosis;
            this.AdditionalNotes = medicalRecordDTO.AdditionalNotes;

            Mode = cMode;
        }

        private int IsValid()
        {
            if (AppointmentID < 1)
                return -10;

            return 1;
        }

        public static MedicalRecordBusiness? Find(int MedicalRecordID)
        {
            MedicalRecordDTO? medicalRecordDTO =
                MedicalRecordData.GetMedicalRecordByID(MedicalRecordID);

            if (medicalRecordDTO != null)
                return new MedicalRecordBusiness(
                    medicalRecordDTO,
                    enMode.Update);
            else
                return null;
        }

        public static List<MedicalRecordDTO> GetAllMedicalRecords()
        {
            return MedicalRecordData.GetAllMedicalRecords();
        }

        public static List<MedicalRecordDTO> GetMedicalRecordsLookup()
        {
            return MedicalRecordData.GetMedicalRecordsLookup();
        }

        private int _AddNewMedicalRecord()
        {
            int result =
                MedicalRecordData.AddNewMedicalRecord(CreateDTO);

            if (result > 0)
            {
                this.MedicalRecordID = result;
                return 1;
            }

            return result;
        }

        private int _UpdateMedicalRecord()
        {
            return MedicalRecordData.UpdateMedicalRecord(
                this.MedicalRecordID,
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

                    int result = _AddNewMedicalRecord();

                    if (result == 1)
                    {
                        Mode = enMode.Update;
                        return 1;
                    }

                    return result;

                case enMode.Update:

                    return _UpdateMedicalRecord();
            }

            return 0;
        }

        public static int DeleteMedicalRecord(int MedicalRecordID)
        {
            return MedicalRecordData.DeleteMedicalRecord(MedicalRecordID);
        }
    }
}