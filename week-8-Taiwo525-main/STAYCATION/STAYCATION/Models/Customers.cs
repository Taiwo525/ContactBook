using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using STAYCATION.Data;
namespace STAYCATION.Models
{
    public class Customers
    {
        public Customers()
        {
            Id = Guid.NewGuid();
            RegisteredOn = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }

        [Required]
        [DisplayName("Email")]

        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        [DisplayName("Password")]

        public string? PassWord { get; set; }

        [Required]
        [MinLength(8)]
        [DisplayName("Repeat Password")]

        public string? RepeatPassword { get; set; }

        public DateTime RegisteredOn { get; set; }
        public Customers(Guid Id, string fullname, string email, string password, DateTime dateTime)
        {

        }
    }
}
