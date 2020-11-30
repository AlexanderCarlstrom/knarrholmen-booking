using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using booking_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace booking_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<Response> RegisterUserAsync(RegisterModel model)
        {
            // check if user already exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return Response.UnprocessableEntity("User already exist");

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

                return Response.Created("User created successfully");
            }

            // return user registration errors
            var response = Response.BadRequest("Could not register user, please try again");
            response.Errors = result.Errors.Select(err => err.Description);
            return response;
        }

        /**
         * <summary>Login use with provided email and password</summary>
         */
        public async Task<Response> LoginUserAsync(LoginModel model)
        {
            // check if user exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Response.BadRequest("Wrong email or password");

            // attempt user login with provided credentials
            var signInResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!signInResult) return Response.BadRequest("Wrong email or password");
            
            // get user roles
            user.Roles = await _userManager.GetRolesAsync(user);

            // create sing in response
            var response = new LoginResponse
            {
                Message = "User authenticated",
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                User = user,
                JWT = await GenerateJwt(user),
            };
            return response;
        }

        public Task<Response> ConfirmEmailAsync(ConfirmEmailModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> ResetPasswordAsync(ResetPasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        // helper methods
        /**
         * <summary>Generate jwt with user credentials</summary>
         */
        private async Task<string> GenerateJwt(User user)
        {
            // get user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            // create new claims
            var authClaims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // add user roles to claims
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            // create sign in key from secret
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // create jwt
            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}