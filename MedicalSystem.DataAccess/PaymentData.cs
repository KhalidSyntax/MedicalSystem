using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class PaymentData
    {
        public static List<PaymentDTO> GetAllPayments()
        {
            var PaymentsList = new List<PaymentDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Payments_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymentsList.Add(new PaymentDTO
                            {
                                PaymentID = reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                                PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("PaymentMethod")),
                                AmountPaid = reader.GetDecimal(reader.GetOrdinal("AmountPaid")),
                                AdditionalNotes = reader.IsDBNull(reader.GetOrdinal("AdditionalNotes"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("AdditionalNotes"))
                            });
                        }
                    }
                }
            }
            return PaymentsList;
        }

        public static PaymentDTO? GetPaymentByID(int PaymentID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Payments_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PaymentID", PaymentID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PaymentDTO
                            {
                                PaymentID = reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                                PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("PaymentMethod")),
                                AmountPaid = reader.GetDecimal(reader.GetOrdinal("AmountPaid")),
                                AdditionalNotes = reader.IsDBNull(reader.GetOrdinal("AdditionalNotes"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("AdditionalNotes"))
                            };
                        }

                        return null;
                    }
                }
            }
        }

        public static int AddNewPayment(CreatePaymentDTO createPayment)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Payments_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@AppointmentID",
                        createPayment.AppointmentID);

                    command.Parameters.AddWithValue(
                        "@PaymentDate",
                        createPayment.PaymentDate);

                    command.Parameters.AddWithValue(
                        "@PaymentMethod",
                        (object?)createPayment.PaymentMethod ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@AmountPaid",
                        createPayment.AmountPaid);

                    command.Parameters.AddWithValue(
                        "@AdditionalNotes",
                        (object?)createPayment.AdditionalNotes ?? DBNull.Value);

                    var newId = new SqlParameter(
                        "@NewPaymentID",
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(newId);

                    var returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);

                    connection.Open();

                    command.ExecuteNonQuery();

                    int result = (int)returnValue.Value;

                    if (result == 1)
                        return (int)newId.Value;

                    return result;
                }
            }
        }

        public static int UpdatePayment(int PaymentID, UpdatePaymentDTO updatePayment)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Payments_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@PaymentID",
                        PaymentID);

                    command.Parameters.AddWithValue(
                        "@AppointmentID",
                        updatePayment.AppointmentID);

                    command.Parameters.AddWithValue(
                        "@PaymentDate",
                        updatePayment.PaymentDate);

                    command.Parameters.AddWithValue(
                        "@PaymentMethod",
                        (object?)updatePayment.PaymentMethod ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@AmountPaid",
                        updatePayment.AmountPaid);

                    command.Parameters.AddWithValue(
                        "@AdditionalNotes",
                        (object?)updatePayment.AdditionalNotes ?? DBNull.Value);

                    var returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)returnValue.Value;
                }
            }
        }

        public static int DeletePayment(int PaymentID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Payments_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@PaymentID",
                        PaymentID);

                    var returnValue = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };

                    command.Parameters.Add(returnValue);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)returnValue.Value;
                }
            }
        }
    }
}