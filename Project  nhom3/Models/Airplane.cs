using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Airplane
    {
        [Key]
        public int airplane_id { get; set; }
        public string? name { get; set; }
        public string? logo_url { get; set; }
        public int? seat_total { get; set; }
        public string? airplane_brand { get; set; }
        public string? Flag { get; set; }
        public string? model { get; set; }
        public int? seat_id { get; set; }
        public Seat? Seat { get; internal set; }
    }
}
