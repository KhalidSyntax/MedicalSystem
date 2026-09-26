namespace MedicalSystem.DTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public int AppointmentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string? AdditionalNotes { get; set; }
    }

    public class CreatePaymentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string? AdditionalNotes { get; set; }
    }

    public class UpdatePaymentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}