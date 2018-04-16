using System.ComponentModel.DataAnnotations;
namespace raw_wedding.Models
{
    public class Login
    {
        [Required(ErrorMessage = "You must provide an email")]
        [EmailAddress(ErrorMessage = "You must enter a valid email")]
        public string Email {get;set;}

        [Required(ErrorMessage = "You must provide a password")]
        public string Password {get;set;}
    }
}