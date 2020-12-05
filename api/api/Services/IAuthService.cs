using System.Threading.Tasks;
using api.Models;

namespace booking_api.Services
{
    public interface IAuthService
    {
        Task<ApiResponse> RegisterUserAsync(RegisterModel model);
        Task<ApiResponse> LoginUserAsync(LoginModel model);
        Task<ApiResponse> ConfirmEmailAsync(ConfirmEmailModel model);
        Task<ApiResponse> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<ApiResponse> ResetPasswordAsync(ResetPasswordModel model);
    }
}