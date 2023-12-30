using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Ticket
    {
        [Key]
        public int? ticket_id { get; set; }
        public int? flight_id { get; set; }
        public int? customer_id { get; set; }
        public int? passenger_id { get; set; }
        public int? seat_id { get; set; }
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set; }
        public DateTime? departure_time { get; set; }
        public DateTime? arrival_time { get; set; }
        public DateTime? real_time_flight { get; set; }
        public string? ticket_status { get; set; }
        public decimal? price { get; set; }
        public string? Flag { get; set; }
        public Passenger? Passenger { get; set; }
        public Flight? Flight { get; set; }
        public Customer? Customer { get; set; }
        public Seat? Seat { get; set; }
    }
    public class TicketDto
    {
        public int? flight_id { get; set; }
        public int? customer_id { get; set; }
        public int? passenger_id { get; set; }
        public int? seat_id { get; set; }
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set; }
        public DateTime? departure_time { get; set; }
        public DateTime? arrival_time { get; set; }
        public DateTime? real_time_flight { get; set; }
        public string? ticket_status { get; set; }
        public decimal? price { get; set; }
        public string? Flag { get; set; }
    }
}

