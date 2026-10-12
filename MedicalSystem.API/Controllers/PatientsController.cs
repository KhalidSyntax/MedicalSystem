using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPatients")]
        [ProducesResponseType(typeof(IEnumerable<PatientDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PatientDTO>> GetAllPatients()
        {
            List<PatientDTO> PatientList = PatientBusiness.GetAllPatients();
            if (!PatientList.Any())
                return NotFound("There are no patients");

            return Ok(PatientList);
        }

        [HttpGet("Lookup", Name = "GetPatientsLookup")]
        [ProducesResponseType(typeof(IEnumerable<PatientLookupDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PatientLookupDTO>> GetPatientsLookup()
        {
            List<PatientLookupDTO> PatientList = PatientBusiness.GetPatientsLookup();

            if (!PatientList.Any())
                return NotFound("There are no Patients");

            return Ok(PatientList);
        }

        [HttpGet("AvailablePersons", Name = "GetAvailablePersons")]
        [ProducesResponseType(typeof(IEnumerable<AvailablePersonDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AvailablePersonDTO>> GetAvailablePersons()
        {
            List<AvailablePersonDTO> availablePerson = PatientBusiness.GetAvailablePersons();
            if (!availablePerson.Any())
                return NotFound("There are no available persons to add as patients");

            return Ok(availablePerson);
        }

        [HttpGet("{PatientID}", Name = "GetPatientByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        public ActionResult<PatientDTO> GetPatientByID(int PatientID)
        {
            if (PatientID < 1)
            {
                return BadRequest($"Not accepted ID {PatientID}");
            }

            PatientBusiness? patient = PatientBusiness.Find(PatientID);

            if (patient == null)
            {
                return NotFound($"Patient with ID {PatientID} not found!");
            }

            PatientDTO patientDTO = patient.PDTO;

            return Ok(patientDTO);
        }

        [HttpPost(Name = "AddNewPatient")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<CreatePatientDTO> AddNewPatient(CreatePatientDTO createPatient)
        {
            if (createPatient == null)
            {
                return BadRequest("Invalid Patient data.");
            }

            PatientBusiness newPatient = new PatientBusiness();

            newPatient.PersonID = createPatient.PersonID;

            int result = newPatient.AddNewPatient();

            switch (result)
            {
                case -1:
                    return BadRequest("Person does not exist.");

                case -2:
                    return Conflict("Person is already a patient.");
            }

            int PatientID = newPatient.PatientID;
            PatientBusiness? patient = PatientBusiness.Find(PatientID);

            return CreatedAtRoute("GetPatientByID", new { PatientID = PatientID }, patient?.PDTO);
        }

        [HttpDelete("{PatientID}", Name = "DeletePatient")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult DeletePatient(int PatientID)
        {
            if (PatientID < 1)
            {
                return BadRequest($"Not accepted ID {PatientID}");
            }

            int result = PatientBusiness.DeletePatient(PatientID);

            switch (result)
            {
                case 1:
                    return Ok("Patient deleted successfully.");

                case -1:
                    return NotFound("Patient does not exist.");

                case -2:
                    return Conflict("Patient has appointments and cannot be deleted.");

                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
