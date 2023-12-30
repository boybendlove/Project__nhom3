using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Ticket_type
    {
        [Key]
        public int ticket_type_id { get; set; }
        public string? name { get; set; }
    }
}
