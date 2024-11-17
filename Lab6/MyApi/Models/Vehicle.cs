using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Vehicle
    {
        [Key]
        [Column(TypeName = "char(5)")]
        public string RegNumber { get; set; }

        [ForeignKey("Manufacturer")]
        [Column(TypeName = "char(10)")]
        public string ManufacturerStatusCode { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [ForeignKey("Model")]
        [Column(TypeName = "char(10)")]
        public string ModelCode { get; set; }
        public Model Model { get; set; }

        [ForeignKey("VehicleCategory")]
        [Column(TypeName = "char(5)")]
        public string VehicleCategoryCode { get; set; }
        public VehicleCategory VehicleCategory { get; set; }

        [Column(TypeName = "integer")]
        public int CurrentMileAge { get; set; }

        [Column(TypeName = "numeric(8,2)")]
        public decimal DailyHireRate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateMotDue { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
