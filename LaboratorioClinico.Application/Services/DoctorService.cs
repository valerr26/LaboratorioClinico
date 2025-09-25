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

        // Obtener doctores activos
        public async Task<IEnumerable<Doctor>> ObtenerDoctoresActivosAsync()
        {
            var doctores = await _repository.GetDoctoresAsync();
            return doctores.Where(d => d.Estado);
        }

        // Obtener doctor por Id (si está activo)
        public async Task<Doctor?> ObtenerDoctorPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var doctor = await _repository.GetDoctorByIdAsync(id);
            return (doctor != null && doctor.Estado) ? doctor : null;
        }

        // Agregar doctor
        public async Task<string> AgregarDoctorAsync(Doctor nuevoDoctor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoDoctor.Nombre) || string.IsNullOrWhiteSpace(nuevoDoctor.Apellido))
                    return "Error: El nombre y apellido son obligatorios";

                if (string.IsNullOrWhiteSpace(nuevoDoctor.LicenciaMedica))
                    return "Error: La licencia médica es obligatoria";

                nuevoDoctor.Estado = true; // Activo por defecto
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

        // Modificar doctor
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
            existente.Estado = doctor.Estado;

            var actualizado = await _repository.UpdateDoctorAsync(existente);

            return actualizado != null ? "Doctor modificado correctamente" : "Error: No se pudo actualizar el doctor";
        }

        // Eliminar doctor (borrado lógico)
        public async Task<string> EliminarDoctorAsync(int id)
        {
            var doctor = await _repository.GetDoctorByIdAsync(id);
            if (doctor == null || !doctor.Estado)
                return "Error: Doctor no encontrado";

            doctor.Estado = false;
            await _repository.UpdateDoctorAsync(doctor);

            return "Doctor eliminado correctamente";
        }
    }
}
