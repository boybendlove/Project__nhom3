using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class Reschedule_Ticket
    {
        [Key]
        public int? reschedule_ticket_id { get; set; }
        public string? status_ticket { get; set; }
    }
}
