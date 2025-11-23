using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;

namespace LaboratorioClinico.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        // Obtener usuarios activos
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosActivosAsync()
        {
            var usuarios = await _repository.GetUsuarioAsync();
            return usuarios.Where(u => u.EstaActivo()); // ahora compara string
        }

        // Obtener usuario por Id (si está activo)
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var usuario = await _repository.GetUsuarioByIdAsync(id);

            return (usuario != null && usuario.EstaActivo()) ? usuario : null;
        }

        // Agregar usuario
        public async Task<string> AgregarUsuarioAsync(Usuario nuevoUsuario)
        {
            try
            {
                var usuarios = await _repository.GetUsuarioAsync();

                if (usuarios.Any(u => u.Username.ToLower() == nuevoUsuario.Username.ToLower()))
                    return "Error: Ya existe un usuario con ese username";

                // Estado por defecto
                nuevoUsuario.Activar(); // Estado = "Activo"

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

        // Modificar usuario
        public async Task<string> ModificarUsuarioAsync(Usuario usuario)
        {
            if (usuario.Id <= 0)
                return "Error: ID no válido";

            var existente = await _repository.GetUsuarioByIdAsync(usuario.Id);
            if (existente == null)
                return "Error: Usuario no encontrado";

            existente.Username = usuario.Username;
            existente.PasswordHash = usuario.PasswordHash;
            existente.IdRol = usuario.IdRol;
            existente.Estado = usuario.Estado; // ahora es string

            var actualizado = await _repository.UpdateUsuarioAsync(existente);

            return actualizado != null
                ? "Usuario modificado correctamente"
                : "Error: No se pudo actualizar el usuario";
        }

        // Eliminar usuario (cambia a INACTIVO)
        public async Task<string> EliminarUsuarioAsync(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);

            if (usuario == null || !usuario.EstaActivo())
                return "Error: Usuario no encontrado";

            usuario.Inactivar(); // Estado = "Inactivo"

            await _repository.UpdateUsuarioAsync(usuario);

            return "Usuario eliminado correctamente";
        }
    }
}
