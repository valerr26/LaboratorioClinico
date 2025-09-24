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

        public async Task<IEnumerable<Paciente>> ObtenerPacientesAsync()
        {
            return await _repository.GetPacientesAsync();
        }

        public async Task<Paciente?> ObtenerPacientePorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repository.GetPacienteByIdAsync(id);
        }

        public async Task<string> AgregarPacienteAsync(Paciente paciente)
        {
            if (string.IsNullOrWhiteSpace(paciente.Nombre) || string.IsNullOrWhiteSpace(paciente.Apellido))
                return "Error: Nombre y apellido son obligatorios";

            var pacienteInsertado = await _repository.AddPacienteAsync(paciente);
            return pacienteInsertado != null ? "Paciente agregado correctamente" : "Error: No se pudo agregar el paciente";
        }

        public async Task<string> ModificarPacienteAsync(Paciente paciente)
        {
            if (paciente.Id <= 0)
                return "Error: Id de paciente no válido";

            var existente = await _repository.GetPacienteByIdAsync(paciente.Id);
            if (existente == null)
                return "Error: Paciente no encontrado";

            existente.Nombre = paciente.Nombre;
            existente.Apellido = paciente.Apellido;
            existente.FechaNacimiento = paciente.FechaNacimiento;
            existente.Telefono = paciente.Telefono;
            existente.Email = paciente.Email;
            existente.Direccion = paciente.Direccion;
            existente.IdUsuario = paciente.IdUsuario;

            var actualizado = await _repository.UpdatePacienteAsync(existente);
            return actualizado != null ? "Paciente modificado correctamente" : "Error: No se pudo modificar el paciente";
        }

        public async Task<string> EliminarPacienteAsync(int id)
        {
            var resultado = await _repository.DeletePacienteAsync(id);
            return resultado ? "Paciente eliminado correctamente" : "Error: No se encontró el paciente";
        }
    }
}