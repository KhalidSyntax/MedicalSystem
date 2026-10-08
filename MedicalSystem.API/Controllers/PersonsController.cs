using System;
using MedicalSystem.DTOs;
using MedicalSystem.Business;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
    [Route("api/Persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPersons")]
        [ProducesResponseType(typeof(IEnumerable<PersonDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPersons()
        {
            List<PersonDTO> PersonList = PersonBusiness.GetAllPersons();
            if (!PersonList.Any())
                return NotFound("There are no Persons");

            return Ok(PersonList);
        }

        [HttpGet("{PersonID}", Name = "GetPersonByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PersonDTO), StatusCodes.Status200OK)]
        public ActionResult<PersonDTO> GetPersonByID(int PersonID)
        {
            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            PersonBusiness? person = PersonBusiness.Find(PersonID);

            if (person == null)
            {
                return NotFound($"Person with ID {PersonID} not found!");
            }

            PersonDTO personDTO = person.PDTO;

            return Ok(personDTO);
        }

        [HttpPost(Name = "AddNewPerson")]
        [ProducesResponseType(typeof(PersonDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult<CreatePersonDTO> AddNewPerson(CreatePersonDTO createPerson)
        {
            if (createPerson == null)
            {
                return BadRequest("Invalid Person data.");
            }

            PersonBusiness person = new PersonBusiness();

            person.Name = createPerson.Name;
            person.DateOfBirth = createPerson.DateOfBirth;
            person.Gender = createPerson.Gender;
            person.PhoneNumber = createPerson.PhoneNumber;
            person.Email = createPerson.Email;
            person.Address = createPerson.Address;

            int result = person.Save();

            switch (result)
            {
                case -10:
                    return BadRequest("Name is required.");

                case -11:
                    return BadRequest("Phone number is required.");

                case -12:
                    return BadRequest("Email format is invalid.");

                case -13:
                    return BadRequest("Date of birth is invalid.");

                case -14:
                    return BadRequest("Gender must be M or F.");

                case -1:
                    return Conflict("Email is already in use by another person.");
            }

            int PersonID = person.PersonID;
            PersonBusiness? newPerson = PersonBusiness.Find(PersonID);

            return CreatedAtRoute("GetPersonByID", new { PersonID = PersonID }, newPerson?.PDTO);
        }

        [HttpPut("{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(UpdatePersonDTO), StatusCodes.Status200OK)]
        public ActionResult<UpdatePersonDTO> UpdatePerson(int PersonID, UpdatePersonDTO updatePersonDTO)
        {
            if (updatePersonDTO == null)
            {
                return BadRequest("Invalid Person data.");
            }

            PersonBusiness? person = PersonBusiness.Find(PersonID);
            if (person == null)
            {
                return NotFound($"Person with ID {PersonID} not found!");
            }

            person.Name = updatePersonDTO.Name;
            person.DateOfBirth = updatePersonDTO.DateOfBirth;
            person.Gender = updatePersonDTO.Gender;
            person.PhoneNumber = updatePersonDTO.PhoneNumber;
            person.Email = updatePersonDTO.Email;
            person.Address = updatePersonDTO.Address;

            int result = person.Save();

            switch (result)
            {
                case -10:
                    return BadRequest("Name is required.");

                case -11:
                    return BadRequest("Phone number is required.");

                case -12:
                    return BadRequest("Email format is invalid.");

                case -13:
                    return BadRequest("Date of birth is invalid.");

                case -14:
                    return BadRequest("Gender must be M or F.");

                case -2:
                    return Conflict("Email is already in use by another person.");
            }
            return Ok(person.PDTO);
        }

        [HttpDelete("{PersonID}", Name = "DeletePerson")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult DeletePerson(int PersonID)
        {
            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            int result = PersonBusiness.DeletePerson(PersonID);
            
            switch(result)
            {
                case -1:
                    return NotFound($"Person with ID {PersonID} not found. No rows deleted.");
                case -2:
                    return Conflict($"Person with ID {PersonID} is a Patient and cannot be deleted.");
                case -3:
                    return Conflict($"Person with ID {PersonID} is a Doctor and cannot be deleted.");
                default:
                    return Ok($"Person with ID {PersonID} has been deleted.");
            }
        }
    }
}