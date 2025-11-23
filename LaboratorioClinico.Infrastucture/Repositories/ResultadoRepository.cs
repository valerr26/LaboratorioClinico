using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Referencias
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioClinico.Infrastructure.Repositories
{
    public class ResultadoRepository : IResultadoRepository
    {
        private readonly AppDBContext _context;

        public ResultadoRepository(AppDBContext context)
        {
            _context = context;
        }

        // ✔ Obtener todos los resultados
        public async Task<IEnumerable<Resultado>> GetResultadosAsync()
        {
            return await _context.Resultados
                                 .Include(r => r.Examen)
                                 .Include(r => r.Doctor)
                                 .Include(r => r.Paciente)
                                 .ToListAsync();
        }

        // ✔ Obtener por ID con relaciones
        public async Task<Resultado> GetResultadoByIdAsync(int id)
        {
            return await _context.Resultados
                                 .Include(r => r.Examen)
                                 .Include(r => r.Doctor)
                                 .Include(r => r.Paciente)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        // ✔ Agregar
        public async Task<Resultado> AddResultadoAsync(Resultado resultado)
        {
            _context.Resultados.Add(resultado);
            await _context.SaveChangesAsync();
            return resultado;
        }

        // ✔ Actualizar con estado STRING
        public async Task<Resultado> UpdateResultadoAsync(Resultado resultado)
        {
            var existingResultado = await _context.Resultados.FindAsync(resultado.Id);
            if (existingResultado == null)
            {
                return null;
            }

            existingResultado.Detalle = resultado.Detalle;
            existingResultado.FechaEmision = resultado.FechaEmision;
            existingResultado.IdExamen = resultado.IdExamen;
            existingResultado.IdDoctor = resultado.IdDoctor;
            existingResultado.IdPaciente = resultado.IdPaciente;

            // 👉 Estado tipo STRING
            existingResultado.Estado = resultado.Estado;

            await _context.SaveChangesAsync();
            return existingResultado;
        }

        // ❗ AHORA SE HACE ELIMINADO LÓGICO
        public async Task<bool> DeleteResultadoAsync(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado == null)
            {
                return false;
            }

            // 👉 NO borra físicamente — solo cambia el estado
            resultado.Estado = "Anulado";

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
