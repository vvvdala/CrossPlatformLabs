using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using System;

namespace MyApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookingStatusController : Controller
    {
        private readonly AppDbContext _context;
        public BookingStatusController(AppDbContext context)
        {
            _context = context;
        }


        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingStatus>>> GetBookingStatuesV1()
        {
            var statuses = await _context.BookingStatuses.ToListAsync();
            return Ok(statuses);
        }


        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingStatus>>> GetBookingStatuesV2()
        {
            var statuses = await _context.BookingStatuses
                .Select(b => new
                {
                    b.BookingStatusCode,
                })
                .ToListAsync();
            return Ok(statuses);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingStatus>> GetBookingStatusV1(string id)
        {
            var bookingStatus = await _context.BookingStatuses.FirstOrDefaultAsync(b => b.BookingStatusCode == id);
            return Ok(bookingStatus);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingStatus>> GetBookingStatusV2(string id)
        {
            var bookingStatus = await _context.BookingStatuses
                .Where(b => b.BookingStatusCode == id)
                .Select(b => new
                {
                    b.BookingStatusDescription,
                    CreatedDate = DateTime.UtcNow
                })
                .FirstOrDefaultAsync();
            return Ok(bookingStatus);
        }


    }
}
