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
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDBContext _context;

        public ConsultaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetConsultasAsync()
        {
            return await _context.Consultas
                                 .Include(c => c.Paciente)
                                 .ToListAsync();
        }

        public async Task<Consulta> GetConsultaByIdAsync(int id)
        {
            return await _context.Consultas
                                 .Include(c => c.Paciente)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Consulta> AddConsultaAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task<Consulta> UpdateConsultaAsync(Consulta consulta)
        {
            var existingConsulta = await _context.Consultas.FindAsync(consulta.Id);
            if (existingConsulta == null)
            {
                return null;
            }

            existingConsulta.FechaConsulta = consulta.FechaConsulta;
            existingConsulta.Motivo = consulta.Motivo;
            existingConsulta.IdPaciente = consulta.IdPaciente;

            // 👉 Estado (sea string o bool)
            existingConsulta.Estado = consulta.Estado;

            await _context.SaveChangesAsync();
            return existingConsulta;
        }

        // ❗ ELIMINACIÓN LÓGICA
        public async Task<bool> DeleteConsultaAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return false;
            }

            // 👉 NO se borra — solo se cambia el estado
             consulta.Estado = "Cancelada"; // Si usas string

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

