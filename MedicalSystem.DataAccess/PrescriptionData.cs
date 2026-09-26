using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class PrescriptionData
    {
        public static List<PrescriptionDTO> GetAllPrescriptions()
        {
            var PrescriptionsList = new List<PrescriptionDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Prescriptions_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrescriptionsList.Add(new PrescriptionDTO
                            {
                                PrescriptionID = reader.GetInt32(reader.GetOrdinal("PrescriptionID")),
                                MedicalRecordID = reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                                MedicationName = reader.GetString(reader.GetOrdinal("MedicationName")),
                                Dosage = reader.GetString(reader.GetOrdinal("Dosage")),
                                Frequency = reader.GetString(reader.GetOrdinal("Frequency")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                SpecialInstructions = reader.IsDBNull(reader.GetOrdinal("SpecialInstructions"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("SpecialInstructions"))
                            });
                        }
                    }
                }
            }
            return PrescriptionsList;
        }

        public static PrescriptionDTO? GetPrescriptionByID(int PrescriptionID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Prescriptions_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PrescriptionDTO
                            {
                                PrescriptionID = reader.GetInt32(reader.GetOrdinal("PrescriptionID")),
                                MedicalRecordID = reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                                MedicationName = reader.GetString(reader.GetOrdinal("MedicationName")),
                                Dosage = reader.GetString(reader.GetOrdinal("Dosage")),
                                Frequency = reader.GetString(reader.GetOrdinal("Frequency")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                SpecialInstructions = reader.IsDBNull(reader.GetOrdinal("SpecialInstructions"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("SpecialInstructions"))
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddNewPrescription(CreatePrescriptionDTO createPrescription)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Prescriptions_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MedicalRecordID", createPrescription.MedicalRecordID);
                    command.Parameters.AddWithValue("@MedicationName", createPrescription.MedicationName);
                    command.Parameters.AddWithValue("@Dosage", createPrescription.Dosage);
                    command.Parameters.AddWithValue("@Frequency", createPrescription.Frequency);
                    command.Parameters.AddWithValue("@StartDate", createPrescription.StartDate);
                    command.Parameters.AddWithValue("@EndDate", createPrescription.EndDate);
                    command.Parameters.AddWithValue("@SpecialInstructions",
                        (object?)createPrescription.SpecialInstructions ?? DBNull.Value);

                    var newId = new SqlParameter("@NewPrescriptionID", SqlDbType.Int)
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

        public static int UpdatePrescription(int PrescriptionID, UpdatePrescriptionDTO updatePrescription)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Prescriptions_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                    command.Parameters.AddWithValue("@MedicationName", updatePrescription.MedicationName);
                    command.Parameters.AddWithValue("@Dosage", updatePrescription.Dosage);
                    command.Parameters.AddWithValue("@Frequency", updatePrescription.Frequency);
                    command.Parameters.AddWithValue("@StartDate", updatePrescription.StartDate);
                    command.Parameters.AddWithValue("@EndDate", updatePrescription.EndDate);
                    command.Parameters.AddWithValue("@SpecialInstructions",
                        (object?)updatePrescription.SpecialInstructions ?? DBNull.Value);

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

        public static int DeletePrescription(int PrescriptionID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Prescriptions_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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