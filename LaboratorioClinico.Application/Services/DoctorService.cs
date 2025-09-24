using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los doctores
        public async Task<IEnumerable<Doctor>> ObtenerDoctoresAsync()
        {
            return await _repository.GetDoctoresAsync();
        }

        // Obtener doctor por Id
        public async Task<Doctor?> ObtenerDoctorPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repository.GetDoctorByIdAsync(id);
        }

        // Agregar un doctor
        public async Task<string> AgregarDoctorAsync(Doctor nuevoDoctor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoDoctor.Nombre) || string.IsNullOrWhiteSpace(nuevoDoctor.Apellido))
                    return "Error: El nombre y apellido son obligatorios";

                if (string.IsNullOrWhiteSpace(nuevoDoctor.LicenciaMedica))
                    return "Error: La licencia médica es obligatoria";

                var doctorInsertado = await _repository.AddDoctorAsync(nuevoDoctor);

                if (doctorInsertado == null || doctorInsertado.Id <= 0)
                    return "Error: No se pudo registrar el doctor";

                return "Doctor agregado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Modificar un doctor
        public async Task<string> ModificarDoctorAsync(Doctor doctor)
        {
            if (doctor.Id <= 0)
                return "Error: Id de doctor no válido";

            var existente = await _repository.GetDoctorByIdAsync(doctor.Id);
            if (existente == null)
                return "Error: Doctor no encontrado";

            existente.Nombre = doctor.Nombre;
            existente.Apellido = doctor.Apellido;
            existente.Especialidad = doctor.Especialidad;
            existente.Telefono = doctor.Telefono;
            existente.Email = doctor.Email;
            existente.LicenciaMedica = doctor.LicenciaMedica;
            existente.IdUsuario = doctor.IdUsuario;

            var actualizado = await _repository.UpdateDoctorAsync(existente);

            if (actualizado == null)
                return "Error: No se pudo actualizar el doctor";

            return "Doctor modificado correctamente";
        }

        // Eliminar un doctor
        public async Task<string> EliminarDoctorAsync(int id)
        {
            var resultado = await _repository.DeleteDoctorAsync(id);
            return resultado ? "Doctor eliminado correctamente" : "Error: No se encontró el doctor";
        }
    }
}
