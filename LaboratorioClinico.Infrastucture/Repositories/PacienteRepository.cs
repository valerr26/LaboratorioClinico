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

namespace LaboratorioClinico.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDBContext _context;

        public PacienteRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetPacientesAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> AddPacienteAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> UpdatePacienteAsync(Paciente paciente)
        {
            var existingPaciente = await _context.Pacientes.FindAsync(paciente.Id);
            if (existingPaciente == null)
            {
                return null;
            }

            existingPaciente.Nombre = paciente.Nombre;
            existingPaciente.Apellido = paciente.Apellido;
            existingPaciente.FechaNacimiento = paciente.FechaNacimiento;
            existingPaciente.Telefono = paciente.Telefono;
            existingPaciente.Email = paciente.Email;
            existingPaciente.Direccion = paciente.Direccion;
            existingPaciente.IdUsuario = paciente.IdUsuario;

            await _context.SaveChangesAsync();
            return existingPaciente;
        }

        public async Task<bool> DeletePacienteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return false;
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
