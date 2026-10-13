using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class AppointmentBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int AppointmentID { get; set; }

        public int PatientID { get; set; }
        public string PatientName { get; set; } = "";

        public int DoctorID { get; set; }
        public string DoctorName { get; set; } = "";

        public DateTime AppointmentDateTime { get; set; }

        public byte AppointmentStatus { get; set; }

        public AppointmentDTO ADTO
        {
            get
            {
                return new AppointmentDTO
                {
                    AppointmentID = this.AppointmentID,

                    PatientID = this.PatientID,
                    PatientName = this.PatientName,

                    DoctorID = this.DoctorID,
                    DoctorName = this.DoctorName,

                    AppointmentDateTime = this.AppointmentDateTime,
                    AppointmentStatus = this.AppointmentStatus
                };
            }
        }

        public CreateAppointmentDTO CreateDTO
        {
            get
            {
                return new CreateAppointmentDTO
                {
                    PatientID = this.PatientID,
                    DoctorID = this.DoctorID,
                    AppointmentDateTime = this.AppointmentDateTime,
                    AppointmentStatus = this.AppointmentStatus
                };
            }
        }

        public UpdateAppointmentDTO UpdateDTO
        {
            get
            {
                return new UpdateAppointmentDTO
                {
                    PatientID = this.PatientID,
                    DoctorID = this.DoctorID,
                    AppointmentDateTime = this.AppointmentDateTime,
                    AppointmentStatus = this.AppointmentStatus
                };
            }
        }

        public AppointmentBusiness()
        {
            this.AppointmentID = -1;

            this.PatientID = -1;
            this.PatientName = "";

            this.DoctorID = -1;
            this.DoctorName = "";

            this.AppointmentDateTime = DateTime.Now;

            this.AppointmentStatus = 1;

            Mode = enMode.AddNew;
        }

        public AppointmentBusiness(
            AppointmentDTO appointmentDTO,
            enMode cMode = enMode.AddNew)
        {
            this.AppointmentID = appointmentDTO.AppointmentID;

            this.PatientID = appointmentDTO.PatientID;
            this.PatientName = appointmentDTO.PatientName;

            this.DoctorID = appointmentDTO.DoctorID;
            this.DoctorName = appointmentDTO.DoctorName;

            this.AppointmentDateTime = appointmentDTO.AppointmentDateTime;

            this.AppointmentStatus = appointmentDTO.AppointmentStatus;

            Mode = cMode;
        }

        private int IsValid()
        {
            if (AppointmentDateTime < DateTime.Now)
                return -10;

            if (AppointmentStatus < 1 || AppointmentStatus > 6)
                return -11;

            return 1;
        }

        public static AppointmentBusiness? Find(int AppointmentID)
        {
            AppointmentDTO? appointmentDTO =
                AppointmentData.GetAppointmentByID(AppointmentID);

            if (appointmentDTO != null)
                return new AppointmentBusiness(
                    appointmentDTO,
                    enMode.Update);
            else
                return null;
        }

        public static List<AppointmentDTO> GetAllAppointments()
        {
            return AppointmentData.GetAllAppointments();
        }

        public static List<AppointmentDTO> GetAppointmentsByDoctor(int DoctorID)
        {
            return AppointmentData.GetAppointmentsByDoctor(DoctorID);
        }

        public static List<AppointmentDTO> GetAppointmentsByPatient(int PatientID)
        {
            return AppointmentData.GetAppointmentsByPatient(PatientID);
        }

        public static List<AppointmentLookupDTO> GetAvailableForMedicalRecord()
        {
            return AppointmentData.GetAvailableForMedicalRecord();
        }

        private int _AddNewAppointment()
        {
            int result = AppointmentData.AddNewAppointment(CreateDTO);

            if (result > 0)
            {
                this.AppointmentID = result;
                return 1;
            }

            return result;
        }

        private int _UpdateAppointment()
        {
            return AppointmentData.UpdateAppointment(
                this.AppointmentID,
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

                    int result = _AddNewAppointment();

                    if (result == 1)
                    {
                        Mode = enMode.Update;
                        return 1;
                    }

                    return result;

                case enMode.Update:

                    return _UpdateAppointment();
            }

            return 0;
        }

        public static int DeleteAppointment(int AppointmentID)
        {
            return AppointmentData.DeleteAppointment(AppointmentID);
        }
    }
}