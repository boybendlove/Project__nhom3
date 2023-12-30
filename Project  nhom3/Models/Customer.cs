using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Customer
    {
        [Key]
        public int? customer_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public string? phone_number { get; set; }
        public int? passport { get; set; }
        public string? Flag { get; set; }
        public string? CreditCard { get; set; }
        public int? NumberCard { get; set; }
        public int? SkyMiles { get; set; }
        public string? sex { get; set; }
        public string username { get; internal set; } = null!;
        public string password { get; internal set; } = null!;
    }
}
