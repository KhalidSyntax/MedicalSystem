using MedicalSystem.Business;
using MedicalSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPrescriptions")]
        [ProducesResponseType(typeof(IEnumerable<PrescriptionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PrescriptionDTO>> GetAllPrescriptions()
        {
            List<PrescriptionDTO> PrescriptionList =
                PrescriptionBusiness.GetAllPrescriptions();

            if (!PrescriptionList.Any())
                return NotFound("There are no Prescriptions");

            return Ok(PrescriptionList);
        }

        [HttpGet("{PrescriptionID}", Name = "GetPrescriptionByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PrescriptionDTO), StatusCodes.Status200OK)]
        public ActionResult<PrescriptionDTO> GetPrescriptionByID(int PrescriptionID)
        {
            if (PrescriptionID < 1)
            {
                return BadRequest($"Not accepted ID {PrescriptionID}");
            }

            PrescriptionBusiness? prescription =
                PrescriptionBusiness.Find(PrescriptionID);

            if (prescription == null)
            {
                return NotFound(
                    $"Prescription with ID {PrescriptionID} not found!");
            }

            return Ok(prescription.PDTO);
        }

        [HttpPost(Name = "AddNewPrescription")]
        [ProducesResponseType(typeof(PrescriptionDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<PrescriptionDTO> AddNewPrescription(CreatePrescriptionDTO createPrescription)
        {
            if (createPrescription == null)
            {
                return BadRequest("Invalid Prescription data.");
            }

            PrescriptionBusiness prescription =
                new PrescriptionBusiness();

            prescription.MedicalRecordID =
                createPrescription.MedicalRecordID;

            prescription.MedicationName =
                createPrescription.MedicationName;

            prescription.Dosage =
                createPrescription.Dosage;

            prescription.Frequency =
                createPrescription.Frequency;

            prescription.StartDate =
                createPrescription.StartDate;

            prescription.EndDate =
                createPrescription.EndDate;

            prescription.SpecialInstructions =
                createPrescription.SpecialInstructions;

            int result = prescription.Save();

            switch (result)
            {
                case -11:
                    return BadRequest(
                        "MedicalRecordID must be a valid ID.");

                case -12:
                    return BadRequest(
                        "MedicationName is required.");

                case -13:
                    return BadRequest(
                        "Dosage is required.");

                case -14:
                    return BadRequest(
                        "Frequency is required.");

                case -15:
                    return BadRequest(
                        "StartDate is required.");

                case -16:
                    return BadRequest(
                        "EndDate is required.");

                case -17:
                    return BadRequest(
                        "EndDate cannot be before StartDate.");

                case -1:
                    return NotFound(
                        $"Medical Record with ID {createPrescription.MedicalRecordID} does not exist.");
            }

            int PrescriptionID = prescription.PrescriptionID;

            PrescriptionBusiness? newPrescription =
                PrescriptionBusiness.Find(PrescriptionID);

            return CreatedAtRoute(
                "GetPrescriptionByID",
                new { PrescriptionID = PrescriptionID },
                newPrescription?.PDTO);
        }

        [HttpPut("{PrescriptionID}", Name = "UpdatePrescription")]
        [ProducesResponseType(typeof(PrescriptionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<PrescriptionDTO> UpdatePrescription(int PrescriptionID, UpdatePrescriptionDTO updatePrescriptionDTO)
        {
            if (updatePrescriptionDTO == null)
            {
                return BadRequest("Invalid Prescription data.");
            }

            if (PrescriptionID < 1)
            {
                return BadRequest(
                    $"Not accepted ID {PrescriptionID}");
            }

            PrescriptionBusiness? prescription =
                PrescriptionBusiness.Find(PrescriptionID);

            if (prescription == null)
            {
                return NotFound(
                    $"Prescription with ID {PrescriptionID} not found!");
            }

            prescription.MedicationName =
                updatePrescriptionDTO.MedicationName;

            prescription.Dosage =
                updatePrescriptionDTO.Dosage;

            prescription.Frequency =
                updatePrescriptionDTO.Frequency;

            prescription.StartDate =
                updatePrescriptionDTO.StartDate;

            prescription.EndDate =
                updatePrescriptionDTO.EndDate;

            prescription.SpecialInstructions =
                updatePrescriptionDTO.SpecialInstructions;

            int result = prescription.Save();

            switch (result)
            {
                case -10:
                    return BadRequest(
                        "Invalid PrescriptionID.");

                case -12:
                    return BadRequest(
                        "MedicationName is required.");

                case -13:
                    return BadRequest(
                        "Dosage is required.");

                case -14:
                    return BadRequest(
                        "Frequency is required.");

                case -15:
                    return BadRequest(
                        "StartDate is required.");

                case -16:
                    return BadRequest(
                        "EndDate is required.");

                case -17:
                    return BadRequest(
                        "EndDate cannot be before StartDate.");

                case -1:
                    return NotFound(
                        $"Prescription with ID {PrescriptionID} not found.");
            }

            return Ok(prescription.PDTO);
        }

        [HttpDelete("{PrescriptionID}", Name = "DeletePrescription")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult DeletePrescription(int PrescriptionID)
        {
            if (PrescriptionID < 1)
            {
                return BadRequest(
                    $"Not accepted ID {PrescriptionID}");
            }

            int result =
                PrescriptionBusiness.DeletePrescription(PrescriptionID);

            switch (result)
            {
                case -1:
                    return NotFound(
                        $"Prescription with ID {PrescriptionID} not found. No rows deleted.");

                default:
                    return Ok(
                        $"Prescription with ID {PrescriptionID} has been deleted.");
            }
        }
    }
}