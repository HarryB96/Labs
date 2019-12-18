using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab14LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Explanation
            /*
             * 1) read Northwind using entity core 2.1.1
             * 2) Basic LINQ
             * 3) More advanced Linq with LAMBDA
             */
            #endregion

            //    List<Customer> customers = new List<Customer>();
            //    List<Customer> selectedCustomers2;
            //    List<Product> products;
            //    List<Category> categories;
            //    using (var db = new Northwind())
            //    {
            //        customers = db.Customers.ToList();

            //        Simple LINQ
            //        whole dataset returned (more data)
            //        Ienumerable array

            //        same query over database directly
            //        only return actual data required
            //        Lazy loading : query not actually exectuted - data not actually retrieved yet
            //        selectedCustomers2 =
            //           (from Customer in db.Customers
            //            where Customer.City == "London" || Customer.City == "Berlin"
            //            where Customer.CompanyName != "Sparta"
            //            orderby Customer.ContactName
            //            select Customer).ToList();
            //        force data by pushing to llist or by taking aggregate

            //        var selectedCustomer3 =
            //            (from Customer in db.Customers
            //             select new
            //             {
            //                 Name = Customer.ContactName,
            //                 Location = Customer.City + " " + Customer.Country
            //             }).ToList();

            //        foreach (var c in selectedCustomer3)
            //        {
            //            Console.WriteLine($"{c.Name,-25}{c.Location}");
            //        }

            //        Grouping
            //        group and list all customers by city
            //        var selectedCustomers4 =
            //            (from c in db.Customers
            //             group c by c.City into Cities
            //             where Cities.Count() > 1
            //             orderby Cities.Count() descending
            //             select new
            //             {
            //                 City = Cities.Key,
            //                 Count = Cities.Count()
            //             }).ToList();
            //        foreach (var c in selectedCustomers4)
            //        {
            //            Console.WriteLine($"{c.City,-15}{c.Count}");
            //        }

            //        Console.WriteLine("\n\nList of products joined to category showing name\n");

            //        var listOfProducts =
            //            (from p in db.Products
            //             join c in db.Categories
            //             on p.CategoryID equals c.CategoryID
            //             select new
            //             {
            //                 iD = p.ProductID,
            //                 Name = p.ProductName,
            //                 Category = c.CategoryName
            //             }).ToList();
            //        listOfProducts.ForEach(p => Console.WriteLine($"{p.iD,-15}{p.Name,-30}{p.Category}"));

            //        Console.WriteLine("\n\nSame list but using smarter 'dot' notation\n");
            //        products = db.Products.ToList();
            //        categories = db.Categories.ToList();
            //        products.ForEach(p => Console.WriteLine($"{p.ProductID,-15}{p.ProductName,-30}{p.Category.CategoryName}"));

            //        var selectedCusomers =
            //                from Customer in customers
            //                select Customer;


            //        Console.WriteLine("\n\nList Categories with count of products and sublist of product names\n");

            //        categories.ForEach(c =>
            //        {
            //            Console.WriteLine($"{c.CategoryID,-5}{c.CategoryName,-15} has {c.Products.Count()} products");
            //            foreach (var p in c.Products)
            //            {
            //                Console.WriteLine($"\t\t\t\t{p.ProductID,-5}{p.ProductName}");
            //            }
            //        });

            //        Console.WriteLine("\n\nList of distinct cities ordered by alphabetical\n");
            //        var cityList = db.Customers.Select(c => c.City).Distinct().OrderBy(c => c).ToList();
            //        cityList.ForEach(c => Console.WriteLine(c));

            //        Console.WriteLine("\n\nContains (SQL like)\n");
            //        var cityListFiltered = db.Customers.Select(c => c.City).Where(city => city.Contains("o")).Distinct().OrderBy(c => c).ToList();
            //        cityListFiltered.ForEach(city => Console.WriteLine(city));
            //    }


            //    printCustomers(selectedCustomers2);


            //}
            //static void printCustomers(List<Customer> customers)
            //{
            //    customers.ForEach(c => Console.WriteLine($"{c.CustomerID,-10}{c.ContactName,-30}{c.CompanyName,-40}{c.City}"));
            //}
        }

    }
    public class NorthwindCustomerCollection
    {
        public int peopleCount(string city)
        {
            if (city == null || city == string.Empty)
            {
                using (var db = new Northwind())
                {
                    return (db.Customers.Count());
                }
            }
            else
            {
                using (var db = new Northwind())
                {
                    return (db.Customers.Where(c => c.City == city).Count());
                }
            }
        }
    }
       
        #region DatabaseContextAndClasses
        // connect to database

        public partial class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string ContactTitle { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }
        public class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public virtual ICollection<Product> Products { get; set; }

            public Category()
            {
                this.Products = new List<Product>();
            }
        }

        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int? CategoryID { get; set; }
            public virtual Category Category { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal? UnitPrice { get; set; } = 0;
            public short? UnitsInStock { get; set; } = 0;
            public short? UnitsOnOrder { get; set; } = 0;
            public short? ReorderLevel { get; set; } = 0;
            public bool Discontinued { get; set; } = false;
        }

        public class Northwind : DbContext
        {
            public DbSet<Category> Categories { get; set; }

            public DbSet<Product> Products { get; set; }

            public DbSet<Customer> Customers { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;" + "Initial Catalog=Northwind;" + "Integrated Security = true;" + "MultipleActiveResultSets=true;");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Category>()
                    .Property(c => c.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                // define a one-to-many relationship
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.Products)
                    .WithOne(p => p.Category);

                modelBuilder.Entity<Product>()
                    .Property(c => c.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products);
            }
        }
        #endregion

}
