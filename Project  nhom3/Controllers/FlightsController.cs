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
    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public FlightsController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlight()
        {
            var Flight = await _context.Flight
          .Include(f => f.Airplane)
          .Include(f => f.Location)
          .Include(f => f.Airplane.Seat)
          .ToListAsync();

            if (Flight == null)
            {
                return NotFound();
            }

            return Ok(Flight);
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int? id)
        {
            if (_context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .Include(f => f.Airplane)
                .Include(f => f.Location)
                .Include(f => f.Airplane.Seat)
                .FirstOrDefaultAsync(f => f.flight_id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int? id, Flight flight)
        {
            if (id != flight.flight_id)
            {
                return BadRequest();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            flight.Airplane = null; // Xóa thông tin về airplane
            flight.Location = null; // Xóa thông tin về location

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            if (_context.Flight == null)
            {
                return Problem("Entity set 'AirlineDbContext.Flight' is null.");
            }

            // Xóa các thông tin không cần thiết trước khi thêm vào cơ sở dữ liệu
            flight.Airplane = null; // Xóa thông tin về airplane
            flight.Location = null; // Xóa thông tin về location

            _context.Flight.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.flight_id }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int? id)
        {
            if (_context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            // Xóa các thông tin không cần thiết trước khi xóa khỏi cơ sở dữ liệu
            flight.Airplane = null; // Xóa thông tin về airplane
            flight.Location = null; // Xóa thông tin về location

            _context.Flight.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int? id)
        {
            return (_context.Flight?.Any(e => e.flight_id == id)).GetValueOrDefault();
        }
    }
}
