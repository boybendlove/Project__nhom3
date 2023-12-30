using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Booking
    {
        [Key]
        public int? booking_id { get; set; }
        public int? ticket_id { get; set; }
        public int? customer_id { get; set; }
        public DateTime? time_order { get; set; }
        public string? booking_status { get; set; }

        public Ticket? Ticket { get; set; }
        public Customer? Customer { get; set; }

    }
    public class BookingDto
    {

        public int? ticket_id { get; set; }
        public int? customer_id { get; set; }
        public DateTime? time_order { get; set; }
        public string? booking_status { get; set; }


    }
}
