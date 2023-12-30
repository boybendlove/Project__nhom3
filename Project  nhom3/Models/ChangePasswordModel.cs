using System.ComponentModel.DataAnnotations;

namespace Project__nhom3.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

    }
}
