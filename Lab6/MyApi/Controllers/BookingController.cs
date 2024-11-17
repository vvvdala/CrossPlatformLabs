using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using System.Runtime.InteropServices;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "FLE Standard Time" : "Europe/Kyiv";
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            var bookings = await _context.Bookings
                            .Include(b => b.BookingStatus)
                            .Include(b => b.Customer)
                            .Include(b => b.Vehicle)
                            .Select(b => new
                            {
                                b.BookingId,
                                b.BookingStatus,
                                DateFrom = TimeZoneInfo.ConvertTimeFromUtc(b.DateFrom, timeZone),
                                DateTo = TimeZoneInfo.ConvertTimeFromUtc(b.DateTo, timeZone)
                            })
                            .ToListAsync();

            return Ok(bookings);
        }

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<object>> GetBooking(int bookingId)
        {
            var booking = await _context.Bookings
                .Where(b => b.BookingId == bookingId)
                .Select(b => new
                {
                    b.BookingId,
                    CustomerName = b.Customer.CustomerName,
                    VehicleRegNumber = b.Vehicle.RegNumber,
                    StatusCode = b.BookingStatus.BookingStatusCode,
                    StatusDescr = b.BookingStatus.BookingStatusDescription,
                })
                .FirstOrDefaultAsync();

            return Ok(booking);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchBookings(
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] List<string> statusCodes,
            [FromQuery] string? startVehicleReg = null,
            [FromQuery] string? endVehicleReg = null)
        {

            var query = _context.Bookings
                       .Include(b => b.BookingStatus)
                       .Include(b => b.Customer)
                       .Include(b => b.Vehicle)
                       .AsQueryable();

            if(dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(b => b.DateFrom >= dateFrom && b.DateTo <= dateTo);
            }

            if (statusCodes != null && statusCodes.Any())
            {
                query = query.Where(b => statusCodes.Contains(b.BookingStatus.BookingStatusCode));
            }

            if (!String.IsNullOrEmpty(startVehicleReg))
            {
                query = query.Where(b => b.Vehicle.RegNumber.StartsWith(startVehicleReg));
            }

            if (!String.IsNullOrEmpty(endVehicleReg))
            {
                query = query.Where(b => b.Vehicle.RegNumber.EndsWith(endVehicleReg));
            }

            var results = await query.Select(b => new
            {
                b.BookingId,
                CustomerName = b.Customer.CustomerName,
                VehicleRegNumber = b.Vehicle.RegNumber,
                StatusCode = b.BookingStatus.BookingStatusCode,
                StatusDescr = b.BookingStatus.BookingStatusDescription,
                b.DateFrom,
                b.DateTo
            }).ToListAsync();

            return Ok(results);
        }

    }
}
