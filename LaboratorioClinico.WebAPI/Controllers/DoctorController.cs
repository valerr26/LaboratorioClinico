using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratorioClinico.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;


        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctor/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            var doctor = await _doctorService.ObtenerDoctoresActivosAsync();
            return Ok(doctor);
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            try
            {
                var doctor = await _doctorService.ObtenerDoctorPorIdAsync(id);

                if (doctor == null)
                    return NotFound($"No se encontró un Doctor activo con ID {id}");

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<DoctorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _doctorService.AgregarDoctorAsync(doctor);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);

        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Doctor doctor)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                doctor.Id = id; // nos aseguramos de que use el id de la ruta

                var resultado = await _doctorService.ModificarDoctorAsync(doctor);

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

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
