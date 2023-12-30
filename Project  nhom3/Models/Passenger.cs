using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Passenger
    {
        [Key]
        [ReadOnly(true)]
        public int? passenger_id { get; set; }
        public int? customer_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? address { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? sex { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string? Flag { get; set; }

        public Customer? Customer { get; set; }
    }
    public class PassengerDto
    {
        public int? customer_id { get; set; }
        public string first_name { get; set; } = null!;
        public string? last_name { get; set; } = null!;
        public string? address { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? sex { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string? Flag { get; set; }
    }
}
