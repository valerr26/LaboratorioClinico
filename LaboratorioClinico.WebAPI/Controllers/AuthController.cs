using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            var (ok, msg) = await _authService.RegisterAsync(usuario.Username, usuario.Password!, usuario.IdRol);
            if (!ok) return BadRequest(new { message = msg });
            return Ok(new { message = msg });
        }

        // 🔐 Login con username
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Username) || string.IsNullOrWhiteSpace(usuario.Password))
                return BadRequest(new { message = "Debe ingresar nombre de usuario y contraseña." });

            var (ok, tokenOrMsg) = await _authService.LoginAsync(usuario.Username, usuario.Password!);

            if (!ok)
                return Unauthorized(new { message = tokenOrMsg });

            return Ok(new { token = tokenOrMsg });
        }

        // 👤 Endpoint para ver información del usuario autenticado
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }
    }
}