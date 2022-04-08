using System.ComponentModel.DataAnnotations;

namespace Fora.UI.Model
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Username field is required")]
       // [Range(4, 10, ErrorMessage = "Username must be between 4 and 10 characters!")]  // works only with Intigers
        [MaxLength(10 , ErrorMessage = "Can't be longer than 10 characters")]
        [MinLength(4, ErrorMessage = "Can't be shorter than 4 characters")]
        public string Username { get; set; } 
        //-----------------------------------------------------------------------------------
        [Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; } 
        //-----------------------------------------------------------------------------------
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; } 
        //-----------------------------------------------------------------------------------
        [Required(ErrorMessage = "Verifying your password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [Display(Name = "Password Verifying")]
        public string VerifiedPassword { get; set; }
       // public bool RemeberMe { get; set; }



    }
}
