using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Payment
    {
        [Key]
        public int? payment_id { get; set; }
        public int? booking_id { get; set; }
        public int? customer_id { get; set; }
        public int? ticket_id { get; set; }
        public DateTime? payment_time { get; set; }
        public string? payment_method { get; set; }
        public DateTime? create_at { get; set; }
        public string? Flag { get; set; }

        public Booking? Booking { get; set; }
        public Customer? Customer { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
