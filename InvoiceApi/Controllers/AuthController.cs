using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Helpers;
using InvoiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName
                                                         && u.Password == dto.Password);
            if (user == null)
                return Unauthorized();
            var token = JwtHelper.GenerateToken(user.UserName, user.Id);

            return Ok(new { token });
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var UserExists = await _appDbContext.Users.AnyAsync(u => u.UserName == dto.UserName);

            if (UserExists)
            {
                return BadRequest("User already exists!");
            }
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return Ok(new { message = "User Create!" });

        }

    }
}
