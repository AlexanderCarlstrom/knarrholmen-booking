using System.Threading.Tasks;
using booking_api.Models;

namespace booking_api.Services
{
    public interface IAuthService
    {
        Task<Response> RegisterUserAsync(RegisterModel model);
        Task<Response> LoginUserAsync(LoginModel model);
        Task<Response> ConfirmEmailAsync(ConfirmEmailModel model);
        Task<Response> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<Response> ResetPasswordAsync(ResetPasswordModel model);
    }
}