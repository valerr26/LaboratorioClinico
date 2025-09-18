using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface IExamenRepository
    {
        // Obtener todos los examenes
        Task<IEnumerable<Examen>> GetExamenesAsync();

        // Obtener un examen por su id
        Task<Examen> GetExamenByIdAsync(int id);

        // Agregar un nuevo examen
        Task<Examen> AddExamenAsync(Examen examen);

        // Actualizar un examen existente
        Task<Examen> UpdateExamenAsync(Examen examen);

        // Eliminar un examen por su id
        Task<bool> DeleteExamenAsync(int id);
    }
}
