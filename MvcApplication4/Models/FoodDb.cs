using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication4.Models
{
    public class FoodDb
    {
        private readonly string _connectionString;

        public FoodDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCustomer(string name, string address)
        {
            using(var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Customers VALUES (@name, @address)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}