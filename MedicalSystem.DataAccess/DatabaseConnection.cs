using System;
using Microsoft.Data.SqlClient;

namespace MedicalSystem.DataAccess
{
    public class DatabaseConnection
    {
        private static string _connectionString =
        "Server=.;Database=MedicalSystemDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
