using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    /**
     * Register model used for registration input
     */
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    
    /**
     * Login model used for login input
     */
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    /**
     * Confirm email model used for confirm email input
     */
    public class ConfirmEmailModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
    }

    /**
     * Forget password model used for forget password input
     */
    public class ForgetPasswordModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    /**
     * Reset password model used for reset password input
     */
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}