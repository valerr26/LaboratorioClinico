using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface IResultadoRepository
    {
        // Obtener todos los resultados
        Task<IEnumerable<Resultado>> GetResultadosAsync();

        // Obtener un resultado por su id
        Task<Resultado> GetResultadoByIdAsync(int id);

        // Agregar un nuevo resultado
        Task<Resultado> AddResultadoAsync(Resultado resultado);

        // Actualizar un resultado existente
        Task<Resultado> UpdateResultadoAsync(Resultado resultado);

        
    }
}
