using System.Data;
using MedicalSystem.DTOs;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class AppointmentData
    {
        public static List<AppointmentDTO> GetAllAppointments()
        {
            var AppointmentsList = new List<AppointmentDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AppointmentsList.Add(new AppointmentDTO
                            {
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),

                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),

                                AppointmentDateTime =
                                    reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),

                                AppointmentStatus =
                                    reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
                            });
                        }
                    }
                }
            }
            return AppointmentsList;
        }

        public static AppointmentDTO? GetAppointmentByID(int AppointmentID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_GetByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AppointmentDTO
                            {
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),

                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),

                                AppointmentDateTime =
                                    reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),

                                AppointmentStatus =
                                    reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
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

        public static List<AppointmentDTO> GetAppointmentsByDoctor(int DoctorID)
        {
            var AppointmentsList = new List<AppointmentDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_GetByDoctor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DoctorID", DoctorID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AppointmentsList.Add(new AppointmentDTO
                            {
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),

                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),

                                AppointmentDateTime =
                                    reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),

                                AppointmentStatus =
                                    reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
                            });
                        }
                    }
                }
            }
            return AppointmentsList;
        }

        public static List<AppointmentDTO> GetAppointmentsByPatient(int PatientID)
        {
            var AppointmentsList = new List<AppointmentDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_GetByPatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PatientID", PatientID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AppointmentsList.Add(new AppointmentDTO
                            {
                                AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),

                                PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                PatientName = reader.GetString(reader.GetOrdinal("PatientName")),

                                DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),

                                AppointmentDateTime =
                                    reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),

                                AppointmentStatus =
                                    reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
                            });
                        }
                    }
                }
            }
            return AppointmentsList;
        }

        public static List<AppointmentLookupDTO> GetAvailableForMedicalRecord()
        {
            var appointments = new List<AppointmentLookupDTO>();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            using (SqlCommand command = new SqlCommand("SP_Appointments_GetAvailableForMedicalRecord", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new AppointmentLookupDTO
                        {
                            AppointmentID = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                            PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                            DoctorName = reader.GetString(reader.GetOrdinal("DoctorName"))
                        });
                    }
                }
            }

            return appointments;
        }

        public static int AddNewAppointment(CreateAppointmentDTO createAppointment)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_Add", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PatientID", createAppointment.PatientID);
                    command.Parameters.AddWithValue("@DoctorID", createAppointment.DoctorID);
                    command.Parameters.AddWithValue("@AppointmentDateTime", createAppointment.AppointmentDateTime);
                    command.Parameters.AddWithValue("@AppointmentStatus", createAppointment.AppointmentStatus);

                    var newId = new SqlParameter("@NewAppointmentID", SqlDbType.Int)
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

        public static int UpdateAppointment(int AppointmentID, UpdateAppointmentDTO updateAppointment)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    command.Parameters.AddWithValue("@PatientID", updateAppointment.PatientID);
                    command.Parameters.AddWithValue("@DoctorID", updateAppointment.DoctorID);
                    command.Parameters.AddWithValue("@AppointmentDateTime", updateAppointment.AppointmentDateTime);
                    command.Parameters.AddWithValue("@AppointmentStatus", updateAppointment.AppointmentStatus);

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

        public static int DeleteAppointment(int AppointmentID)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("SP_Appointments_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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