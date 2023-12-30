using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Flight
    {
        [Key]
        public int flight_id { get; set; }
        public int? airplane_id { get; set; }
        public int? location_id { get; set; }
        public DateTime? time_start { get; set; }
        public DateTime? time_end { get; set; }
        public string? flight_status { get; set; }
        public decimal? price_economy { get; set; }
        public decimal? price_business { get; set; }
        public int? economy_booked { get; set; }
        public int? business_booked { get; set; }
        public string? economy_status { get; set; }
        public string? business_status { get; set; }
        public Airplane? Airplane { get; set; }
        public Location? Location { get; set; }
    }
}