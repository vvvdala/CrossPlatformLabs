using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Model
    {

        [Key]
        [Column(TypeName = "char(10)")]
        public string ModelCode { get; set; }

        [Column(TypeName ="numeric(8,2)")]
        public decimal DailyHireRate { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ModelName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
