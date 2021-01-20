using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Contexts;
using api.Contracts.Requests;
using api.Contracts.Responses;
using api.DTOs;
using api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace api.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// Create a new booking
        /// </summary>
        Task<ApiResponse> Create(BookingRequest model);

        /// <summary>
        /// Get all bookings in given week
        /// </summary>
        Task<BookingResponse> GetBookingsWeek(BookingsWeekRequest model);

        /// <summary>
        /// Get all bookings in given day
        /// </summary>
        Task<BookingResponse> GetBookingsDay(DateTime date);

        /// <summary>
        /// Get all future bookings for current user
        /// </summary>
        Task<BookingResponse> GetFutureBookings(ClaimsPrincipal userPrincipal);
    }

    public class BookingService : IBookingService
    {
        private readonly BookingDbContext _bookingDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public BookingService(BookingDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _bookingDbContext = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Create(BookingRequest model)
        {
            var user = await _userManager.GetUserAsync(model.UserPrincipal);
            var booking = new Booking
                {Start = model.Start, End = model.End, ActivityId = model.ActivityId, UserId = user.Id};
            await _bookingDbContext.Bookings.AddAsync(booking);
            await _bookingDbContext.SaveChangesAsync();

            return new ApiResponse(true, 201);
        }

        public async Task<BookingResponse> GetBookingsWeek(BookingsWeekRequest model)
        {
            var date = DateTime.Now;
            // check if year is in the before or more than 1 year after current year
            if (model.Year < date.Year || model.Year > date.Year + 1)
                return new BookingResponse(400, "Year must be current or next year");

            // check if week is before current week
            if (model.Year == date.Year && model.Week < ISOWeek.GetWeekOfYear(date))
                return new BookingResponse(400, "Week must be in the future");

            // check if week is less than 1
            if (model.Week < 1) return new BookingResponse(400, "Week must be at least 1");
            var numberOfWeeks = ISOWeek.GetWeeksInYear(model.Year);
            // check if week is more than number of weeks in given year
            if (model.Week > numberOfWeeks)
                return new BookingResponse(400, "Week must be at most " + numberOfWeeks);

            var from = ISOWeek.ToDateTime(model.Year, model.Week, DayOfWeek.Monday);
            var nextWeek = model.Week == numberOfWeeks ? 1 : model.Week + 1;
            var newYear = model.Week == numberOfWeeks ? model.Year + 1 : model.Year;
            var to = ISOWeek.ToDateTime(newYear, nextWeek, DayOfWeek.Monday);

            var bookings = await _bookingDbContext.Bookings.Where(b => b.Start >= from && b.Start < to)
                .Select(b => _mapper.Map<PublicBookingsDto>(b)).ToListAsync();
            return new BookingResponse(bookings);
        }

        public async Task<BookingResponse> GetBookingsDay(DateTime date)
        {
            var today = DateTime.Today;
            // check if date is in the past
            if (date < today) return new BookingResponse(400, "Date must be in the future");

            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            var from = new DateTime(year, month, day);
            if (month == 12)
            {
                year++;
                month = 1;
                day = 1;
            }
            else if (day == DateTime.DaysInMonth(year, month))
            {
                month++;
                day = 1;
            }
            else day++;

            var to = new DateTime(year, month, day);

            var bookings = await _bookingDbContext.Bookings
                .Where(booking => booking.Start >= from && booking.Start < to)
                .Select(booking => _mapper.Map<PublicBookingsDto>(booking)).ToListAsync();

            return new BookingResponse(bookings);
        }

        public async Task<BookingResponse> GetFutureBookings(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            var now = DateTime.Now;
            var bookings = await _bookingDbContext.Bookings
                .Where(booking => booking.UserId == user.Id && booking.Start >= now)
                .Select(booking => _mapper.Map<PrivateBookingsDto>(booking)).ToListAsync();

            return new BookingResponse(bookings);
        }
    }
}