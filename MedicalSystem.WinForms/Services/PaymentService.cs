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
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly JavaScriptSerializer _serializer;

        public PaymentService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7277/");
            _serializer = new JavaScriptSerializer();
        }

        public async Task<List<PaymentDTO>> GetAllPaymentsAsync()
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync("api/Payments/All"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<PaymentDTO>();

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<List<PaymentDTO>>(json);
            }
        }

        public async Task<PaymentDTO> GetPaymentByIDAsync(int paymentID)
        {
            using (HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Payments/{paymentID}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<PaymentDTO>(json);
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PaymentDTO Payment)>
            AddPaymentAsync(CreatePaymentDTO payment)
        {
            string json = _serializer.Serialize(new
            {
                payment.AppointmentID,
                PaymentDate = payment.PaymentDate.ToString("yyyy-MM-dd"),
                payment.PaymentMethod,
                payment.AmountPaid,
                payment.AdditionalNotes
            });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PostAsync("api/Payments", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        PaymentDTO newPayment =
                            _serializer.Deserialize<PaymentDTO>(responseContent);

                        return (response.StatusCode,
                            "Payment added successfully.", newPayment);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message, PaymentDTO Payment)>
            UpdatePaymentAsync(int paymentID, UpdatePaymentDTO payment)
        {
            string json = _serializer.Serialize(new
            {
                payment.AppointmentID,
                PaymentDate = payment.PaymentDate.ToString("yyyy-MM-dd"),
                payment.PaymentMethod,
                payment.AmountPaid,
                payment.AdditionalNotes
            });

            using (StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/Payments/{paymentID}", content))
                {
                    string responseContent =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        PaymentDTO updatedPayment =
                            _serializer.Deserialize<PaymentDTO>(responseContent);

                        return (response.StatusCode,
                            "Payment updated successfully.", updatedPayment);
                    }

                    return (response.StatusCode, responseContent, null);
                }
            }
        }

        public async Task<(HttpStatusCode StatusCode, string Message)> DeletePaymentAsync(int paymentID)
        {
            using (HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Payments/{paymentID}"))
            {
                string message =
                    await response.Content.ReadAsStringAsync();

                return (response.StatusCode, message);
            }
        }
    }
}