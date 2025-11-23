using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cita")]
    public class CitaController : ControllerBase
    {
        private readonly CitaService _citaService;

        public CitaController(CitaService citaService)
        {
            _citaService = citaService;
        }

        // -------------------------------------------------------
        // GET: api/cita  → Lista solo citas ACTIVAS (usa CitaService)
        // -------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> Get()
        {
            var citas = await _citaService.ObtenerCitasActivasAsync();
            return Ok(citas);
        }

        // -------------------------------------------------------
        // GET: api/cita/5 → Obtener CITA SOLO SI ESTÁ ACTIVA
        // -------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetById(int id)
        {
            var cita = await _citaService.ObtenerCitaPorIdAsync(id);

            if (cita == null)
                return NotFound($"No se encontró una cita activa con ID {id}");

            return Ok(cita);
        }

        // -------------------------------------------------------
        // POST: api/cita → Crear CITA
        // -------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cita cita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _citaService.AgregarCitaAsync(cita);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // -------------------------------------------------------
        // PUT: api/cita/5 → Modificar CITA
        // -------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cita cita)
        {
            cita.Id = id; // Se asegura que el ID sea el de la URL

            var resultado = await _citaService.ModificarCitaAsync(cita);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // -------------------------------------------------------
        // DELETE: API NO ELIMINA CITAS (solo cambia estado)
        // -------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return BadRequest("No se puede eliminar una cita. Solo cambiar estado.");
        }
    }
}
