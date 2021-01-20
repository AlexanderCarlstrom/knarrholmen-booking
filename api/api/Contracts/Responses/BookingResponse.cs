using System.Collections.Generic;
using api.DTOs;

namespace api.Contracts.Responses
{
    public class BookingResponse : ApiResponse
    {
        public PrivateBookingsDto Booking { get; set; }
        public List<PublicBookingsDto> Bookings { get; set; }

        public BookingResponse(PrivateBookingsDto booking) : base(true, 200)
        {
            Booking = booking;
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