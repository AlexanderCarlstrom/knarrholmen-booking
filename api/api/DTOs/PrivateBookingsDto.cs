using System;

namespace api.DTOs
{
    public class PrivateBookingsDto
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ActivityName { get; set; }
        public string ActivityLocation { get; set; }
    }
}