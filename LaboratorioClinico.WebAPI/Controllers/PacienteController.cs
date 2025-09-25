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
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _pacienteService;


        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: api/PacienteController/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> Get()
        {
            var paciente = await _pacienteService.ObtenerPacientesActivosAsync();
            return Ok(paciente);
        }

        // GET api/<PacienteController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetById(int id)
        {
            try
            {
                var paciente = await _pacienteService.ObtenerPacientePorIdAsync(id);

                if (paciente == null)
                    return NotFound($"No se encontró un Paciente activo con ID {id}");

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<PacienteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Paciente paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _pacienteService.AgregarPacienteAsync(paciente);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);

        }
        // PUT api/<PacienteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Paciente paciente)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                paciente.Id = id; // nos aseguramos de que use el id de la ruta

                var resultado = await _pacienteService.ModificarPacienteAsync(paciente);

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

        // DELETE api/<PacienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
