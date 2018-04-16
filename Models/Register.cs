using System.ComponentModel.DataAnnotations;


namespace raw_wedding.Models
{
        public class Register
    {
        [Required(ErrorMessage = "You must provide a first name.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
        public string Fname {get;set;}
        [Required(ErrorMessage = "You must provide a last name.")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters.")]
        public string Lname {get;set;}
        [Required(ErrorMessage = "You must provide an email.")]
        [EmailAddress(ErrorMessage = "Your email must be valid.")]
        public string Email {get;set;}
        [Required(ErrorMessage = "You must provide a password.")]
        [DataType(DataType.Password)]
        [Compare("passmatch")]
        public string pass {get;set;}
        [Required(ErrorMessage = "Please re-enter your password.")]
        public string passmatch {get;set;}
    }
}