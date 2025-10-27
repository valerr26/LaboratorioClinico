using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        // Obtener todos los usuarios
        Task<IEnumerable<Usuario>> GetUsuarioAsync();

        // Obtener un usuario por su id
        Task<Usuario> GetUsuarioByIdAsync(int id);

        // 🔍 Obtener un usuario por su username (usado por AuthService)
        Task<Usuario?> GetByUsernameAsync(string username);

        // Agregar un nuevo usuario
        Task<Usuario> AddUsuarioAsync(Usuario usuario);

        // Actualizar un usuario existente
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);

        // Eliminar un usuario por su id
        Task<bool> DeleteUsuarioAsync(int id);
    }
}