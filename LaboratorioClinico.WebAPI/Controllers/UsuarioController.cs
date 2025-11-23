using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosActivosAsync();
            return Ok(usuarios);
        }

        // GET api/usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

                if (usuario == null)
                    return NotFound($"No se encontró un Usuario activo con ID {id}");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/usuario
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            usuario.Estado = "ACTIVO";  // 👈 por defecto

            var resultado = await _usuarioService.AgregarUsuarioAsync(usuario);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // PUT api/usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                usuario.Id = id;

                var resultado = await _usuarioService.ModificarUsuarioAsync(usuario);

                if (resultado.StartsWith("Error"))
                    return BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE api/usuario/5 (borrado lógico)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _usuarioService.EliminarUsuarioAsync(id);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
