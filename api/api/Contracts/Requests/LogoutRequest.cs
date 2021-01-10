using System.ComponentModel.DataAnnotations;

namespace api.Contracts.Requests
{
    public class LogoutRequest
    {
        [Required(ErrorMessage = "User id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Token cannot be null")]
        public string RefreshToken { get; set; }
    }
}