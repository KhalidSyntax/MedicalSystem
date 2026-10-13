using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/MedicalRecords")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllMedicalRecords")]
        [ProducesResponseType(typeof(IEnumerable<MedicalRecordDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MedicalRecordDTO>> GetAllMedicalRecords()
        {
            List<MedicalRecordDTO> MedicalRecordList =
                MedicalRecordBusiness.GetAllMedicalRecords();

            if (!MedicalRecordList.Any())
                return NotFound("There are no Medical Records");

            return Ok(MedicalRecordList);
        }

        [HttpGet("{MedicalRecordID}", Name = "GetMedicalRecordByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MedicalRecordDTO), StatusCodes.Status200OK)]
        public ActionResult<MedicalRecordDTO> GetMedicalRecordByID(int MedicalRecordID)
        {
            if (MedicalRecordID < 1)
                return BadRequest($"Not accepted ID {MedicalRecordID}");

            MedicalRecordBusiness? medicalRecord =
                MedicalRecordBusiness.Find(MedicalRecordID);

            if (medicalRecord == null)
                return NotFound($"Medical Record with ID {MedicalRecordID} not found!");

            return Ok(medicalRecord.MRDTO);
        }

        [HttpPost(Name = "AddNewMedicalRecord")]
        [ProducesResponseType(typeof(MedicalRecordDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<MedicalRecordDTO> AddNewMedicalRecord(CreateMedicalRecordDTO createMedicalRecord)
        {
            if (createMedicalRecord == null)
                return BadRequest("Invalid Medical Record data.");

            MedicalRecordBusiness medicalRecord =
                new MedicalRecordBusiness();

            medicalRecord.AppointmentID = createMedicalRecord.AppointmentID;
            medicalRecord.VisitDescription = createMedicalRecord.VisitDescription;
            medicalRecord.Diagnosis = createMedicalRecord.Diagnosis;
            medicalRecord.AdditionalNotes = createMedicalRecord.AdditionalNotes;

            int result = medicalRecord.Save();

            switch (result)
            {
                case -10:
                    return BadRequest($"Not accepted Appointment ID {createMedicalRecord.AppointmentID}");

                case -1:
                    return NotFound($"Appointment with ID {createMedicalRecord.AppointmentID} does not exist.");

                case -2:
                    return BadRequest("Medical Record can only be added for a Completed Appointment.");

                case -3:
                    return Conflict("A Medical Record already exists for this Appointment.");
            }

            int MedicalRecordID = medicalRecord.MedicalRecordID;

            MedicalRecordBusiness? newMedicalRecord =
                MedicalRecordBusiness.Find(MedicalRecordID);

            return CreatedAtRoute(
                "GetMedicalRecordByID",
                new { MedicalRecordID = MedicalRecordID },
                newMedicalRecord?.MRDTO);
        }

        [HttpPut("{MedicalRecordID}", Name = "UpdateMedicalRecord")]
        [ProducesResponseType(typeof(MedicalRecordDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<MedicalRecordDTO> UpdateMedicalRecord(int MedicalRecordID, UpdateMedicalRecordDTO updateMedicalRecordDTO)
        {
            if (updateMedicalRecordDTO == null)
                return BadRequest("Invalid Medical Record data.");

            if (MedicalRecordID < 1)
                return BadRequest($"Not accepted ID {MedicalRecordID}");

            MedicalRecordBusiness? medicalRecord =
                MedicalRecordBusiness.Find(MedicalRecordID);

            if (medicalRecord == null)
                return NotFound($"Medical Record with ID {MedicalRecordID} not found!");

            medicalRecord.VisitDescription = updateMedicalRecordDTO.VisitDescription;
            medicalRecord.Diagnosis = updateMedicalRecordDTO.Diagnosis;
            medicalRecord.AdditionalNotes = updateMedicalRecordDTO.AdditionalNotes;

            int result = medicalRecord.Save();

            switch (result)
            {
                case -1:
                    return NotFound($"Medical Record with ID {MedicalRecordID} not found.");
            }

            return Ok(medicalRecord.MRDTO);
        }

        [HttpDelete("{MedicalRecordID}", Name = "DeleteMedicalRecord")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult DeleteMedicalRecord(int MedicalRecordID)
        {
            if (MedicalRecordID < 1)
                return BadRequest($"Not accepted ID {MedicalRecordID}");

            int result =
                MedicalRecordBusiness.DeleteMedicalRecord(MedicalRecordID);

            switch (result)
            {
                case 1:
                    return Ok(
                        $"Medical Record with ID {MedicalRecordID} has been deleted.");

                case -1:
                    return NotFound(
                        $"Medical Record with ID {MedicalRecordID} not found. No rows deleted.");

                case -2:
                    return Conflict(
                        $"Medical Record with ID {MedicalRecordID} cannot be deleted because it has associated prescriptions.");

                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}