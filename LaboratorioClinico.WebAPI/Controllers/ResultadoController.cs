using LaboratorioClinico.Application.Services;
using LaboratorioClinico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/resultado")]
    public class ResultadoController : ControllerBase
    {
        private readonly ResultadoService _resultadoService;

        public ResultadoController(ResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }

        // GET: api/resultado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resultado>>> Get()
        {
            var resultado = await _resultadoService.ObtenerResultadosActivosAsync();
            return Ok(resultado);
        }

        // GET api/resultado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resultado>> GetById(int id)
        {
            try
            {
                var resultado = await _resultadoService.ObtenerResultadoPorIdAsync(id);

                if (resultado == null)
                    return NotFound($"No se encontró un Resultado activo con ID {id}");

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/resultado
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Resultado resultado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mensaje = await _resultadoService.AgregarResultadoAsync(resultado);

            if (mensaje.StartsWith("Error"))
                return BadRequest(mensaje);

            return Ok(mensaje);
        }

        // PUT api/resultado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Resultado resultado)
        {
            try
            {
                // nos aseguramos de que use el ID de la ruta
                resultado.Id = id;

                var mensaje = await _resultadoService.ModificarResultadoAsync(resultado);

                if (mensaje.StartsWith("Error"))
                    return BadRequest(mensaje);

                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE api/resultado/5  (Eliminación lógica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mensaje = await _resultadoService.EliminarResultadoAsync(id);

            if (mensaje.StartsWith("Error"))
                return BadRequest(mensaje);

            return Ok(mensaje);
        }
    }
}
