using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class MedicalRecordData
    {
        public static List<MedicalRecordDTO> GetAllMedicalRecords()
        {
            var MedicalRecordsList = new List<MedicalRecordDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_MedicalRecords_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MedicalRecordsList.Add(new MedicalRecordDTO
                            {
                                MedicalRecordID =
                                    reader.GetInt32(
                                        reader.GetOrdinal("MedicalRecordID")),

                                AppointmentID =
                                    reader.GetInt32(
                                        reader.GetOrdinal("AppointmentID")),

                                PatientName =
                                    reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorName =
                                    reader.GetString(reader.GetOrdinal("DoctorName")),

                                VisitDescription =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("VisitDescription"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("VisitDescription")),

                                Diagnosis =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("Diagnosis"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("Diagnosis")),

                                AdditionalNotes =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("AdditionalNotes"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("AdditionalNotes"))
                            });
                        }
                    }
                }
            }
            return MedicalRecordsList;
        }

        public static MedicalRecordDTO? GetMedicalRecordByID(int MedicalRecordID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_MedicalRecords_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MedicalRecordDTO
                            {
                                MedicalRecordID =
                                    reader.GetInt32(
                                        reader.GetOrdinal("MedicalRecordID")),

                                AppointmentID =
                                    reader.GetInt32(
                                        reader.GetOrdinal("AppointmentID")),

                                PatientName =
                                    reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorName =
                                    reader.GetString(reader.GetOrdinal("DoctorName")),

                                VisitDescription =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("VisitDescription"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("VisitDescription")),

                                Diagnosis =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("Diagnosis"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("Diagnosis")),

                                AdditionalNotes =
                                    reader.IsDBNull(
                                        reader.GetOrdinal("AdditionalNotes"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("AdditionalNotes"))
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public static int AddNewMedicalRecord(CreateMedicalRecordDTO createMedicalRecord)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_MedicalRecords_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@AppointmentID",
                        createMedicalRecord.AppointmentID);

                    command.Parameters.AddWithValue(
                        "@VisitDescription",
                        (object?)createMedicalRecord.VisitDescription
                        ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@Diagnosis",
                        (object?)createMedicalRecord.Diagnosis
                        ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@AdditionalNotes",
                        (object?)createMedicalRecord.AdditionalNotes
                        ?? DBNull.Value);

                    var newId = new SqlParameter(
                        "@NewMedicalRecordID",
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
                    {
                        return (int)newId.Value;
                    }

                    return result;
                }
            }
        }

        public static int UpdateMedicalRecord(int MedicalRecordID, UpdateMedicalRecordDTO updateMedicalRecord)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_MedicalRecords_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@MedicalRecordID",
                        MedicalRecordID);

                    command.Parameters.AddWithValue(
                        "@VisitDescription",
                        (object?)updateMedicalRecord.VisitDescription
                        ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@Diagnosis",
                        (object?)updateMedicalRecord.Diagnosis
                        ?? DBNull.Value);

                    command.Parameters.AddWithValue(
                        "@AdditionalNotes",
                        (object?)updateMedicalRecord.AdditionalNotes
                        ?? DBNull.Value);

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

        public static int DeleteMedicalRecord(int MedicalRecordID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_MedicalRecords_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@MedicalRecordID",
                        MedicalRecordID);

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