using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        // GET: api/Cita/get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> Get()
        {
            var cita = await _citaService.ObtenerCitasActivasAsync ();
            return Ok(cita );
        }

        // GET api/<CitaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetById(int id)
        {
            try
            {
                var cita = await _citaService.ObtenerCitaPorIdAsync(id);

                if (cita == null)
                    return NotFound($"No se encontró una Cita activa con ID {id}");

                return Ok(cita);

            }
            catch (Exception ex)
            {

                //Aquí podrías registrar el error con ILogger
                return StatusCode(500, $"Error interno del servidor: {ex.Message}"); 
            }
        }

        // POST api/<CitaController>
        [HttpPost]
        public async Task<IActionResult> Post(Cita  cita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _citaService.AgregarCitaAsync(cita);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);

            
        }

        // PUT api/<CitaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cita  cita)
        {
            try
            {
                // El servicio valida si el id es válido o no coincide, no lo hacemos aquí
                cita.Id = id; // nos aseguramos de que use el id de la ruta

                var resultado = await _citaService.ModificarCitaAsync(cita);

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

        // DELETE api/<CitaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
