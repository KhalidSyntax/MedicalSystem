using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class PatientData
    {
        public static List<PatientDTO> GetAllPatients()
        {
            var PatientsList = new List<PatientDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Patients_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientsList.Add(new PatientDTO
                            {
                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),

                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                                ? null
                                : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),

                                Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),

                                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Email")),

                                Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Address"))
                            });
                        }
                    }
                }
            }
            return PatientsList;
        }

        public static PatientDTO? GetPatientByID(int PatientID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Patient_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PatientID", PatientID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PatientDTO
                            {
                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),

                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                                ? null
                                : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),

                                Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),

                                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Email")),

                                Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Address"))
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static int AddNewPatient(CreatePatientDTO createPatient)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Patients_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", createPatient.PersonID);

                    var newId = new SqlParameter("@NewPatientID", SqlDbType.Int)
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

                    if (result != 1)
                    {
                        return result;
                    }

                    return (int)newId.Value;
                }
            }
        }

        public static int DeletePatient(int PatientID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Patients_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PatientID", PatientID);

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
