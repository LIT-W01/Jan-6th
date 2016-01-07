using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication4.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Customer
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Region { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }

    public class NorthwindDb
    {
        private readonly string _connectionString;

        public NorthwindDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetCategories()
        {
            List<Category> result = new List<Category>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Categories";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category c = new Category();
                    c.Id = (int)reader["CategoryId"];
                    c.Name = (string)reader["CategoryName"];
                    c.Description = (string)reader["Description"];
                    result.Add(c);
                }

                return result;
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            List<Product> result = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Products WHERE CategoryId = @categoryId";
                command.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();
                    object productName = reader["ProductName"];
                    if (productName != DBNull.Value)
                    {
                        p.Name = (string)productName;
                    }

                    p.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                    p.UnitPrice = (decimal)reader["UnitPrice"];
                    result.Add(p);
                }

                return result;
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> result = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Customers";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(GetCustomerFromReader(reader));
                }

                return result;
            }
        }

        public Customer GetCustomerById(string customerId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Customers WHERE CustomerId = @customerId";
                command.Parameters.AddWithValue("@customerId", customerId);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return GetCustomerFromReader(reader);

            }
        }

        private Customer GetCustomerFromReader(SqlDataReader reader)
        {
            Customer c = new Customer();
            c.CustomerId = (string)reader["CustomerId"];
            c.CompanyName = (string)reader["CompanyName"];
            c.ContactName = (string)reader["ContactName"];
            c.ContactTitle = (string)reader["ContactTitle"];
            c.Address = (string)reader["Address"];
            c.City = (string)reader["City"];
            object fax = reader["Fax"];
            if (fax != DBNull.Value)
            {
                c.Fax = (string)fax;
            }
            object region = reader["Region"];
            if (region != DBNull.Value)
            {
                c.Region = (string)region;
            }

            return c;
        }
    }
}