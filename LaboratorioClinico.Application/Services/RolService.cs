using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class RolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        // Obtener roles activos
        public async Task<IEnumerable<Rol>> ObtenerRolesActivosAsync()
        {
            var roles = await _repository.GetRolesAsync();
            return roles.Where(r => r.Estado == "ACTIVO");
        }

        // Obtener rol por Id (solo si está activo)
        public async Task<Rol?> ObtenerRolPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var rol = await _repository.GetRolByIdAsync(id);
            return (rol != null && rol.Estado == "ACTIVO") ? rol : null;
        }

        // Agregar rol
        public async Task<string> AgregarRolAsync(Rol nuevoRol)
        {
            if (string.IsNullOrWhiteSpace(nuevoRol.Nombre))
                return "Error: El nombre del rol es obligatorio";

            nuevoRol.Estado = "ACTIVO";  // ⬅️ Activo por defecto
            var rolInsertado = await _repository.AddRolAsync(nuevoRol);

            return rolInsertado != null ? "Rol agregado correctamente" : "Error: No se pudo agregar el rol";
        }

        // Modificar rol
        public async Task<string> ModificarRolAsync(Rol rol)
        {
            if (rol.Id <= 0)
                return "Error: Id de rol no válido";

            var existente = await _repository.GetRolByIdAsync(rol.Id);
            if (existente == null)
                return "Error: Rol no encontrado";

            existente.Nombre = rol.Nombre;
            existente.Descripcion = rol.Descripcion;

            // Garantizar valores válidos
            existente.Estado = rol.Estado?.ToUpper() == "INACTIVO" ? "INACTIVO" : "ACTIVO";

            var actualizado = await _repository.UpdateRolAsync(existente);

            return actualizado != null ? "Rol modificado correctamente" : "Error: No se pudo actualizar el rol";
        }

        // Eliminar rol (borrado lógico → INACTIVO)
        public async Task<string> EliminarRolAsync(int id)
        {
            var rol = await _repository.GetRolByIdAsync(id);
            if (rol == null)
                return "Error: Rol no encontrado";

            if (rol.Estado == "INACTIVO")
                return "Error: El rol ya está inactivo";

            rol.Estado = "INACTIVO";
            await _repository.UpdateRolAsync(rol);

            return "Rol eliminado correctamente";
        }
    }
}
