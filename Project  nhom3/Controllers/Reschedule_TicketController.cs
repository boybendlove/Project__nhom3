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
    public class Reschedule_TicketController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public Reschedule_TicketController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Reschedule_Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reschedule_Ticket>>> GetReschedule_Ticket()
        {
          if (_context.Reschedule_Ticket == null)
          {
              return NotFound();
          }
            return await _context.Reschedule_Ticket.ToListAsync();
        }

        // GET: api/Reschedule_Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reschedule_Ticket>> GetReschedule_Ticket(int? id)
        {
          if (_context.Reschedule_Ticket == null)
          {
              return NotFound();
          }
            var reschedule_Ticket = await _context.Reschedule_Ticket.FindAsync(id);

            if (reschedule_Ticket == null)
            {
                return NotFound();
            }

            return reschedule_Ticket;
        }

        // PUT: api/Reschedule_Ticket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReschedule_Ticket(int? id, Reschedule_Ticket reschedule_Ticket)
        {
            if (id != reschedule_Ticket.reschedule_ticket_id)
            {
                return BadRequest();
            }

            _context.Entry(reschedule_Ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Reschedule_TicketExists(id))
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

        // POST: api/Reschedule_Ticket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reschedule_Ticket>> PostReschedule_Ticket(Reschedule_Ticket reschedule_Ticket)
        {
          if (_context.Reschedule_Ticket == null)
          {
              return Problem("Entity set 'AirlineDbContext.Reschedule_Ticket'  is null.");
          }
            _context.Reschedule_Ticket.Add(reschedule_Ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReschedule_Ticket", new { id = reschedule_Ticket.reschedule_ticket_id }, reschedule_Ticket);
        }

        // DELETE: api/Reschedule_Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReschedule_Ticket(int? id)
        {
            if (_context.Reschedule_Ticket == null)
            {
                return NotFound();
            }
            var reschedule_Ticket = await _context.Reschedule_Ticket.FindAsync(id);
            if (reschedule_Ticket == null)
            {
                return NotFound();
            }

            _context.Reschedule_Ticket.Remove(reschedule_Ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Reschedule_TicketExists(int? id)
        {
            return (_context.Reschedule_Ticket?.Any(e => e.reschedule_ticket_id == id)).GetValueOrDefault();
        }
    }
}
