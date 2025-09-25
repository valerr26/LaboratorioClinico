using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;


        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> Get()
        {
            var rol = await _usuarioService.ObtenerUsuariosActivosAsync();
            return Ok(rol);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var rol = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

                if (rol == null)
                    return NotFound($"No se encontró un Usuario activo con ID {id}");

                return Ok(rol);
            }
            catch (Exception ex)
            {
                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _usuarioService.AgregarUsuarioAsync(usuario);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                usuario.Id = id; // nos aseguramos de que use el id de la ruta

                var resultado = await _usuarioService.ModificarUsuarioAsync(usuario);

                if (resultado.StartsWith("Error"))
                    return BadRequest(resultado);

                return Ok(resultado);


            }
            catch (Exception ex)
            {
                // Registrar log aquí si tienes ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
