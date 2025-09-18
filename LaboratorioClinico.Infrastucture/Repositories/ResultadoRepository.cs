using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referncias
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

        public async Task<IEnumerable<Resultado>> GetResultadosAsync()
        {
            return await _context.Resultados
                                 .Include(r => r.IdExamen) // si configuraste la relación en el DbContext
                                 .Include(r => r.IdDoctor) // opcional
                                 .ToListAsync();
        }

        public async Task<Resultado> GetResultadoByIdAsync(int id)
        {
            return await _context.Resultados
                                 .Include(r => r.IdExamen)
                                 .Include(r => r.IdDoctor)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resultado> AddResultadoAsync(Resultado resultado)
        {
            _context.Resultados.Add(resultado);
            await _context.SaveChangesAsync();
            return resultado;
        }

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

            await _context.SaveChangesAsync();
            return existingResultado;
        }

        public async Task<bool> DeleteResultadoAsync(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado == null)
            {
                return false;
            }

            _context.Resultados.Remove(resultado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
