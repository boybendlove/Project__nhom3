using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Seat
    {
        [Key]
        public int? seat_id { get; set; }
        public int? seat_total { get; set; }
        public string? Flag { get; set; }
        public int? total_economy { get; set; }
        public int? total_business { get; set; }

    }
}
