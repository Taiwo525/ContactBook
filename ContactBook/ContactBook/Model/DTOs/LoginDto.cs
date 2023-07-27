using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required]
        public string RememberMe  { get; set; }
    }
}
