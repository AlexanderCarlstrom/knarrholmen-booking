using System.Collections.Generic;
using api.Entities;
using Newtonsoft.Json;

namespace api.Models
{
    public class ApiResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        [JsonIgnore] public int Status { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
        public User User { get; private set; }
        [JsonIgnore] public RefreshToken RefreshToken { get; private set; }

        /// <summary>
        /// base constructor
        /// </summary>
        public ApiResponse(bool success, string message, int status)
        {
            Success = success;
            Message = message;
            Status = status;
        }

        /// <summary>
        /// Creates a simple success response
        /// </summary>
        public ApiResponse(int status) : this(true, string.Empty, status)
        {
        }

        /// <summary>
        /// Creates an error response with given message and status code
        /// </summary>
        public ApiResponse(string message, int status) : this(false, message, status)
        {
        }
        
        /// <summary>
        /// Creates an error response with given message, status code and errors array
        /// </summary>
        public ApiResponse(string message, int status, IEnumerable<string> errors) : this(false, message, status)
        {
            Errors = errors;
        }

        /// <summary>
        /// Crates a user response with 200 status code
        /// </summary>
        public ApiResponse(User user, RefreshToken refreshToken) : this(true, string.Empty, 200)
        {
            User = user;
            RefreshToken = refreshToken;
        }
    }
}