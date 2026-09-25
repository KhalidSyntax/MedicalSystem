using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllDoctors")]
        [ProducesResponseType(typeof(DoctorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DoctorDTO>> GetAllDoctors()
        {
            List<DoctorDTO> DoctorList = DoctorBusiness.GetAllDoctors();

            if (!DoctorList.Any())
                return NotFound("There are no Doctors");

            return Ok(DoctorList);
        }

        [HttpGet("{DoctorID}", Name = "GetDoctorByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DoctorDTO), StatusCodes.Status200OK)]
        public ActionResult<DoctorDTO> GetDoctorByID(int DoctorID)
        {
            if (DoctorID < 1)
            {
                return BadRequest($"Not accepted ID {DoctorID}");
            }

            DoctorBusiness? doctor = DoctorBusiness.Find(DoctorID);

            if (doctor == null)
            {
                return NotFound($"Doctor with ID {DoctorID} not found!");
            }

            return Ok(doctor.DDTO);
        }

        [HttpPost(Name = "AddNewDoctor")]
        [ProducesResponseType(typeof(DoctorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<CreateDoctorDTO> AddNewDoctor(CreateDoctorDTO createDoctor)
        {
            if (createDoctor == null)
            {
                return BadRequest("Invalid Doctor data.");
            }

            DoctorBusiness doctor = new DoctorBusiness();

            doctor.PersonID = createDoctor.PersonID;
            doctor.Specialization = createDoctor.Specialization;

            int result = doctor.Save();

            switch (result)
            {
                case -10:
                    return BadRequest("Specialization is required.");

                case -1:
                    return Conflict($"Person with ID {createDoctor.PersonID} does not exist.");

                case -2:
                    return Conflict($"Person with ID {createDoctor.PersonID} is already a Doctor.");
            }

            int DoctorID = doctor.DoctorID;

            DoctorBusiness? newDoctor = DoctorBusiness.Find(DoctorID);

            return CreatedAtRoute(
                "GetDoctorByID",
                new { DoctorID = DoctorID },
                newDoctor?.DDTO);
        }

        [HttpPut("{DoctorID}", Name = "UpdateDoctor")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UpdateDoctorDTO), StatusCodes.Status200OK)]
        public ActionResult<UpdateDoctorDTO> UpdateDoctor(int DoctorID, UpdateDoctorDTO updateDoctorDTO)
        {
            if (updateDoctorDTO == null)
            {
                return BadRequest("Invalid Doctor data.");
            }

            if (DoctorID < 1)
            {
                return BadRequest($"Not accepted ID {DoctorID}");
            }

            DoctorBusiness? doctor = DoctorBusiness.Find(DoctorID);

            if (doctor == null)
            {
                return NotFound($"Doctor with ID {DoctorID} not found!");
            }

            doctor.Specialization = updateDoctorDTO.Specialization;

            int result = doctor.Save();

            switch (result)
            {
                case -10:
                    return BadRequest("Specialization is required.");

                case -1:
                    return NotFound($"Doctor with ID {DoctorID} not found.");
            }

            return Ok(doctor.DDTO);
        }

        [HttpDelete("{DoctorID}", Name = "DeleteDoctor")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult DeleteDoctor(int DoctorID)
        {
            if (DoctorID < 1)
            {
                return BadRequest($"Not accepted ID {DoctorID}");
            }

            int result = DoctorBusiness.DeleteDoctor(DoctorID);

            switch (result)
            {
                case -1:
                    return NotFound(
                        $"Doctor with ID {DoctorID} not found. No rows deleted.");

                case -2:
                    return Conflict(
                        $"Doctor with ID {DoctorID} has appointments and cannot be deleted.");

                default:
                    return Ok(
                        $"Doctor with ID {DoctorID} has been deleted.");
            }
        }
    }
}