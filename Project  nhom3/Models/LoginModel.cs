using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class LoginModel
    {
        [Required]
        public string username { get; set; } = null!;
 
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; } = null!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }
}
