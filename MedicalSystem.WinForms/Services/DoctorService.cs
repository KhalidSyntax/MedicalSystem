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
    public class DoctorService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public DoctorService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7277/");
            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<DoctorDTO>> GetAllDoctorsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Doctors/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<DoctorDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<DoctorDTO>>(json);
            }
        }

        public async Task<List<AvailablePersonDTO>> GetAvailablePersonsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Doctors/AvailablePersons"))
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

        public async Task<DoctorDTO> GetDoctorByIDAsync(int doctorID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Doctors/{doctorID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<DoctorDTO>(json);
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, DoctorDTO Doctor)>
            AddDoctorAsync(CreateDoctorDTO doctor)
        {
            string json = _serializer.Serialize(doctor);

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync("api/Doctors", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        DoctorDTO newDoctor =
                            _serializer.Deserialize<DoctorDTO>(responseContent);

                        return (response.StatusCode,
                            "Doctor added successfully.", newDoctor);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, DoctorDTO Doctor)>
            UpdateDoctorAsync(int doctorID, UpdateDoctorDTO doctor)
        {
            string json = _serializer.Serialize(doctor);

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/Doctors/{doctorID}", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DoctorDTO updatedDoctor =
                            _serializer.Deserialize<DoctorDTO>(responseContent);

                        return (response.StatusCode,
                            "Doctor updated successfully.", updatedDoctor);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeleteDoctorAsync(int doctorID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Doctors/{doctorID}"))
            {
                string message =
                    await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}