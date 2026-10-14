using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPayments")]
        [ProducesResponseType(typeof(IEnumerable<PaymentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PaymentDTO>> GetAllPayments()
        {
            List<PaymentDTO> PaymentList =
                PaymentBusiness.GetAllPayments();

            if (!PaymentList.Any())
                return NotFound("There are no Payments");

            return Ok(PaymentList);
        }

        [HttpGet("{PaymentID}", Name = "GetPaymentByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaymentDTO), StatusCodes.Status200OK)]
        public ActionResult<PaymentDTO> GetPaymentByID(int PaymentID)
        {
            if (PaymentID < 1)
                return BadRequest($"Not accepted ID {PaymentID}");

            PaymentBusiness? payment =
                PaymentBusiness.Find(PaymentID);

            if (payment == null)
                return NotFound(
                    $"Payment with ID {PaymentID} not found!");

            return Ok(payment.PDTO);
        }

        [HttpPost(Name = "AddNewPayment")]
        [ProducesResponseType(typeof(PaymentDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<PaymentDTO> AddNewPayment(CreatePaymentDTO createPayment)
        {
            if (createPayment == null)
                return BadRequest("Invalid Payment data.");

            PaymentBusiness payment = new PaymentBusiness();

            payment.AppointmentID = createPayment.AppointmentID;
            payment.PaymentDate = createPayment.PaymentDate;
            payment.PaymentMethod = createPayment.PaymentMethod;
            payment.AmountPaid = createPayment.AmountPaid;
            payment.AdditionalNotes = createPayment.AdditionalNotes;

            int result = payment.Save();

            switch (result)
            {
                case -10:
                    return BadRequest(
                        "AppointmentID is required.");

                case -12:
                    return BadRequest(
                        "Amount Paid must be greater than 0.");

                case -13:
                    return BadRequest(
                        "Payment Date is required.");

                case -1:
                    return NotFound(
                        $"Appointment with ID {createPayment.AppointmentID} does not exist.");

                case -2:
                    return BadRequest(
                        "Payment can only be added for completed appointments.");

                case -5:
                    return Conflict(
                        "A payment already exists for this appointment.");
            }

            int PaymentID = payment.PaymentID;

            PaymentBusiness? newPayment =
                PaymentBusiness.Find(PaymentID);

            return CreatedAtRoute(
                "GetPaymentByID",
                new { PaymentID = PaymentID },
                newPayment?.PDTO);
        }

        [HttpPut("{PaymentID}", Name = "UpdatePayment")]
        [ProducesResponseType(typeof(PaymentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<PaymentDTO> UpdatePayment(int PaymentID, UpdatePaymentDTO updatePaymentDTO)
        {
            if (updatePaymentDTO == null)
                return BadRequest("Invalid Payment data.");

            if (PaymentID < 1)
                return BadRequest($"Not accepted ID {PaymentID}");

            PaymentBusiness? payment =
                PaymentBusiness.Find(PaymentID);

            if (payment == null)
                return NotFound(
                    $"Payment with ID {PaymentID} not found!");

            payment.AppointmentID = updatePaymentDTO.AppointmentID;
            payment.PaymentDate = updatePaymentDTO.PaymentDate;
            payment.PaymentMethod = updatePaymentDTO.PaymentMethod;
            payment.AmountPaid = updatePaymentDTO.AmountPaid;
            payment.AdditionalNotes = updatePaymentDTO.AdditionalNotes;

            int result = payment.Save();

            switch (result)
            {
                case -10:
                    return BadRequest(
                        $"Not accepted ID {PaymentID}");

                case -11:
                    return BadRequest("AppointmentID is required.");

                case -12:
                    return BadRequest(
                        "Amount Paid must be greater than 0.");

                case -13:
                    return BadRequest(
                        "Payment Date is required.");
            }

            return Ok(payment.PDTO);
        }

        [HttpDelete("{PaymentID}", Name = "DeletePayment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult DeletePayment(int PaymentID)
        {
            if (PaymentID < 1)
                return BadRequest($"Not accepted ID {PaymentID}");

            int result =
                PaymentBusiness.DeletePayment(PaymentID);

            switch (result)
            {
                case 1:
                    return Ok(
                        $"Payment with ID {PaymentID} has been deleted.");

                case -1:
                    return NotFound(
                        $"Payment with ID {PaymentID} not found. No rows deleted.");

                default:
                    return StatusCode(
                        500,
                        "An unexpected error occurred while deleting the payment.");
            }
        }
    }
}