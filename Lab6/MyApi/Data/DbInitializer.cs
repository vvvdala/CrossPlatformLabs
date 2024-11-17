using MyApi.Models;

namespace MyApi.Data
{
    public class DbInitializer
    {
        public static void Initiliazer(AppDbContext appDbContext)
        {
            appDbContext.Database.EnsureCreated();

            if (!appDbContext.BookingStatuses.Any())
            {
                appDbContext.BookingStatuses.AddRange(
                    new BookingStatus { BookingStatusCode = "NEW", BookingStatusDescription = "NewBook" },
                    new BookingStatus { BookingStatusCode = "CON", BookingStatusDescription = "Confirmed" },
                    new BookingStatus { BookingStatusCode = "CNC", BookingStatusDescription = "Cancelled" },
                    new BookingStatus { BookingStatusCode = "CMP", BookingStatusDescription = "Completed" },
                    new BookingStatus { BookingStatusCode = "PND", BookingStatusDescription = "Pending" }
                );
            }

            if (!appDbContext.Manufacturers.Any())
            {
                appDbContext.Manufacturers.AddRange(
                    new Manufacturer { ManufacturerStatusCode = "MFR001", ManufacturerName = "Manufacturer 1", ManufacturerDetails = "Details about manufacturer 1" },
                    new Manufacturer { ManufacturerStatusCode = "MFR002", ManufacturerName = "Manufacturer 2", ManufacturerDetails = "Details about manufacturer 2" },
                    new Manufacturer { ManufacturerStatusCode = "MFR003", ManufacturerName = "Manufacturer 3", ManufacturerDetails = "Details about manufacturer 3" },
                    new Manufacturer { ManufacturerStatusCode = "MFR004", ManufacturerName = "Manufacturer 4", ManufacturerDetails = "Details about manufacturer 4" },
                    new Manufacturer { ManufacturerStatusCode = "MFR005", ManufacturerName = "Manufacturer 5", ManufacturerDetails = "Details about manufacturer 5" }
                );
            }

            if (!appDbContext.Models.Any())
            {
                appDbContext.Models.AddRange(
                    new Model { ModelCode = "M001", DailyHireRate = 100, ModelName = "Model 1" },
                    new Model { ModelCode = "M002", DailyHireRate = 120, ModelName = "Model 2" },
                    new Model { ModelCode = "M003", DailyHireRate = 150, ModelName = "Model 3" },
                    new Model { ModelCode = "M004", DailyHireRate = 130, ModelName = "Model 4" },
                    new Model { ModelCode = "M005", DailyHireRate = 200, ModelName = "Model 5" }
                );
            }

            if (!appDbContext.VehicleCategories.Any())
            {
                appDbContext.VehicleCategories.AddRange(
                    new VehicleCategory { VehicleCategoryCode = "VC001", VehicleCategoryDescription = "Category 1" },
                    new VehicleCategory { VehicleCategoryCode = "VC002", VehicleCategoryDescription = "Category 2" },
                    new VehicleCategory { VehicleCategoryCode = "VC003", VehicleCategoryDescription = "Category 3" },
                    new VehicleCategory { VehicleCategoryCode = "VC004", VehicleCategoryDescription = "Category 4" },
                    new VehicleCategory { VehicleCategoryCode = "VC005", VehicleCategoryDescription = "Category 5" }
                );
            }

            if (!appDbContext.Customers.Any())
            {
                appDbContext.Customers.AddRange(
                    new Customer { CustomerName = "Customer 1", CustomerEmail = "customer1@email.com", CustomerDetails = "Details about customer 1", Gender = "M", CustomerPhone = "1234567890", AddressLine1 = "Address 1", AddressLine2 = "Address 1", AddressLine3 = "Address 1", Town = "Town 1", County = "County 1", Country = "Country 1" },
                    new Customer { CustomerName = "Customer 2", CustomerEmail = "customer2@email.com", CustomerDetails = "Details about customer 2", Gender = "F", CustomerPhone = "0987654321", AddressLine1 = "Address 2", AddressLine2 = "Address 2", AddressLine3 = "Address 2", Town = "Town 2", County = "County 2", Country = "Country 2" },
                    new Customer { CustomerName = "Customer 3", CustomerEmail = "customer3@email.com", CustomerDetails = "Details about customer 3", Gender = "M", CustomerPhone = "1122334455", AddressLine1 = "Address 3", AddressLine2 = "Address 3", AddressLine3 = "Address 3", Town = "Town 3", County = "County 3", Country = "Country 3" },
                    new Customer { CustomerName = "Customer 4", CustomerEmail = "customer4@email.com", CustomerDetails = "Details about customer 4", Gender = "F", CustomerPhone = "6677889900", AddressLine1 = "Address 4", AddressLine2 = "Address 4", AddressLine3 = "Address 4", Town = "Town 4", County = "County 4", Country = "Country 4" },
                    new Customer { CustomerName = "Customer 5", CustomerEmail = "customer5@email.com", CustomerDetails = "Details about customer 5", Gender = "M", CustomerPhone = "5566778899", AddressLine1 = "Address 5", AddressLine2 = "Address 5", AddressLine3 = "Address 5", Town = "Town 5", County = "County 5", Country = "Country 5" }
                );
            }

            if (!appDbContext.Vehicles.Any())
            {
                appDbContext.Vehicles.AddRange(
                    new Vehicle { RegNumber = "V001", ManufacturerStatusCode = "MFR001", ModelCode = "M001", VehicleCategoryCode = "VC001", CurrentMileAge = 1000, DailyHireRate = 100, DateMotDue = DateTime.Now.AddMonths(6) },
                    new Vehicle { RegNumber = "V002", ManufacturerStatusCode = "MFR002", ModelCode = "M002", VehicleCategoryCode = "VC002", CurrentMileAge = 1500, DailyHireRate = 120, DateMotDue = DateTime.Now.AddMonths(5) },
                    new Vehicle { RegNumber = "V003", ManufacturerStatusCode = "MFR003", ModelCode = "M003", VehicleCategoryCode = "VC003", CurrentMileAge = 2000, DailyHireRate = 150, DateMotDue = DateTime.Now.AddMonths(3) },
                    new Vehicle { RegNumber = "V004", ManufacturerStatusCode = "MFR004", ModelCode = "M004", VehicleCategoryCode = "VC004", CurrentMileAge = 2500, DailyHireRate = 130, DateMotDue = DateTime.Now.AddMonths(7) },
                    new Vehicle { RegNumber = "V005", ManufacturerStatusCode = "MFR005", ModelCode = "M005", VehicleCategoryCode = "VC005", CurrentMileAge = 3000, DailyHireRate = 200, DateMotDue = DateTime.Now.AddMonths(1) }
                );
            }

            if (!appDbContext.Bookings.Any())
            {
                appDbContext.Bookings.AddRange(
                    new Booking { BookingStatusCode = "NEW", CustomerId = 1, RegNumber = "V001", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(3), ConfirmationLetterSentYN = "Y", PaymentReceivedYN = "Y" },
                    new Booking { BookingStatusCode = "CON", CustomerId = 2, RegNumber = "V002", DateFrom = DateTime.Now.AddDays(1), DateTo = DateTime.Now.AddDays(4), ConfirmationLetterSentYN = "Y", PaymentReceivedYN = "Y" },
                    new Booking { BookingStatusCode = "CNC", CustomerId = 3, RegNumber = "V003", DateFrom = DateTime.Now.AddDays(2), DateTo = DateTime.Now.AddDays(5), ConfirmationLetterSentYN = "N", PaymentReceivedYN = "N" },
                    new Booking { BookingStatusCode = "PND", CustomerId = 4, RegNumber = "V004", DateFrom = DateTime.Now.AddDays(3), DateTo = DateTime.Now.AddDays(6), ConfirmationLetterSentYN = "Y", PaymentReceivedYN = "N" },
                    new Booking { BookingStatusCode = "CMP", CustomerId = 5, RegNumber = "V005", DateFrom = DateTime.Now.AddDays(4), DateTo = DateTime.Now.AddDays(7), ConfirmationLetterSentYN = "Y", PaymentReceivedYN = "Y" }
                );
            }

            appDbContext.SaveChanges();

        }
    }
}
