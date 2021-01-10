using System.Collections.Generic;
using System.Text.Json.Serialization;
using api.Entities;

namespace api.Contracts.Responses
{
    public class UserResponse : Response
    {
        public User User { get; set; }
        [JsonIgnore] public RefreshToken RefreshToken { get; set; }

        public UserResponse(int statusCode, User user, RefreshToken refreshToken)
            : base(true, statusCode)
        {
            User = user;
            RefreshToken = refreshToken;
        }

        public UserResponse(int statusCode, string message)
            : base(statusCode, message)
        {
        }

        public UserResponse(int statusCode, string message, IEnumerable<string> errors)
            : base(statusCode, message, errors)
        {
        }
    }
}