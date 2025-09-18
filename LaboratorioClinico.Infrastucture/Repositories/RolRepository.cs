using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioClinico.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDBContext _context;

        public RolRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetRolesAsync()
        {
            return await _context.Roles
                                 .Include(r => r.Usuarios) // opcional, si tienes relación en DbContext
                                 .ToListAsync();
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            return await _context.Roles
                                 .Include(r => r.Usuarios)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rol> AddRolAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> UpdateRolAsync(Rol rol)
        {
            var existingRol = await _context.Roles.FindAsync(rol.Id);
            if (existingRol == null)
            {
                return null;
            }

            existingRol.Nombre = rol.Nombre;
            existingRol.Descripcion = rol.Descripcion;

            await _context.SaveChangesAsync();
            return existingRol;
        }

        public async Task<bool> DeleteRolAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return false;
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

