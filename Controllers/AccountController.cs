using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarbeariaLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // Assuming you have a service for handling authentication
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);
            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new { Token = result.Token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Assuming you handle logout in your service
            _authService.Logout();
            return Ok(new { Message = "Logout successful" });
        }
    
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}