using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Project__nhom3.Data;
using Project__nhom3.Models;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Project__nhom3.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AirlineDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AirlineDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var existingUser = await _dbContext.Customer.FirstOrDefaultAsync(u => u.email == model.email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already registered.");
                }

                var hashedPassword = HashPassword(model.password);

                var newUser = new Customer
                {
                    first_name = model.first_name,
                    last_name = model.last_name,
                    date_of_birth = model.date_of_birth,
                    email = model.email,
                    address = model.address,
                    phone_number = model.phone_number,
                    username = model.username,
                    password = hashedPassword,
                    sex = model.sex
                };

                _dbContext.Customer.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _dbContext.Customer.FirstOrDefaultAsync(u => u.username == model.username);
                if (user == null || !VerifyPassword(model.password, user.password))
                {
                    return BadRequest("Invalid username or password.");
                }

                return Ok(new { user.customer_id });
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Perform any necessary actions for logging out
            return Ok("Logged out successfully.");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value; // Lấy tên người dùng từ JWT
                var user = await _dbContext.Customer.FirstOrDefaultAsync(u => u.username == username);
                if (user == null || !VerifyPassword(model.CurrentPassword, user.password))
                {
                    return BadRequest("Invalid current password.");
                }

                var hashedNewPassword = HashPassword(model.NewPassword);
                user.password = hashedNewPassword;

                await _dbContext.SaveChangesAsync();
                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Password change failed: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            // Sử dụng BCrypt để hash mật khẩu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            // Xác minh mật khẩu bằng cách so sánh mật khẩu đã nhập với mật khẩu đã hash
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
            return isPasswordValid;
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];
            byte[] bytes = Encoding.UTF8.GetBytes(jwtKey);
            var key = bytes;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
