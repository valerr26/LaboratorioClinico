using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return citas.Where(c => c.Estado == "Activo");
        }

        // Obtener cita solo si está activa
        public async Task<Cita?> ObtenerCitaPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var cita = await _repository.GetCitaByIdAsync(id);

            if (cita != null && cita.Estado == "Activo")
                return cita;

            return null;
        }

        // Agregar cita
        public async Task<string> AgregarCitaAsync(Cita nuevaCita)
        {
            try
            {
                // Validar fecha futura
                if (nuevaCita.FechaHora <= DateTime.Now)
                    return "Error: La fecha de la cita debe ser futura.";

                // Validar reglas según tipo de cita (doctor requerido o no)
                nuevaCita.Validar();

                // Evitar citas duplicadas misma hora
                var citas = await _repository.GetCitasAsync();
                bool existeMismaHora = citas.Any(c =>
                    c.FechaHora == nuevaCita.FechaHora &&
                    c.Estado == "Activo"
                );

                if (existeMismaHora)
                    return "Error: Ya existe una cita en esa fecha y hora.";

                // Estado inicial
                nuevaCita.Estado = "Activo";

                var citaInsertada = await _repository.AddCitaAsync(nuevaCita);

                if (citaInsertada == null || citaInsertada.Id <= 0)
                    return "Error: No se pudo guardar la cita.";

                return "Cita agregada correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Modificar cita
        public async Task<string> ModificarCitaAsync(Cita cita)
        {
            if (cita.Id <= 0)
                return "Error: Id de cita inválido.";

            var existente = await _repository.GetCitaByIdAsync(cita.Id);
            if (existente == null)
                return "Error: Cita no encontrada.";

            // Validar fecha
            if (cita.FechaHora <= DateTime.Now)
                return "Error: La fecha debe ser futura.";

            // Validar reglas de consulta/examen
            cita.Validar();

            // Validar que no haya otra cita a la misma hora
            var todas = await _repository.GetCitasAsync();
            bool existeMismaHora = todas.Any(c =>
                c.Id != cita.Id &&
                c.FechaHora == cita.FechaHora &&
                c.Estado == "Activo"
            );

            if (existeMismaHora)
                return "Error: Ya existe una cita activa en esa fecha/hora.";

            // Actualizar campos
            existente.FechaHora = cita.FechaHora;
            existente.Motivo = cita.Motivo;
            existente.NotasConsulta = cita.NotasConsulta;
            existente.Estado = cita.Estado;
            existente.TipoCita = cita.TipoCita;
            existente.IdPaciente = cita.IdPaciente;

            // Doctor solo si es consulta
            existente.IdDoctor = (cita.TipoCita == "CONSULTA") ? cita.IdDoctor : null;

            var actualizada = await _repository.UpdateCitaAsync(existente);

            if (actualizada == null)
                return "Error: No se pudo actualizar la cita.";

            return "Cita modificada correctamente.";
        }

        // Eliminar cita (lógico o físico según tu repo)
        public async Task<string> EliminarCitaAsync(int id)
        {
            var resultado = await _repository.DeleteCitaAsync(id);
            return resultado ? "Cita eliminada correctamente." : "Error: No se encontró la cita.";
        }
    }
}
