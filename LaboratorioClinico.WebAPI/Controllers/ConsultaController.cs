using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/consulta")]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultaController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        // GET: api/consulta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> Get()
        {
            var consultas = await _consultaService.ObtenerConsultasActivasAsync();
            return Ok(consultas);
        }

        // GET api/consulta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetById(int id)
        {
            var consulta = await _consultaService.ObtenerConsultaPorIdAsync(id);

            if (consulta == null)
                return NotFound($"No se encontró una Consulta activa con ID {id}");

            return Ok(consulta);
        }

        // POST api/consulta
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Consulta consulta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            consulta.Estado = "Activo";

            var resultado = await _consultaService.AgregarConsultaAsync(consulta);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // PUT api/consulta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Consulta consulta)
        {
            consulta.Id = id; // asegurar el ID de la ruta

            var resultado = await _consultaService.ModificarConsultaAsync(consulta);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // DELETE api/consulta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Eliminación lógica: Estado = false
            var resultado = await _consultaService.EliminarConsultaAsync(id);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}

