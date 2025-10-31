using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/Examen/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examen>>> Get()
        {
            var examen = await _examenService.ObtenerExamenesActivosAsync();
            return Ok(examen);
        }

        // GET api/<ExamenController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examen>> GetById(int id)
        {
            try
            {
                var examen = await _examenService.ObtenerExamenPorIdAsync(id);

                if (examen == null)
                    return NotFound($"No se encontró un Examen activo con ID {id}");

                return Ok(examen);
            }
            catch (Exception ex)
            {
                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<ExamenController>
        [HttpPost]
        public async Task<IActionResult> Post(Examen examen)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _examenService.AgregarExamenAsync(examen);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);

        }

        // PUT api/<ExamenController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Examen examen)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                examen.Id = id; // nos aseguramos de que use el id de la ruta

                var resultado = await _examenService.ModificarExamenAsync(examen);

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

        // DELETE api/<ExamenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
