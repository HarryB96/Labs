using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace Lab05CRUDAppRawSQL
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static void Main(string[] args)
        {
            var justAddedCustomer = new Customer();
            //conection string
            string connectionString = @"Data Source=(localdb)\mssqllocaldb; Initial Catalog=Northwind";
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                Console.WriteLine(sqlConnection.State);

                
                justAddedCustomer = addCustomer(sqlConnection);

                updateCustomer(sqlConnection, justAddedCustomer);
                deleteCustomer(sqlConnection, justAddedCustomer);
                listCustomer(sqlConnection);
            }
            static void listCustomer(SqlConnection sqlConnection)
            {
                customers.Clear();
                using (var sqlCommand = new SqlCommand("SELECT * FROM Customers", sqlConnection))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        var customer = new Customer()
                        {
                            CustomerId = sqlReader["CustomerID"].ToString(),
                            ContactName = sqlReader["ContactName"].ToString(),
                            CompanyName = sqlReader["CompanyName"].ToString(),
                            City= sqlReader["City"].ToString(),
                            Country = sqlReader["Country"].ToString()
                        };
                        customers.Add(customer);
                    }
                }
                //foreach (var customer in customers)
                //{
                //    Console.WriteLine($"{customer.CustomerId}{customer.ContactName}{customer.CompanyName}" + $"{customer.City}{customer.Country}");
                //}
                customers.ForEach(customer => Console.WriteLine($"{customer.CustomerId, -10}{customer.ContactName, -30}{customer.CompanyName, -40}" + $"{customer.City, -20}{customer.Country, -15}"));
            }

            static Customer addCustomer(SqlConnection sqlConnection)
            {
                var randomCustomerId = randomIdGenerator();
                var newCustomer = new Customer()
                {
                    CustomerId = randomCustomerId,
                    CompanyName = "Sparta",
                    ContactName = "Harry",
                    City = "London",
                    Country = "UK"
                };

                //var sqlString = "INSERT INTO Customers (CustomerID, ContactName, CompanyName, City, Country)" + "VALUES ('HARR1','Harry', 'Sparta', 'London', 'UK')";

                //using(var sqlCommand = new SqlCommand(sqlString, sqlConnection))
                //{
                //    int affected = sqlCommand.ExecuteNonQuery();
                //    Console.WriteLine(affected);
                //}

                var sqlString2 = "INSERT INTO Customers (CustomerID, ContactName, CompanyName, City, Country)" + "VALUES(@CustomerID, @ContactName, @CompanyName, @City, @Country)";
                using(var sqlCommand2 = new SqlCommand(sqlString2, sqlConnection))
                {
                    sqlCommand2.Parameters.AddWithValue("@CustomerID", newCustomer.CustomerId);
                    sqlCommand2.Parameters.AddWithValue("@ContactName", newCustomer.ContactName);
                    sqlCommand2.Parameters.AddWithValue("@CompanyName", newCustomer.CompanyName);
                    sqlCommand2.Parameters.AddWithValue("@City", newCustomer.City);
                    sqlCommand2.Parameters.AddWithValue("@Country", newCustomer.Country);
                    int affected = sqlCommand2.ExecuteNonQuery();
                    Console.WriteLine(affected);
                    if (affected == 1)
                    {
                        return newCustomer;
                    }
                }
                return null;
            }

            static void updateCustomer(SqlConnection sqlConnection, Customer c)
            {
                c.ContactName = "New Name";
                var updateSqlString = $"UPDATE Customers SET ContactName = '{c.ContactName}'" + $"WHERE CustomerID = '{c.CustomerId}' ";
                using (var sqlCommand = new SqlCommand(updateSqlString, sqlConnection))
                {
                    int affected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(affected);
                }
            }

            static void deleteCustomer(SqlConnection sqlConnection, Customer c)
            {
                var deleteSqlString = $"DELETE FROM Customers WHERE ContactName = '{c.ContactName}'";
                using (var sqlCommand = new SqlCommand(deleteSqlString, sqlConnection))
                {
                    int affected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(affected);
                }
            }

            static string randomIdGenerator()
            {
                char[] iD = new char[5];
                var rnd = new Random();
                for (int i = 0; i < iD.Length; i++)
                {
                    iD[i] = (char)rnd.Next('A', 'Z');
                }
                string iDString = new string(iD);
                Console.WriteLine(iD);
                return iDString;
            }
        }
    }
    class Customer
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
