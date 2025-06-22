using Microsoft.AspNetCore.Mvc;
using Models.Client.DTOs;
using ServiceCore.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
