using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contexts;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace booking_api.Controllers
{
    [Route("activities")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly BookingDbContext _bookingDbContext;

        public ActivityController(IActivityService activityService, BookingDbContext bookingDbContext)
        {
            _activityService = activityService;
            _bookingDbContext = bookingDbContext;
        }
        
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            // var activity = new Activity
            // {
            //     Name = "Sauna",
            //     Price = 100,
            //     Description = "Test",
            // };
            //
            // await _bookingContext.Activities.AddAsync(activity);
            // await _bookingContext.SaveChangesAsync();
            //
            // var result = await _bookingContext.Activities.FindAsync(activity.Id);
            return Ok(GetOpeningHours(12, 40));
        }
        
        // helper methods
        private IEnumerable<string> GetOpeningHours(int openTime, int closeTime)
        {
            var times = new[]
            {
                "00:00", "00:30", "01:00", "01:30", "02:00", "02:30", "03:00", "03:30", "04:00", "04:30", "05:00",
                "05:30", "06:00", "06:30", "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30",
                "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00",
                "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00", "21:30",
                "22:00", "22:30", "23:00", "23:30", "24:00"
            };

            var afterOpenHours = times.Skip(openTime);
            var openingHours = afterOpenHours.Take(times.Length - openTime - (times.Length - 1 - closeTime));
            return openingHours;
        }
    }
}