using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        // Obtener pacientes activos
        public async Task<IEnumerable<Paciente>> ObtenerPacientesActivosAsync()
        {
            var pacientes = await _repository.GetPacientesAsync();
            return pacientes.Where(p => p.Estado == "Activo");
        }

        // Obtener paciente por Id (si está activo)
        public async Task<Paciente?> ObtenerPacientePorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var paciente = await _repository.GetPacienteByIdAsync(id);
            return (paciente != null && paciente.Estado == "Activo") ? paciente : null;
        }

        // Agregar paciente
        public async Task<string> AgregarPacienteAsync(Paciente nuevoPaciente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoPaciente.Nombre) || string.IsNullOrWhiteSpace(nuevoPaciente.Apellido))
                    return "Error: El nombre y apellido son obligatorios";

                if (string.IsNullOrWhiteSpace(nuevoPaciente.Direccion))
                    return "Error: La dirección es obligatoria";

                // Estado por defecto
                nuevoPaciente.Estado = "Activo";
                var pacienteInsertado = await _repository.AddPacienteAsync(nuevoPaciente);

                if (pacienteInsertado == null || pacienteInsertado.Id <= 0)
                    return "Error: No se pudo registrar el paciente";

                return "Paciente agregado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Modificar paciente
        public async Task<string> ModificarPacienteAsync(Paciente paciente)
        {
            if (paciente.Id <= 0)
                return "Error: Id de paciente no válido";

            var existente = await _repository.GetPacienteByIdAsync(paciente.Id);
            if (existente == null)
                return "Error: Paciente no encontrado";

            existente.Nombre = paciente.Nombre;
            existente.Apellido = paciente.Apellido;
            existente.Direccion = paciente.Direccion;
            existente.Telefono = paciente.Telefono;
            existente.Email = paciente.Email;
            existente.FechaNacimiento = paciente.FechaNacimiento;
            existente.IdUsuario = paciente.IdUsuario;

            // Cambiar estado solo si es válido
            if (!string.IsNullOrWhiteSpace(paciente.Estado))
            {
                existente.CambiarEstado(paciente.Estado);
            }

            var actualizado = await _repository.UpdatePacienteAsync(existente);

            return actualizado != null ? "Paciente modificado correctamente" : "Error: No se pudo actualizar el paciente";
        }

        // Eliminar paciente (borrado lógico)
        public async Task<string> EliminarPacienteAsync(int id)
        {
            var paciente = await _repository.GetPacienteByIdAsync(id);
            if (paciente == null || paciente.Estado != "Activo")
                return "Error: Paciente no encontrado";

            paciente.CambiarEstado("Inactivo");
            await _repository.UpdatePacienteAsync(paciente);

            return "Paciente eliminado correctamente";
        }
    }
}
