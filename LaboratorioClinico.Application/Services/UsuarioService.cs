using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

   
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            return await _repository.GetUsuarioAsync();
        }

  
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var usuario = await _repository.GetUsuarioByIdAsync(id);
            return usuario; // retorna null si no se encuentra
        }


        public async Task<string> AgregarUsuarioAsync(Usuario nuevoUsuario)
        {
            try
            {
                var usuarios = await _repository.GetUsuarioAsync();
                if (usuarios.Any(u => u.Username.ToLower() == nuevoUsuario.Username.ToLower()))
                    return "Error: Ya existe un usuario con ese username";

                var usuarioInsertado = await _repository.AddUsuarioAsync(nuevoUsuario);
                if (usuarioInsertado == null || usuarioInsertado.Id <= 0)
                    return "Error: No se pudo agregar el usuario";

                return "Usuario agregado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> ModificarUsuarioAsync(Usuario usuario)
        {
            if (usuario.Id <= 0)
                return "Error: ID no válido";

            var existente = await _repository.GetUsuarioByIdAsync(usuario.Id);
            if (existente == null)
                return "Error: Usuario no encontrado";

            existente.Username = usuario.Username;
            existente.Password = usuario.Password;
            existente.IdRol = usuario.IdRol;

            await _repository.UpdateUsuarioAsync(existente);
            return "Usuario modificado correctamente";
        }

        public async Task<string> EliminarUsuarioAsync(int id)
        {
            if (id <= 0)
                return "Error: ID no válido";

            var eliminado = await _repository.DeleteUsuarioAsync(id);
            return eliminado ? "Usuario eliminado correctamente" : "Error: Usuario no encontrado";
        }
    }
}
