using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
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
                                 .Include(e => e.Paciente)  // 👈 propiedad de navegación
                                 .Include(e => e.Cita)      // opcional si quieres también la cita
                                 .ToListAsync();
        }

        public async Task<Examen> GetExamenByIdAsync(int id)
        {
            return await _context.Examenes
                                 .Include(e => e.Paciente)  // 👈 no el Id, sino la entidad relacionada
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
            existingExamen.Estado = examen.Estado;

            await _context.SaveChangesAsync();
            return existingExamen;
        }

        public async Task<bool> DeleteExamenAsync(int id)
        {
            var examen = await _context.Examenes.FindAsync(id);
            if (examen == null)
            {
                return false;
            }

            _context.Examenes.Remove(examen);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}