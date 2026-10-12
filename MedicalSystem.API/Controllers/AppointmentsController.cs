using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllAppointments")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAllAppointments()
        {
            List<AppointmentDTO> AppointmentList =
                AppointmentBusiness.GetAllAppointments();

            if (!AppointmentList.Any())
                return NotFound("There are no Appointments");

            return Ok(AppointmentList);
        }

        [HttpGet("{AppointmentID}", Name = "GetAppointmentByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        public ActionResult<AppointmentDTO> GetAppointmentByID(int AppointmentID)
        {
            if (AppointmentID < 1)
            {
                return BadRequest($"Not accepted ID {AppointmentID}");
            }

            AppointmentBusiness? appointment =
                AppointmentBusiness.Find(AppointmentID);

            if (appointment == null)
            {
                return NotFound(
                    $"Appointment with ID {AppointmentID} not found!");
            }

            return Ok(appointment.ADTO);
        }

        [HttpGet("Doctor/{DoctorID}", Name = "GetAppointmentsByDoctor")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointmentsByDoctor(int DoctorID)
        {
            if (DoctorID < 1)
            {
                return BadRequest($"Not accepted ID {DoctorID}");
            }

            List<AppointmentDTO> AppointmentList =
                AppointmentBusiness.GetAppointmentsByDoctor(DoctorID);

            if (!AppointmentList.Any())
            {
                return NotFound(
                    $"There are no Appointments for Doctor ID {DoctorID}");
            }

            return Ok(AppointmentList);
        }

        [HttpGet("Patient/{PatientID}", Name = "GetAppointmentsByPatient")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointmentsByPatient(int PatientID)
        {
            if (PatientID < 1)
            {
                return BadRequest($"Not accepted ID {PatientID}");
            }

            List<AppointmentDTO> AppointmentList =
                AppointmentBusiness.GetAppointmentsByPatient(PatientID);

            if (!AppointmentList.Any())
            {
                return NotFound(
                    $"There are no Appointments for Patient ID {PatientID}");
            }

            return Ok(AppointmentList);
        }

        [HttpPost(Name = "AddNewAppointment")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<AppointmentDTO> AddNewAppointment(CreateAppointmentDTO createAppointment)
        {
            if (createAppointment == null)
            {
                return BadRequest("Invalid Appointment data.");
            }

            AppointmentBusiness appointment =
                new AppointmentBusiness();

            appointment.PatientID = createAppointment.PatientID;
            appointment.DoctorID = createAppointment.DoctorID;
            appointment.AppointmentDateTime = createAppointment.AppointmentDateTime;
            appointment.AppointmentStatus = createAppointment.AppointmentStatus;

            int result = appointment.Save();

            switch (result)
            {
                case -10:
                    return BadRequest(
                        "Appointment date and time cannot be in the past.");

                case -11:
                    return BadRequest(
                        "Appointment status must be between 1 and 6.");

                case -1:
                    return NotFound(
                        $"Patient with ID {createAppointment.PatientID} does not exist.");

                case -2:
                    return NotFound(
                        $"Doctor with ID {createAppointment.DoctorID} does not exist.");

                case -3:
                    return Conflict(
                        "Doctor already has another appointment at the same date and time.");

                case -4:
                    return BadRequest(
                        "Appointment status must be between 1 and 6.");
            }

            int AppointmentID = appointment.AppointmentID;

            AppointmentBusiness? newAppointment =
                AppointmentBusiness.Find(AppointmentID);

            return CreatedAtRoute(
                "GetAppointmentByID",
                new { AppointmentID = AppointmentID },
                newAppointment?.ADTO);
        }

        [HttpPut("{AppointmentID}", Name = "UpdateAppointment")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<AppointmentDTO> UpdateAppointment(int AppointmentID, UpdateAppointmentDTO updateAppointmentDTO)
        {
            if (updateAppointmentDTO == null)
            {
                return BadRequest("Invalid Appointment data.");
            }

            if (AppointmentID < 1)
            {
                return BadRequest(
                    $"Not accepted ID {AppointmentID}");
            }

            AppointmentBusiness? appointment =
                AppointmentBusiness.Find(AppointmentID);

            if (appointment == null)
            {
                return NotFound(
                    $"Appointment with ID {AppointmentID} not found!");
            }

            appointment.PatientID =
                updateAppointmentDTO.PatientID;

            appointment.DoctorID =
                updateAppointmentDTO.DoctorID;

            appointment.AppointmentDateTime =
                updateAppointmentDTO.AppointmentDateTime;

            appointment.AppointmentStatus =
                updateAppointmentDTO.AppointmentStatus;

            int result = appointment.Save();

            switch (result)
            {
                case -10:
                    return BadRequest(
                        "Appointment date and time cannot be in the past.");

                case -11:
                    return BadRequest(
                        "Appointment status must be between 1 and 6.");

                case -1:
                    return NotFound(
                        $"Appointment with ID {AppointmentID} not found.");

                case -2:
                    return BadRequest(
                        "Appointment status must be between 1 and 6.");

                case -3:
                    return BadRequest(
                        "Appointment cannot be changed from Canceled to Completed.");

                case -4:
                    return Conflict(
                        "Doctor already has another appointment at the same date and time.");
            }

            return Ok(appointment.ADTO);
        }

        [HttpDelete("{AppointmentID}", Name = "DeleteAppointment")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult DeleteAppointment(int AppointmentID)
        {
            if (AppointmentID < 1)
            {
                return BadRequest(
                    $"Not accepted ID {AppointmentID}");
            }

            int result =
                AppointmentBusiness.DeleteAppointment(AppointmentID);

            switch (result)
            {
                case 1:
                    return Ok(
                        $"Appointment with ID {AppointmentID} has been deleted.");

                case -1:
                    return NotFound(
                        $"Appointment with ID {AppointmentID} not found. No rows deleted.");

                case -2:
                    return Conflict(
                        $"Appointment with ID {AppointmentID} is linked to a Medical Record and cannot be deleted.");

                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}