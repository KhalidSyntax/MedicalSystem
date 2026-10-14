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
    public class PrescriptionService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public PrescriptionService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7277/");
            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<PrescriptionDTO>> GetAllPrescriptionsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Prescriptions/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<PrescriptionDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<PrescriptionDTO>>(json);
            }
        }

        public async Task<PrescriptionDTO> GetPrescriptionByIDAsync(int prescriptionID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Prescriptions/{prescriptionID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<PrescriptionDTO>(json);
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PrescriptionDTO Prescription)>
            AddPrescriptionAsync(CreatePrescriptionDTO prescription)
        {
            string json = _serializer.Serialize(new
            {
                prescription.MedicalRecordID,
                prescription.MedicationName,
                prescription.Dosage,
                prescription.Frequency,
                StartDate = prescription.StartDate.ToString("yyyy-MM-dd"),
                EndDate = prescription.EndDate.ToString("yyyy-MM-dd"),
                prescription.SpecialInstructions
            });

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync("api/Prescriptions", content))
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        PrescriptionDTO newPrescription =
                            _serializer.Deserialize<PrescriptionDTO>(responseContent);

                        return (response.StatusCode,
                            "Prescription added successfully.", newPrescription);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PrescriptionDTO Prescription)>
            UpdatePrescriptionAsync(int prescriptionID, UpdatePrescriptionDTO prescription)
        {
            string json = _serializer.Serialize(new
            {
                prescription.MedicationName,
                prescription.Dosage,
                prescription.Frequency,
                StartDate = prescription.StartDate.ToString("yyyy-MM-dd"),
                EndDate = prescription.EndDate.ToString("yyyy-MM-dd"),
                prescription.SpecialInstructions
            });

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/Prescriptions/{prescriptionID}", content))
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        PrescriptionDTO updatedPrescription =
                            _serializer.Deserialize<PrescriptionDTO>(responseContent);

                        return (response.StatusCode,
                            "Prescription updated successfully.", updatedPrescription);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeletePrescriptionAsync(int prescriptionID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Prescriptions/{prescriptionID}"))
            {
                string message = await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}