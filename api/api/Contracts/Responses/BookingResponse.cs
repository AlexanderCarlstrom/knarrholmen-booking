using System.Collections.Generic;
using api.DTOs;

namespace api.Contracts.Responses
{
    public class BookingResponse : ApiResponse
    {
        public List<PrivateBookingsDto> UserBookings { get; set; }
        public List<PublicBookingsDto> Bookings { get; set; }

        public BookingResponse(List<PrivateBookingsDto> bookings) : base(true, 200)
        {
            UserBookings = bookings;
        }

        public BookingResponse(List<PublicBookingsDto> bookings) : base(true, 200)
        {
            Bookings = bookings;
        }

        public BookingResponse(int statusCode, string message) : base(false, statusCode, message)
        {
        }
    }
}