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
    public class PaymentsController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public PaymentsController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            var Payment = await _context.Payment
          .Include(P => P.Customer)
          .Include(P => P.Booking)
          .Include(P => P.Ticket)
          .ToListAsync();

            if (Payment == null)
          {
              return NotFound();
          }
            return await _context.Payment.ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int? id)
        {
          if (_context.Payment == null)
          {
              return NotFound();
          }
            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.Booking)
                .Include(p => p.Ticket)
                .FirstOrDefaultAsync(p => p.payment_id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int? id, Payment payment)
        {
            if (id != payment.payment_id)
            {
                return BadRequest();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            payment.Customer = null; // Xóa thông tin về Customer
            payment.Booking = null;// Xóa thông tin về Booking
            payment.Ticket = null;// Xóa thông tin về Ticket

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
          if (_context.Payment == null)
          {
              return Problem("Entity set 'AirlineDbContext.Payment'  is null.");
          }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            payment.Customer = null; // Xóa thông tin về Customer
            payment.Booking = null;// Xóa thông tin về Booking
            payment.Ticket = null;// Xóa thông tin về Ticket

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.payment_id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int? id)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            payment.Customer = null; // Xóa thông tin về Customer
            payment.Booking = null;// Xóa thông tin về Booking
            payment.Ticket = null;// Xóa thông tin về Ticket

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int? id)
        {
            return (_context.Payment?.Any(e => e.payment_id == id)).GetValueOrDefault();
        }
    }
}
