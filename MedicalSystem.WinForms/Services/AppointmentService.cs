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
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public AppointmentService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7277/");
            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Appointments/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<AppointmentDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<AppointmentDTO>>(json);
            }
        }

        public async Task<AppointmentDTO> GetAppointmentByIDAsync(int appointmentID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Appointments/{appointmentID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<AppointmentDTO>(json);
            }
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsByDoctorAsync(int doctorID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Appointments/Doctor/{doctorID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<AppointmentDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<AppointmentDTO>>(json);
            }
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsByPatientAsync(int patientID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Appointments/Patient/{patientID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<AppointmentDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<AppointmentDTO>>(json);
            }
        }

        public async Task<List<AppointmentLookupDTO>> GetAvailableForMedicalRecordAsync()
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(
                "api/Appointments/AvailableForMedicalRecord"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<AppointmentLookupDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<AppointmentLookupDTO>>(json);
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, AppointmentDTO Appointment)>
            AddAppointmentAsync(CreateAppointmentDTO appointment)
        {
            string json = _serializer.Serialize(new
            {
                appointment.PatientID,
                appointment.DoctorID,
                AppointmentDateTime = appointment.AppointmentDateTime
                .ToString("yyyy-MM-ddTHH:mm:ss"),
                appointment.AppointmentStatus
            });

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync("api/Appointments", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        AppointmentDTO newAppointment =
                            _serializer.Deserialize<AppointmentDTO>(responseContent);

                        return (response.StatusCode,
                            "Appointment added successfully.", newAppointment);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, AppointmentDTO Appointment)>
            UpdateAppointmentAsync(int appointmentID, UpdateAppointmentDTO appointment)
        {
            string json = _serializer.Serialize(new
            {
                appointment.PatientID,
                appointment.DoctorID,
                AppointmentDateTime = appointment.AppointmentDateTime
                .ToString("yyyy-MM-ddTHH:mm:ss"),
                appointment.AppointmentStatus
            });

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/Appointments/{appointmentID}", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        AppointmentDTO updatedAppointment =
                            _serializer.Deserialize<AppointmentDTO>(responseContent);

                        return (response.StatusCode,
                            "Appointment updated successfully.", updatedAppointment);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeleteAppointmentAsync(int appointmentID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Appointments/{appointmentID}"))
            {
                string message =
                    await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}