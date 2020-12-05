using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Models;
using api.Entities;
using booking_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BookingContext _bookingContext;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            BookingContext bookingContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookingContext = bookingContext;
        }


        /// <summary>
        /// Creates a new user with given credentials and personal info
        /// </summary>
        public async Task<ApiResponse> RegisterUserAsync(RegisterModel model)
        {
            // check if user already exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return new ApiResponse(false, "User already exist", 422);

            var newUser = new User
            {
                FirstName = model.Firstname,
                LastName = model.Lastname,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                // TODO
                // Send confirmation email

                return new ApiResponse(201);
            }

            // return user registration errors
            var errors = result.Errors.Select(err => err.Description);
            return new ApiResponse("Could not register user, please try again", 400, errors);
        }

        /// <summary>
        /// Login with given email and password
        /// </summary>
        public async Task<ApiResponse> LoginUserAsync(LoginModel model)
        {
            // check if user exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return new ApiResponse("Wrong email or password", 400);

            // attempt user login with provided credentials
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!signInResult.Succeeded) return new ApiResponse("Wrong email or password", 400);

            // get user roles
            user.Roles = await _userManager.GetRolesAsync(user);
            var token = CreateRefreshToken();
            user.RefreshTokens.Add(token);
            _bookingContext.Update(user);
            _bookingContext.SaveChanges();

            return new ApiResponse(user, token);
        }

        public Task<ApiResponse> ConfirmEmailAsync(ConfirmEmailModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> ResetPasswordAsync(ResetPasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        // helper methods
        private static RefreshToken CreateRefreshToken()
        {
            var rnd = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(rnd);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(rnd),
                ExpiresAt = DateTime.Now.AddDays(30)
            };
        }
    }
}