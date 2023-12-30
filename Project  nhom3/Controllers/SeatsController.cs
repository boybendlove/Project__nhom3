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
    public class SeatsController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public SeatsController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeat()
        {
          if (_context.Seat == null)
          {
              return NotFound();
          }
            return await _context.Seat.ToListAsync();
        }

        // GET: api/Seats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int? id)
        {
          if (_context.Seat == null)
          {
              return NotFound();
          }
            var seat = await _context.Seat.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        // PUT: api/Seats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat(int? id, Seat seat)
        {
            if (id != seat.seat_id)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostSeat(Seat seat)
        {
          if (_context.Seat == null)
          {
              return Problem("Entity set 'AirlineDbContext.Seat'  is null.");
          }
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeat", new { id = seat.seat_id }, seat);
        }

        // DELETE: api/Seats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int? id)
        {
            if (_context.Seat == null)
            {
                return NotFound();
            }
            var seat = await _context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatExists(int? id)
        {
            return (_context.Seat?.Any(e => e.seat_id == id)).GetValueOrDefault();
        }
    }
}
