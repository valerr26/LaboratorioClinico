using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<Rol>> ObtenerRolesAsync()
        {
            return await _repository.GetRolesAsync();
        }

        public async Task<Rol?> ObtenerRolPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repository.GetRolByIdAsync(id);
        }

        public async Task<string> AgregarRolAsync(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                return "Error: El nombre del rol es obligatorio";

            var rolInsertado = await _repository.AddRolAsync(rol);
            return rolInsertado != null ? "Rol agregado correctamente" : "Error: No se pudo agregar el rol";
        }

        public async Task<string> ModificarRolAsync(Rol rol)
        {
            if (rol.Id <= 0)
                return "Error: Id de rol no válido";

            var existente = await _repository.GetRolByIdAsync(rol.Id);
            if (existente == null)
                return "Error: Rol no encontrado";

            existente.Nombre = rol.Nombre;
            existente.Descripcion = rol.Descripcion;

            var actualizado = await _repository.UpdateRolAsync(existente);
            return actualizado != null ? "Rol modificado correctamente" : "Error: No se pudo modificar el rol";
        }

        public async Task<string> EliminarRolAsync(int id)
        {
            var resultado = await _repository.DeleteRolAsync(id);
            return resultado ? "Rol eliminado correctamente" : "Error: No se encontró el rol";
        }
    }
}
