using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Contexts;
using api.Contracts;
using api.Contracts.Requests;
using api.Contracts.Responses;
using api.DTOs;
using api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public interface IAuthService
    {
        Task<Response> RegisterUserAsync(RegisterRequest model);
        Task<UserResponse> LoginUserAsync(LoginRequest model);
        Task<UserResponse> LoginWithTokenASync(string refreshToken);
        Task<Response> LogoutUserAsync(LogoutRequest model);
        Task<Response> ConfirmEmailAsync(ConfirmEmailRequest model);
        Task<Response> ForgetPasswordAsync([EmailAddress] string email);
        Task<Response> ResetPasswordAsync(ResetPasswordRequest model);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BookingDbContext _bookingDbContext;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            BookingDbContext bookingDbContext, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookingDbContext = bookingDbContext;
            _mapper = mapper;
        }


        /// <summary>
        /// Creates a new user with given credentials and personal info
        /// </summary>
        public async Task<Response> RegisterUserAsync(RegisterRequest model)
        {
            // check if user already exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return new Response(422, "User already exist");

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
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                // TODO
                // Send confirmation email

                return new Response(true, 201);
            }

            // return user registration errors
            var errors = result.Errors.Select(err => err.Description);
            return new Response(400, "Could not register user, please try again", errors);
        }

        /// <summary>
        /// Login with given email and password
        /// </summary>
        public async Task<UserResponse> LoginUserAsync(LoginRequest model)
        {
            // Check if user exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return new UserResponse(400, "Wrong email or password");

            // Check if password is correct
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheck) return new UserResponse(400, "Wrong email or password");

            // Check if email has been confirmed
            var confirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!confirmed) return new UserResponse(403, "Email must been confirmed");

            await _signInManager.SignInAsync(user, model.Remember);

            // Get user roles
            user.Roles = await _userManager.GetRolesAsync(user);

            // map to user dto
            var userDto = _mapper.Map<UserDTO>(user);

            if (!model.Remember) return new UserResponse(200, userDto);
            
            // Create refresh token
            var token = new RefreshToken(DateTime.Now.AddDays(30));
            user.RefreshTokens.Add(token);
            _bookingDbContext.Update(user);
            await _bookingDbContext.SaveChangesAsync();

            return new UserResponse(200, userDto, token);

        }

        public async Task<UserResponse> LoginWithTokenASync(string refreshToken)
        {
            var token = await _bookingDbContext.RefreshTokens.FindAsync(refreshToken);
            if (token == null) return new UserResponse(401, "invalid refresh token");
            var user = await _userManager.FindByIdAsync(token.UserId);
            await _signInManager.SignInAsync(user, true);
            
            var userDto = _mapper.Map<UserDTO>(user);
            return new UserResponse(200, userDto);
        }

        public async Task<Response> LogoutUserAsync(LogoutRequest model)
        {
            await _signInManager.SignOutAsync();
            
            if (model.RefreshToken == null) return new Response(true, 200);

            var token = await _bookingDbContext.RefreshTokens.FindAsync(model.RefreshToken);
            if (token == null) return new Response(false, 400, "Something went wrong");

            _bookingDbContext.RefreshTokens.Remove(token);
            await _bookingDbContext.SaveChangesAsync();
            
            return new Response(true, 200);
        }

        public Task<Response> ConfirmEmailAsync(ConfirmEmailRequest model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> ForgetPasswordAsync([EmailAddress] string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> ResetPasswordAsync(ResetPasswordRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}