using InvoiceApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InvoiceApi.Services;

namespace InvoiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.Login(dto);
            if (!user.IsSuccess)
                return Unauthorized();


            return Ok(new
            {
                message = user.Message,
                token = user.Token
            });
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {

            var registerResult = await _authService.Register(dto);

            if (!registerResult.IsSuccess)
            {
                return BadRequest(new
                {
                    message = registerResult.Message
                });
            }

            return Ok(new
            {
                message = registerResult.Message,
            });

        }

    }
}
