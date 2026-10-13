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
    public class MedicalRecordService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public MedicalRecordService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7277/");
            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<MedicalRecordDTO>> GetAllMedicalRecordsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/MedicalRecords/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<MedicalRecordDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<MedicalRecordDTO>>(json);
            }
        }

        public async Task<MedicalRecordDTO> GetMedicalRecordByIDAsync(int medicalRecordID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/MedicalRecords/{medicalRecordID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<MedicalRecordDTO>(json);
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, MedicalRecordDTO MedicalRecord)>
            AddMedicalRecordAsync(CreateMedicalRecordDTO medicalRecord)
        {
            string json = _serializer.Serialize(medicalRecord);

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync("api/MedicalRecords", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        MedicalRecordDTO newMedicalRecord =
                            _serializer.Deserialize<MedicalRecordDTO>(responseContent);

                        return (response.StatusCode,
                            "Medical Record added successfully.", newMedicalRecord);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, MedicalRecordDTO MedicalRecord)>
            UpdateMedicalRecordAsync(int medicalRecordID, UpdateMedicalRecordDTO medicalRecord)
        {
            string json = _serializer.Serialize(medicalRecord);

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/MedicalRecords/{medicalRecordID}", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MedicalRecordDTO updatedMedicalRecord =
                            _serializer.Deserialize<MedicalRecordDTO>(responseContent);

                        return (response.StatusCode,
                            "Medical Record updated successfully.", updatedMedicalRecord);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeleteMedicalRecordAsync(int medicalRecordID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/MedicalRecords/{medicalRecordID}"))
            {
                string message =
                    await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}