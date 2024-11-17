using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("BookingStatus")]
        [Column(TypeName = "char(3)")]
        public string BookingStatusCode { get; set; }
        public BookingStatus BookingStatus { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Vehicle")]
        [Column(TypeName = "char(5)")]
        public string RegNumber { get; set; }
        public Vehicle Vehicle { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTo { get; set; }

        [Column(TypeName = "char(1)")]
        public string ConfirmationLetterSentYN { get; set; }

        [Column(TypeName = "char(1)")]
        public string PaymentReceivedYN { get; set; }
    }
}
