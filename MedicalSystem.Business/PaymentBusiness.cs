using MedicalSystem.DataAccess;
using MedicalSystem.DTOs;

namespace MedicalSystem.Business
{
    public class PaymentBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PaymentID { get; set; }

        public int AppointmentID { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? PaymentMethod { get; set; }

        public decimal AmountPaid { get; set; }

        public string? AdditionalNotes { get; set; }

        public PaymentDTO PDTO
        {
            get
            {
                return new PaymentDTO
                {
                    PaymentID = this.PaymentID,
                    AppointmentID = this.AppointmentID,
                    PaymentDate = this.PaymentDate,
                    PaymentMethod = this.PaymentMethod,
                    AmountPaid = this.AmountPaid,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public CreatePaymentDTO CreateDTO
        {
            get
            {
                return new CreatePaymentDTO
                {
                    AppointmentID = this.AppointmentID,
                    PaymentDate = this.PaymentDate,
                    PaymentMethod = this.PaymentMethod,
                    AmountPaid = this.AmountPaid,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public UpdatePaymentDTO UpdateDTO
        {
            get
            {
                return new UpdatePaymentDTO
                {
                    AppointmentID = this.AppointmentID,
                    PaymentDate = this.PaymentDate,
                    PaymentMethod = this.PaymentMethod,
                    AmountPaid = this.AmountPaid,
                    AdditionalNotes = this.AdditionalNotes
                };
            }
        }

        public PaymentBusiness()
        {
            this.PaymentID = -1;

            this.AppointmentID = -1;

            this.PaymentDate = DateTime.Now;

            this.PaymentMethod = null;

            this.AmountPaid = 0;

            this.AdditionalNotes = null;

            Mode = enMode.AddNew;
        }

        public PaymentBusiness(
            PaymentDTO paymentDTO,
            enMode cMode = enMode.AddNew)
        {
            this.PaymentID = paymentDTO.PaymentID;

            this.AppointmentID = paymentDTO.AppointmentID;

            this.PaymentDate = paymentDTO.PaymentDate;

            this.PaymentMethod = paymentDTO.PaymentMethod;

            this.AmountPaid = paymentDTO.AmountPaid;

            this.AdditionalNotes = paymentDTO.AdditionalNotes;

            Mode = cMode;
        }

        private int IsValid()
        {
            if (AppointmentID < 1)
                return -10;

            if (PaymentID < 1 && Mode == enMode.Update)
                return -11;

            if (AmountPaid <= 0)
                return -12;

            if (PaymentDate == default)
                return -13;

            return 1;
        }

        public static PaymentBusiness? Find(int PaymentID)
        {
            PaymentDTO? paymentDTO =
                PaymentData.GetPaymentByID(PaymentID);

            if (paymentDTO != null)
                return new PaymentBusiness(
                    paymentDTO,
                    enMode.Update);
            else
                return null;
        }

        public static List<PaymentDTO> GetAllPayments()
        {
            return PaymentData.GetAllPayments();
        }

        private int _AddNewPayment()
        {
            int result =
                PaymentData.AddNewPayment(CreateDTO);

            if (result > 0)
            {
                this.PaymentID = result;
                return 1;
            }

            return result;
        }

        private int _UpdatePayment()
        {
            return PaymentData.UpdatePayment(
                this.PaymentID,
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

                    int result = _AddNewPayment();

                    if (result == 1)
                    {
                        Mode = enMode.Update;
                        return 1;
                    }

                    return result;

                case enMode.Update:

                    return _UpdatePayment();
            }

            return 0;
        }

        public static int DeletePayment(int PaymentID)
        {
            return PaymentData.DeletePayment(PaymentID);
        }
    }
}