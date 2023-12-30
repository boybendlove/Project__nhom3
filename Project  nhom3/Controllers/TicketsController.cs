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
    public class TicketsController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public TicketsController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            var Ticket = await _context.Ticket
          .Include(t => t.Customer)
          .Include(t => t.Passenger)
          .Include(t => t.Flight)
          .Include(t => t.Seat)
          .ToListAsync();

            if (Ticket == null)
          {
              return NotFound();
          }
            return await _context.Ticket.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int? id)
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            var ticket = await _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Passenger)
                .Include(t => t.Flight)
                .Include(t => t.Seat)
                .FirstOrDefaultAsync(t => t.ticket_id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int? id, Ticket ticket)
        {
            if (id != ticket.ticket_id)
            {
                return BadRequest();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            ticket.Customer = null; // Xóa thông tin về Customer
            ticket.Flight = null;// Xóa thông tin về Flight
            ticket.Seat = null;// Xóa thông tin về Seat
            ticket.Passenger = null;// Xóa thông tin về Passenger

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketDto ticketDto)
        {
          if (_context.Ticket == null)
          {
              return Problem("Entity set 'AirlineDbContext.Ticket'  is null.");
          }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            var ticket = new Ticket {
                flight_id = ticketDto.flight_id,
                customer_id = ticketDto.customer_id,
                passenger_id = ticketDto.passenger_id,
                seat_id = ticketDto.seat_id,
                create_at = ticketDto.create_at,
                update_at = ticketDto.update_at,
                departure_time = ticketDto.departure_time,
                arrival_time = ticketDto.arrival_time,
                real_time_flight = ticketDto.real_time_flight,
                ticket_status = ticketDto.ticket_status,
                price = ticketDto.price,
                Flag = ticketDto.Flag
            };

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.ticket_id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int? id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            ticket.Customer = null; // Xóa thông tin về Customer
            ticket.Flight = null;// Xóa thông tin về Flight
            ticket.Seat = null;// Xóa thông tin về Seat
            ticket.Passenger = null;// Xóa thông tin về Passenger

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int? id)
        {
            return (_context.Ticket?.Any(e => e.ticket_id == id)).GetValueOrDefault();
        }
    }
}
