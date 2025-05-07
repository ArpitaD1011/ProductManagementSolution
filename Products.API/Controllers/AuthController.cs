using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Services;

namespace Products.API.Controllers
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
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if(loginDto.UserName == "admin" && loginDto.Password=="password")
            {
                var token = _authService.GenerateJWTToken(loginDto.UserName, "Admin");
                return Ok(new {Token = token}); 
            }
            return Unauthorized();
        }
    }
}
