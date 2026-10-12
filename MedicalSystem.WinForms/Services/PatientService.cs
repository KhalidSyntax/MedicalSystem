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
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public PatientService()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress =
                new Uri("https://localhost:7277/");

            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<PatientDTO>> GetAllPatientsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Patients/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<PatientDTO>();

                response.EnsureSuccessStatusCode();

                string json =
                    await response.Content.ReadAsStringAsync();

                List<PatientDTO> patients =
                    _serializer.Deserialize<List<PatientDTO>>(json);

                return patients;
            }
        }

        public async Task<List<PatientLookupDTO>> GetPatientsLookupAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Patients/Lookup"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<PatientLookupDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<PatientLookupDTO>>(json);
            }
        }

        public async Task<List<AvailablePersonDTO>> GetAvailablePersonsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Patients/AvailablePersons"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<AvailablePersonDTO>();

                response.EnsureSuccessStatusCode();

                string json =
                    await response.Content.ReadAsStringAsync();

                List<AvailablePersonDTO> availablePersons =
                    _serializer.Deserialize<List<AvailablePersonDTO>>(json);

                return availablePersons;
            }
        }

        public async Task<PatientDTO> GetPatientByIDAsync(int patientID)
        {
            using (HttpResponseMessage response =
                    await _httpClient.GetAsync($"api/Patients/{patientID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json =
                    await response.Content.ReadAsStringAsync();

                PatientDTO patient =
                    _serializer.Deserialize<PatientDTO>(json);

                return patient;
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PatientDTO patient)>
            AddPatientAsync(int personID)
        {
            CreatePatientDTO createPatient = new CreatePatientDTO
            {
                PersonID = personID
            };

            string json =
                _serializer.Serialize(createPatient); 

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync($"api/Patients", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        PatientDTO newPatient =
                            _serializer.Deserialize<PatientDTO>(responseContent);

                        return (response.StatusCode, "Patient added successfully.", newPatient);
                    }
                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeletePatientAsync(int patientID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Patients/{patientID}"))
            {
                string message =
                    await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}