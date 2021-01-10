using System;
using Newtonsoft.Json;

namespace api.Entities
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        [JsonIgnore] public string UserId { get; set; }

        public RefreshToken()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}