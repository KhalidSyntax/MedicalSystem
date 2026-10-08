using MedicalSystem.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MedicalSystem.WinForms.Services
{
    public class PersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress =
                new Uri("https://localhost:7277/");
        }

        public async Task<List<PersonDTO>> GetAllPersonsAsync()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/Persons/All");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<PersonDTO>();

            response.EnsureSuccessStatusCode();

            string json =
                await response.Content.ReadAsStringAsync();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<PersonDTO> persons =
                serializer.Deserialize<List<PersonDTO>>(json);

            return persons;
        }

        public async Task<PersonDTO> GetPersonByIDAsync(int personID)
        {
            HttpResponseMessage response =
                    await _httpClient.GetAsync($"api/Persons/{personID}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            string json =
                await response.Content.ReadAsStringAsync();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            PersonDTO persons =
                serializer.Deserialize<PersonDTO>(json);

            return persons;
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PersonDTO Person)>
            AddPersonAsync(CreatePersonDTO person)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                person.Name,
                DateOfBirth = person.DateOfBirth?.ToString("yyyy-MM-dd"),
                person.Gender,
                person.PhoneNumber,
                person.Email,
                person.Address
            });

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = 
                await _httpClient.PostAsync($"api/Persons", content);

            string responseContent = 
                await response.Content.ReadAsStringAsync();

            if(response.StatusCode == HttpStatusCode.Created)
            {
                PersonDTO newPerson = 
                    new JavaScriptSerializer().Deserialize<PersonDTO>(responseContent);

                return (response.StatusCode, "Person added successfully.", newPerson);
            }

            return (response.StatusCode, responseContent, null);
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PersonDTO Person)>
            UpdatePersonAsync(int personID, UpdatePersonDTO person)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                person.Name,
                DateOfBirth = person.DateOfBirth?.ToString("yyyy-MM-dd"),
                person.Gender,
                person.PhoneNumber,
                person.Email,
                person.Address
            });

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await _httpClient.PutAsync($"api/Persons/{personID}", content);

            string responseContent =
                await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                PersonDTO updatedPerson =
                    new JavaScriptSerializer().Deserialize<PersonDTO>(responseContent);

                return (response.StatusCode, "Person updated successfully.", updatedPerson);
            }

            return (response.StatusCode, responseContent, null);
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeletePersonAsync(int personID)
        {
            HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Persons/{personID}");

            string message =
                await response.Content.ReadAsStringAsync();

            return (response.StatusCode, message);
        }
    }
}