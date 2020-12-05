using System;

namespace api.Entities
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public RefreshToken()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}