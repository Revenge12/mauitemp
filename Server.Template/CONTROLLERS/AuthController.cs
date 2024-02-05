using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using $safeprojectname$.Services;
using Shared.Template.AuthModels;

namespace $safeprojectname$.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Registration user)
        {
            var response = await _authService.Register(user);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var response = await _authService.Login(user);

            return Ok(response);
        }

    }
}
