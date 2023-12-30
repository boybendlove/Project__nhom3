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
    public class AirplanesController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public AirplanesController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Airplanes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airplane>>> GetAirplane()
        {
            var Airplane = await _context.Airplane
          .Include(f => f.Seat)
          .ToListAsync();
            if (Airplane == null)
          {
              return NotFound();
          }
            return Ok(Airplane);
        }

        // GET: api/Airplanes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airplane>> GetAirplane(int? id)
        {
          if (_context.Airplane == null)
          {
              return NotFound();
          }
            var airplane = await _context.Airplane
               .Include(a => a.Seat)
               .FirstOrDefaultAsync(a => a.airplane_id == id);

            if (airplane == null)
            {
                return NotFound();
            }

            return airplane;
        }

        // PUT: api/Airplanes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirplane(int? id, Airplane airplane)
        {
            if (id != airplane.airplane_id)
            {
                return BadRequest();
            }
            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            airplane.Seat = null; // Xóa thông tin về Seat

            _context.Entry(airplane).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirplaneExists(id))
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

        // POST: api/Airplanes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Airplane>> PostAirplane(Airplane airplane)
        {
          if (_context.Airplane == null)
          {
              return Problem("Entity set 'AirlineDbContext.Airplane'  is null.");
          }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            airplane.Seat = null; // Xóa thông tin về Seat

            _context.Airplane.Add(airplane);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirplane", new { id = airplane.airplane_id }, airplane);
        }

        // DELETE: api/Airplanes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirplane(int? id)
        {
            if (_context.Airplane == null)
            {
                return NotFound();
            }
            var airplane = await _context.Airplane.FindAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            airplane.Seat = null; // Xóa thông tin về Seat

            _context.Airplane.Remove(airplane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirplaneExists(int? id)
        {
            return (_context.Airplane?.Any(e => e.airplane_id == id)).GetValueOrDefault();
        }
    }
}
