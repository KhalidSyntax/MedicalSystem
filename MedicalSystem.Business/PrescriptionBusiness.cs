using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class PrescriptionBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

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

        public PrescriptionDTO PDTO
        {
            get
            {
                return new PrescriptionDTO
                {
                    PrescriptionID = this.PrescriptionID,
                    MedicalRecordID = this.MedicalRecordID,

                    PatientName = this.PatientName,
                    DoctorName = this.DoctorName,

                    MedicationName = this.MedicationName,
                    Dosage = this.Dosage,
                    Frequency = this.Frequency,

                    StartDate = this.StartDate,
                    EndDate = this.EndDate,

                    SpecialInstructions = this.SpecialInstructions
                };
            }
        }

        public CreatePrescriptionDTO CreateDTO
        {
            get
            {
                return new CreatePrescriptionDTO
                {
                    MedicalRecordID = this.MedicalRecordID,

                    MedicationName = this.MedicationName,
                    Dosage = this.Dosage,
                    Frequency = this.Frequency,

                    StartDate = this.StartDate,
                    EndDate = this.EndDate,

                    SpecialInstructions = this.SpecialInstructions
                };
            }
        }

        public UpdatePrescriptionDTO UpdateDTO
        {
            get
            {
                return new UpdatePrescriptionDTO
                {
                    MedicationName = this.MedicationName,
                    Dosage = this.Dosage,
                    Frequency = this.Frequency,

                    StartDate = this.StartDate,
                    EndDate = this.EndDate,

                    SpecialInstructions = this.SpecialInstructions
                };
            }
        }

        public PrescriptionBusiness()
        {
            this.PrescriptionID = -1;
            this.MedicalRecordID = -1;

            this.PatientName = "";
            this.DoctorName = "";

            this.MedicationName = "";
            this.Dosage = "";
            this.Frequency = "";

            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;

            this.SpecialInstructions = null;

            Mode = enMode.AddNew;
        }

        public PrescriptionBusiness(
            PrescriptionDTO prescriptionDTO,
            enMode cMode = enMode.AddNew)
        {
            this.PrescriptionID = prescriptionDTO.PrescriptionID;
            this.MedicalRecordID = prescriptionDTO.MedicalRecordID;

            this.PatientName = prescriptionDTO.PatientName;
            this.DoctorName = prescriptionDTO.DoctorName;

            this.MedicationName = prescriptionDTO.MedicationName;
            this.Dosage = prescriptionDTO.Dosage;
            this.Frequency = prescriptionDTO.Frequency;

            this.StartDate = prescriptionDTO.StartDate;
            this.EndDate = prescriptionDTO.EndDate;

            this.SpecialInstructions = prescriptionDTO.SpecialInstructions;

            Mode = cMode;
        }

        private int IsValid()
        {
            if (PrescriptionID < 1 && Mode == enMode.Update)
                return -10;

            if (MedicalRecordID < 1 && Mode == enMode.AddNew)
                return -11;

            if (string.IsNullOrWhiteSpace(MedicationName))
                return -12;

            if (string.IsNullOrWhiteSpace(Dosage))
                return -13;

            if (string.IsNullOrWhiteSpace(Frequency))
                return -14;

            if (StartDate == default)
                return -15;

            if (EndDate == default)
                return -16;

            if (EndDate < StartDate)
                return -17;

            return 1;
        }

        public static PrescriptionBusiness? Find(int PrescriptionID)
        {
            PrescriptionDTO? prescriptionDTO =
                PrescriptionData.GetPrescriptionByID(PrescriptionID);

            if (prescriptionDTO != null)
                return new PrescriptionBusiness(
                    prescriptionDTO,
                    enMode.Update);
            else
                return null;
        }

        public static List<PrescriptionDTO> GetAllPrescriptions()
        {
            return PrescriptionData.GetAllPrescriptions();
        }

        private int _AddNewPrescription()
        {
            int result =
                PrescriptionData.AddNewPrescription(CreateDTO);

            if (result > 0)
            {
                this.PrescriptionID = result;
                return 1;
            }

            return result;
        }

        private int _UpdatePrescription()
        {
            return PrescriptionData.UpdatePrescription(
                this.PrescriptionID,
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

                    int result = _AddNewPrescription();

                    if (result == 1)
                    {
                        Mode = enMode.Update;
                        return 1;
                    }

                    return result;

                case enMode.Update:

                    return _UpdatePrescription();
            }

            return 0;
        }

        public static int DeletePrescription(int PrescriptionID)
        {
            return PrescriptionData.DeletePrescription(PrescriptionID);
        }
    }
}