using Microsoft.EntityFrameworkCore;

namespace MyApi.Models
{
    public class AppDbContext : DbContext
    {  
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
