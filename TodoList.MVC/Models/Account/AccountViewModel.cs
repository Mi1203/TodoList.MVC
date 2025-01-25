using System.ComponentModel.DataAnnotations;

namespace TodoList.MVC.Models.Account
{
    public class AccountViewModel
    {
        public LoginViewModel? LoginViewModel { get; set; }
        public RegistrationViewModel? RegistrationViewModel { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "This Filed is requrired")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "This Filed is requrired")]
        public string? Password { get; set; }
    }

    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "This Filed is requrired")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "This Filed is requrired")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This Filed is requrired")]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        public string? RepeatPassword { get; set; }
    }
}
