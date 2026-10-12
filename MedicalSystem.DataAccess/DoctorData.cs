using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class DoctorData
    {
        public static List<DoctorDTO> GetAllDoctors()
        {
            var DoctorsList = new List<DoctorDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DoctorsList.Add(new DoctorDTO
                            {
                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
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
                                : reader.GetString(reader.GetOrdinal("Address")),

                                Specialization = reader.GetString(reader.GetOrdinal("Specialization"))
                            });
                        }
                    }
                }
            }

            return DoctorsList;
        }

        public static List<AvailablePersonDTO> GetAvailablePersons()
        {
            var availablePersons = new List<AvailablePersonDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_GetAvailablePersons", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availablePersons.Add(new AvailablePersonDTO
                            {
                                PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            });
                        }
                    }
                }
            }
            return availablePersons;
        }

        public static DoctorDTO? GetDoctorByID(int DoctorID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DoctorDTO
                            {
                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
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
                                : reader.GetString(reader.GetOrdinal("Address")),

                                Specialization = reader.GetString(reader.GetOrdinal("Specialization"))
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

        public static int AddNewDoctor(CreateDoctorDTO createDoctor)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", createDoctor.PersonID);
                    command.Parameters.AddWithValue("@Specialization", createDoctor.Specialization);

                    var newId = new SqlParameter("@NewDoctorID", SqlDbType.Int)
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

        public static int UpdateDoctor(int DoctorID, UpdateDoctorDTO updateDoctor)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DoctorID", DoctorID);
                    command.Parameters.AddWithValue("@Specialization", updateDoctor.Specialization);

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

        public static int DeleteDoctor(int DoctorID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Doctors_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DoctorID", DoctorID);

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
