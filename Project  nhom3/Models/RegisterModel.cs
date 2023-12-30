using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class RegisterModel
    {
        [Required]
        public string username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string email { get; set; } = null!;

        [Required]
        public string first_name { get; set; } = null!;

        [Required]
        public string last_name { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime date_of_birth { get; set; } 
        [Required]
        public string address { get; set; } = null!;
        [Required]
        public string phone_number { get; set; } = null!;
        [Required]
        public string sex { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; } = null!;
    }
}
