using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/rol")]
    public class RolController : ControllerBase
    {
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        // GET api/rol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> Get()
        {
            var roles = await _rolService.ObtenerRolesActivosAsync();
            return Ok(roles);
        }

        // GET api/rol/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetById(int id)
        {
            var rol = await _rolService.ObtenerRolPorIdAsync(id);

            if (rol == null)
                return NotFound($"No se encontró un Rol ACTIVO con ID {id}");

            return Ok(rol);
        }

        // POST api/rol
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Siempre crear el rol como ACTIVO
            rol.Estado = "ACTIVO";

            var resultado = await _rolService.AgregarRolAsync(rol);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok("Rol agregado correctamente");
        }

        // PUT api/rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rol rol)
        {
            rol.Id = id; // Asegurar que se usa el ID de la ruta

            var resultado = await _rolService.ModificarRolAsync(rol);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok("Rol modificado correctamente");
        }

        // DELETE api/rol/5  → Borrado lógico
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _rolService.EliminarRolAsync(id);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok("Rol desactivado correctamente");
        }
    }
}
