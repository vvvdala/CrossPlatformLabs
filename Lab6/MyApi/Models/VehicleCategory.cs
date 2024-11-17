using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class VehicleCategory
    {
        [Key]
        [Column(TypeName = "char(5)")]
        public string VehicleCategoryCode { get; set; }
        [Column(TypeName = "char(10)")]
        public string VehicleCategoryDescription { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
