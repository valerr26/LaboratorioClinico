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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDBContext _context;

        public UsuarioRepository(AppDBContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los usuarios
        public async Task<IEnumerable<Usuario>> GetUsuarioAsync()
        {
            return await _context.Usuarios
                                 .Include(u => u.Rol)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        // 🔹 Obtener un usuario por ID
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                                 .Include(u => u.Rol)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        // 🔹 Obtener un usuario por Username (para autenticación)
        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        // 🔹 Agregar usuario
        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // 🔹 Actualizar usuario
        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            var existingUsuario = await _context.Usuarios.FindAsync(usuario.Id);
            if (existingUsuario == null)
                return null;

            existingUsuario.Username = usuario.Username;
            existingUsuario.PasswordHash = usuario.PasswordHash;
            existingUsuario.IdRol = usuario.IdRol;
            existingUsuario.Estado = usuario.Estado;

            await _context.SaveChangesAsync();
            return existingUsuario;
        }

        // 🔹 Eliminar usuario
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}