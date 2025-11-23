using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // --------------------- AGREGAR CITA ---------------------
        public async Task<Cita> AddCitaAsync(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        // --------------------- OBTENER CITA POR ID ---------------------
        public async Task<Cita?> GetCitaByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // --------------------- OBTENER TODAS LAS CITAS ---------------------
        public async Task<IEnumerable<Cita>> GetCitasAsync()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .ToListAsync();
        }

        // --------------------- OBTENER CITAS ACTIVAS ---------------------
        public async Task<IEnumerable<Cita>> GetCitasActivasAsync()
        {
            return await _context.Citas
                .Where(c => c.Estado == "Activo")
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .ToListAsync();
        }

        // --------------------- ACTUALIZAR CITA ---------------------
        public async Task<Cita?> UpdateCitaAsync(Cita cita)
        {
            var existente = await _context.Citas.FindAsync(cita.Id);
            if (existente == null)
                return null;

            existente.FechaHora = cita.FechaHora;
            existente.Motivo = cita.Motivo;
            existente.TipoCita = cita.TipoCita;
            existente.Estado = cita.Estado;
            existente.NotasConsulta = cita.NotasConsulta;
            existente.IdPaciente = cita.IdPaciente;

            // Doctor solo si es CONSULTA
            existente.IdDoctor = cita.TipoCita == "CONSULTA"
                ? cita.IdDoctor
                : null;

            await _context.SaveChangesAsync();
            return existente;
        }

        // --------------------- CAMBIAR ESTADO (NO ELIMINA) ---------------------
        public async Task<bool> DeleteCitaAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return false;

            // 🔥 En lugar de eliminar → solo cambiar estado a INACTIVO
            cita.Estado = "Inactivo";

            await _context.SaveChangesAsync();
            return true;
        }

        // --------------------- VALIDAR HORA OCUPADA ---------------------
        public async Task<bool> ExisteCitaEnFechaHoraAsync(DateTime fechaHora)
        {
            return await _context.Citas
                .AnyAsync(c => c.FechaHora == fechaHora && c.Estado == "Activo");
        }

        // --------------------- CITAS POR FECHA ---------------------
        public async Task<IEnumerable<Cita>> GetCitasPorFechaAsync(DateTime fecha)
        {
            return await _context.Citas
                .Where(c => c.FechaHora.Date == fecha.Date)
                .ToListAsync();
        }
    }
}
