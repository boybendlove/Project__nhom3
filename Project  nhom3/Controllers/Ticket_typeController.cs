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
    public class Ticket_typeController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public Ticket_typeController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Ticket_type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket_type>>> GetTicket_type()
        {
          if (_context.Ticket_type == null)
          {
              return NotFound();
          }
            return await _context.Ticket_type.ToListAsync();
        }

        // GET: api/Ticket_type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket_type>> GetTicket_type(int id)
        {
          if (_context.Ticket_type == null)
          {
              return NotFound();
          }
            var ticket_type = await _context.Ticket_type.FindAsync(id);

            if (ticket_type == null)
            {
                return NotFound();
            }

            return ticket_type;
        }

        // PUT: api/Ticket_type/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket_type(int id, Ticket_type ticket_type)
        {
            if (id != ticket_type.ticket_type_id)
            {
                return BadRequest();
            }

            _context.Entry(ticket_type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ticket_typeExists(id))
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

        // POST: api/Ticket_type
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket_type>> PostTicket_type(Ticket_type ticket_type)
        {
          if (_context.Ticket_type == null)
          {
              return Problem("Entity set 'AirlineDbContext.Ticket_type'  is null.");
          }
            _context.Ticket_type.Add(ticket_type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket_type", new { id = ticket_type.ticket_type_id }, ticket_type);
        }

        // DELETE: api/Ticket_type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket_type(int id)
        {
            if (_context.Ticket_type == null)
            {
                return NotFound();
            }
            var ticket_type = await _context.Ticket_type.FindAsync(id);
            if (ticket_type == null)
            {
                return NotFound();
            }

            _context.Ticket_type.Remove(ticket_type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Ticket_typeExists(int id)
        {
            return (_context.Ticket_type?.Any(e => e.ticket_type_id == id)).GetValueOrDefault();
        }
    }
}
