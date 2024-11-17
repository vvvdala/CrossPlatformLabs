using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Manufacturer
    {
        [Key]
        [Column(TypeName = "char(10)")]
        public string ManufacturerStatusCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ManufacturerName { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string ManufacturerDetails{ get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
