using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Location
    {
        [Key]
        public int? location_id { get; set; }
        public int? parentId { get; set; }
        public string? Flag { get; set; }
        public string? nation { get; set; }
        public string? Start_location { get; set; }
        public string? End_location { get; set; }
        public int? Flightdistance { get; set; }
        public string? Start_airport { get; set; }
        public string? End_airport { get; set; }

    }
}
