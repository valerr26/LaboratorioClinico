using LaboratorioClinico.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface IExamenRepository
    {
        // Obtener todos los exámenes
        Task<IEnumerable<Examen>> GetExamenesAsync();

        // Obtener un examen por su ID
        Task<Examen> GetExamenByIdAsync(int id);

        // Agregar un nuevo examen
        Task<Examen> AddExamenAsync(Examen examen);

        // Actualizar un examen existente
        Task<Examen> UpdateExamenAsync(Examen examen);

        // ❌ Ya no se debe eliminar físicamente — pero lo dejo por si tu arquitectura lo exige
        // Task<bool> DeleteExamenAsync(int id);
    }
}
