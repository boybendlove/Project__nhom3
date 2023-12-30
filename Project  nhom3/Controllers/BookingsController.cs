using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project__nhom3.Data;
using Project__nhom3.Models;

namespace Project__nhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public BookingsController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            var Booking = await _context.Booking
        .Include(b => b.Customer)
        .Include(b => b.Ticket)
        .ToListAsync();
            if (Booking == null)
          {
              return NotFound();
          }
            return Ok(Booking);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int? id)
        {
          if (_context.Booking == null)
          {
              return NotFound();
          }
            var booking = await _context.Booking
                .Include(b => b.Customer)
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(b => b.booking_id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int? id, Booking booking)
        {
            if (id != booking.booking_id)
            {
                return BadRequest();
            }
            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            booking.Customer = null; // Xóa thông tin về Customer
            booking.Ticket = null; // Xóa thông tin về Ticket

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingDto bookingDto)
        {
          if (_context.Booking == null)
          {
              return Problem("Entity set 'AirlineDbContext.Booking'  is null.");
          }

            var booking = new Booking
            {
                ticket_id = bookingDto.ticket_id,
                customer_id = bookingDto.customer_id,
                time_order = bookingDto.time_order,
                booking_status = bookingDto.booking_status,
            };
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.booking_id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int? id)
        {
            if (_context.Booking == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            booking.Customer = null; // Xóa thông tin về Customer
            booking.Ticket = null; // Xóa thông tin về Ticket
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int? id)
        {
            return (_context.Booking?.Any(e => e.booking_id == id)).GetValueOrDefault();
        }
    }
}
