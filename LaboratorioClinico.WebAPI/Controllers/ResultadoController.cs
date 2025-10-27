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
    [Route("api/resultado")]
    public class ResultadoController : ControllerBase
    {
        private readonly ResultadoService _resultadoService;


        public ResultadoController(ResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }

        // GET: api/ResultadoController/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> Get()
        {
            var resultado = await _resultadoService.ObtenerResultadosActivosAsync();
            return Ok(resultado);
        }

        // GET api/<ResultadoController>/5
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
                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<ResultadoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Resultado resultado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultados = await _resultadoService.AgregarResultadoAsync(resultado);

            if (resultados.StartsWith("Error"))
                return BadRequest(resultados);

            return Ok(resultados);

        }

        // PUT api/<ResultadoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Resultado resultado)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                resultado.Id = id; // nos aseguramos de que use el id de la ruta

                var resultados = await _resultadoService.ModificarResultadoAsync(resultado);

                if (resultados.StartsWith("Error"))
                    return BadRequest(resultados);

                return Ok(resultados);


            }
            catch (Exception ex)
            {
                // Registrar log aquí si tienes ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE api/<ResultadoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
