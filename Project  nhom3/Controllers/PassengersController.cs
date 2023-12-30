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
    public class PassengersController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public PassengersController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: api/Passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassenger()
        {
            var Passenger = await _context.Passenger
          .Include(p => p.Customer)
          .ToListAsync();
            if (Passenger == null)
          {
              return NotFound();
          }
            return Ok(Passenger);
        }

        // GET: api/Passengers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int? id)
        {
          if (_context.Passenger == null)
          {
              return NotFound();
          }
            var passenger = await _context.Passenger
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.passenger_id == id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int? id, Passenger passenger)
        {
            if (id != passenger.passenger_id)
            {
                return BadRequest();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            passenger.Customer = null; // Xóa thông tin về Customer

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
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

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(PassengerDto passengerDto)
        {
            // Kiểm tra xác thực và xử lý lưu trữ dữ liệu trong cơ sở dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tạo một đối tượng Passenger từ DTO
            var passenger = new Passenger
            {
                customer_id = passengerDto.customer_id,
                first_name = passengerDto.first_name,
                last_name = passengerDto.last_name,
                address = passengerDto.address,
                phone_number = passengerDto.phone_number,
                email = passengerDto.email,
                sex = passengerDto.sex,
                date_of_birth = passengerDto.date_of_birth,
                Flag = passengerDto.Flag
            };

            _context.Passenger.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = passenger.passenger_id }, passenger);
        }

        // DELETE: api/Passengers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int? id)
        {
            if (_context.Passenger == null)
            {
                return NotFound();
            }
            var passenger = await _context.Passenger.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            // Xóa các thông tin không cần thiết trước khi cập nhật vào cơ sở dữ liệu
            passenger.Customer = null; // Xóa thông tin về Customer

            _context.Passenger.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerExists(int? id)
        {
            return (_context.Passenger?.Any(e => e.passenger_id == id)).GetValueOrDefault();
        }
    }
}
