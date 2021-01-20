using System;
using System.Globalization;
using System.Threading.Tasks;
using api.Contracts.Requests;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("bookings")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] BookingRequest model)
        {
            if (model.End <= model.Start) return BadRequest("End time must be after start time");
            model.UserPrincipal = this.User;

            var response = await _bookingService.Create(model);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("week")]
        public async Task<IActionResult> GetBookingsWeek([FromQuery] BookingsWeekRequest model)
        {
            var response = await _bookingService.GetBookingsWeek(model);
            return StatusCode(response.StatusCode, response);
        }
    }
}