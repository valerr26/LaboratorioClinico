using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/examen")]
    public class ExamenController : ControllerBase
    {
        private readonly ExamenService _examenService;

        public ExamenController(ExamenService examenService)
        {
            _examenService = examenService;
        }

        // GET: api/examen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examen>>> Get()
        {
            var examen = await _examenService.ObtenerExamenesActivosAsync();
            return Ok(examen);
        }

        // GET api/examen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examen>> GetById(int id)
        {
            var examen = await _examenService.ObtenerExamenPorIdAsync(id);

            if (examen == null)
                return NotFound($"No se encontró un Examen activo con ID {id}");

            return Ok(examen);
        }

        // POST api/examen
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Examen examen)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ahora Estado es string → asigna "Pendiente" si viene vacío
            if (string.IsNullOrWhiteSpace(examen.Estado))
                examen.Estado = "Pendiente";

            var resultado = await _examenService.AgregarExamenAsync(examen);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // PUT api/examen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Examen examen)
        {
            examen.Id = id; // asegurar que usa el ID de la ruta

            var resultado = await _examenService.ModificarExamenAsync(examen);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // DELETE api/examen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // DELETE ahora hace eliminación lógica: Estado = "Cancelado"
            var resultado = await _examenService.EliminarExamenAsync(id);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
