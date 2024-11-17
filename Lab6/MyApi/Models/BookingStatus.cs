using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    public class BookingStatus
    {
        [Key]
        [Column(TypeName = "char(3)")]
        public string BookingStatusCode { get; set; }
        [Column(TypeName = "char(10)")]
        public string BookingStatusDescription { get;set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
