using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class PersonData
    {
        public static List<PersonDTO> GetAllPersons()
        {
            var PersonsList = new List<PersonDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Persons_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonsList.Add(new PersonDTO
                            {
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
            return PersonsList;
        }

        public static PersonDTO? GetPersonByID(int PersonID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Persons_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonDTO
                            {
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

        public static int AddNewPerson(CreatePersonDTO createPerson)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Persons_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", createPerson.Name);
                    command.Parameters.AddWithValue("@DateOfBirth",
                        createPerson.DateOfBirth ?? (object)DBNull.Value);

                    command.Parameters.Add("@Gender", SqlDbType.NVarChar, 1)
                           .Value = createPerson.Gender;

                    command.Parameters.AddWithValue("@PhoneNumber",
                        createPerson.PhoneNumber);

                    command.Parameters.AddWithValue("@Email",
                        createPerson.Email ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@Address",
                        createPerson.Address ?? (object)DBNull.Value);

                    var newId = new SqlParameter("@NewPersonID", SqlDbType.Int)
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

        public static int UpdatePerson(int PersonID, UpdatePersonDTO updatePerson)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Persons_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@Name", updatePerson.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", updatePerson.DateOfBirth ?? (object)DBNull.Value);

                    command.Parameters.Add("@Gender", SqlDbType.NVarChar, 1)
                           .Value = updatePerson.Gender;
                    command.Parameters.AddWithValue("@PhoneNumber", updatePerson.PhoneNumber);

                    command.Parameters.AddWithValue("@Email", updatePerson.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", updatePerson.Address ?? (object)DBNull.Value);

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

        public static int DeletePerson(int PersonID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Persons_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

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
