using System;
using System.Collections.Generic;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data
{
    public static class DataGenerator
    {
        public static List<Car> GenerateCars()
        {
            return new List<Car>
            {
                new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2020, IsAvailable = true },
                new Car { Id = 2, Make = "Honda", Model = "Accord", Year = 2019, IsAvailable = false },
                new Car { Id = 3, Make = "Ford", Model = "Focus", Year = 2021, IsAvailable = true }
            };
        }

        public static List<Customer> GenerateCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice Johnson", Contact = "alice@example.com", LicenseNo = "A1234567" },
                new Customer { Id = 2, Name = "Bob Smith", Contact = "bob@example.com", LicenseNo = "B7654321" }
            };
        }

        public static List<RentalDisplay> GenerateRentals(List<Car> cars, List<Customer> customers)
        {
            var rentals = new List<RentalDisplay>();

            try
            {
                var car = cars[0];
                var customer = customers[0];
                rentals.Add(new RentalDisplay
                {
                    Id = 1,
                    CarDisplay = $"{car.Make} {car.Model}",
                    CustomerDisplay = customer.Name,
                    RentalDate = DateTime.Now.AddDays(-3).ToShortDateString(),
                    ReturnDate = DateTime.Now.AddDays(2).ToShortDateString(),
                    Price = "$120.00"
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating rental data: " + ex.Message);
            }
            return rentals;
        }
    }
}
