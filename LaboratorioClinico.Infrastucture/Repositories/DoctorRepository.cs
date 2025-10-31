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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDBContext _context;

        public DoctorRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctoresAsync()
        {
            return await _context.Doctores.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctores
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            try
            {
                _context.Doctores.Add(doctor);
                await _context.SaveChangesAsync();
                return doctor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _context.Doctores.FindAsync(doctor.Id);
            if (existingDoctor == null)
            {
                return null;
            }

            existingDoctor.Nombre = doctor.Nombre;
            existingDoctor.Apellido = doctor.Apellido;
            existingDoctor.Especialidad = doctor.Especialidad;
            existingDoctor.Telefono = doctor.Telefono;
            existingDoctor.Email = doctor.Email;
            existingDoctor.LicenciaMedica = doctor.LicenciaMedica;
            existingDoctor.IdUsuario = doctor.IdUsuario;
            existingDoctor.Estado = doctor.Estado;

            await _context.SaveChangesAsync();
            return existingDoctor;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctores.FindAsync(id);
            if (doctor == null)
            {
                return false;
            }

            _context.Doctores.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}