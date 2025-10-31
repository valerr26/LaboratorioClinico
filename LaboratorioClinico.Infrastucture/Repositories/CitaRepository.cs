using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referencias
using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories; 
using LaboratorioClinico.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;

namespace LaboratorioClinico.Infrastucture.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly AppDBContext _context;

        public CitaRepository(AppDBContext context)
        {
            _context = context;
        }

        // Agregar una nueva cita
        public async Task<Cita> AddCitaAsync(Cita cita)
        {
            try
            {
                _context.Citas.Add(cita);
                await _context.SaveChangesAsync();
                return cita;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // Obtener una cita por su id
        public async Task<Cita> GetCitaByIdAsync(int id)
        {
            return await _context.Citas
                                 .Include(c => c.IdPaciente)  // propiedad de navegación
                                 .Include(c => c.IdDoctor)    // propiedad de navegación
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Obtener todas las citas
        public async Task<IEnumerable<Cita>> GetCitasAsync()
        {
            var lista= await _context.Citas.ToListAsync();
            return lista;
        }

        // Actualizar una cita existente
        public async Task<Cita> UpdateCitaAsync(Cita cita)
        {
            var existingCita = await _context.Citas.FindAsync(cita.Id);
            if (existingCita == null)
            {
                return null;
            }

            existingCita.FechaHora = cita.FechaHora;
            existingCita.Motivo = cita.Motivo;
            existingCita.Estado = cita.Estado;
            existingCita.NotasConsulta = cita.NotasConsulta;
            existingCita.IdPaciente = cita.IdPaciente;
            existingCita.IdDoctor = cita.IdDoctor;
            existingCita.Estado = cita.Estado;

            await _context.SaveChangesAsync();
            return existingCita;
        }

        // Eliminar una cita por su id
        public async Task<bool> DeleteCitaAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}