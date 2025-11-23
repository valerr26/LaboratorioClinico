using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Referencias
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using LaboratorioClinico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioClinico.Infrastructure.Repositories
{
    public class ExamenRepository : IExamenRepository
    {
        private readonly AppDBContext _context;

        public ExamenRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Examen>> GetExamenesAsync()
        {
            return await _context.Examenes
                                 .Include(e => e.Paciente)
                                 .Include(e => e.Cita)
                                 .ToListAsync();
        }

        public async Task<Examen> GetExamenByIdAsync(int id)
        {
            return await _context.Examenes
                                 .Include(e => e.Paciente)
                                 .Include(e => e.Cita)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Examen> AddExamenAsync(Examen examen)
        {
            _context.Examenes.Add(examen);
            await _context.SaveChangesAsync();
            return examen;
        }

        public async Task<Examen> UpdateExamenAsync(Examen examen)
        {
            var existingExamen = await _context.Examenes.FindAsync(examen.Id);
            if (existingExamen == null)
            {
                return null;
            }

            existingExamen.TipoExamen = examen.TipoExamen;
            existingExamen.Fecha = examen.Fecha;
            existingExamen.Descripcion = examen.Descripcion;
            existingExamen.IdPaciente = examen.IdPaciente;
            existingExamen.IdCita = examen.IdCita;

            // 👉 CAMBIO IMPORTANTE: Estado STRING
            existingExamen.Estado = examen.Estado;

            await _context.SaveChangesAsync();
            return existingExamen;
        }

        // ❗AHORA SE HACE ELIMINACIÓN LÓGICA
        public async Task<bool> DeleteExamenAsync(int id)
        {
            var examen = await _context.Examenes.FindAsync(id);
            if (examen == null)
            {
                return false;
            }

            // 👉 NO BORRA — solo actualiza el estado
            examen.Estado = "Cancelado";

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
