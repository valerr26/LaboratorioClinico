using LaboratorioClinico.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface IConsultaRepository
    {
        // Obtener todas las consultas
        Task<IEnumerable<Consulta>> GetConsultasAsync();

        // Obtener una consulta por su ID
        Task<Consulta> GetConsultaByIdAsync(int id);

        // Agregar una nueva consulta
        Task<Consulta> AddConsultaAsync(Consulta consulta);

        // Actualizar una consulta existente
        Task<Consulta> UpdateConsultaAsync(Consulta consulta);

        // ❌ Igual que Examen: no se eliminará físicamente
        // Task<bool> DeleteConsultaAsync(int id);
    }
}
