using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;

        public CitaService(ICitaRepository repository)
        {
            _repository = repository;
        }

        // Caso de uso: obtener todas las citas activas
        public async Task<IEnumerable<Cita>> ObtenerCitasActivasAsync()
        {
            var citas = await _repository.GetCitasAsync();
            return citas.Where(c => c.Estado == true);
        }

        // Caso de uso: obtener cita por Id (solo si está activa)
        public async Task<Cita?> ObtenerCitaPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var cita = await _repository.GetCitaByIdAsync(id);
            if (cita != null && cita.Estado)
                return cita;

            return null;
        }

        // Caso de uso: agregar cita
        public async Task<string> AgregarCitaAsync(Cita nuevaCita)
        {
            try
            {
                if (nuevaCita.FechaHora < DateTime.Now)
                    return "Error: La fecha de la cita no puede ser en el pasado";

                nuevaCita.Estado = true; // Activa por defecto
                var citaInsertada = await _repository.AddCitaAsync(nuevaCita);

                if (citaInsertada == null || citaInsertada.Id <= 0)
                    return "Error: No se pudo guardar la cita";

                return "Cita agregada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Caso de uso: modificar cita existente
        public async Task<string> ModificarCitaAsync(Cita cita)
        {
            if (cita.Id <= 0)
                return "Error: Id de cita no válido";

            var existente = await _repository.GetCitaByIdAsync(cita.Id);
            if (existente == null)
                return "Error: Cita no encontrada";

            existente.FechaHora = cita.FechaHora;
            existente.Motivo = cita.Motivo;
            existente.NotasConsulta = cita.NotasConsulta;
            existente.Estado = cita.Estado;
            existente.IdPaciente = cita.IdPaciente;
            existente.IdDoctor = cita.IdDoctor;

            var actualizada = await _repository.UpdateCitaAsync(existente);

            if (actualizada == null)
                return "Error: No se pudo actualizar la cita";

            return "Cita modificada correctamente";
        }

        // Caso de uso: eliminar cita
        public async Task<string> EliminarCitaAsync(int id)
        {
            var resultado = await _repository.DeleteCitaAsync(id);
            return resultado ? "Cita eliminada correctamente" : "Error: No se encontró la cita";
        }
    }
}
