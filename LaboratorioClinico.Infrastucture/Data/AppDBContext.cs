using Microsoft.EntityFrameworkCore;
using LaboratorioClinico.Domain.Entities;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Microsoft.Win32;

namespace LaboratorioClinico.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Examen> Examenes { get; set; }
        public DbSet<Resultado> Resultados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("t_usuario");
            modelBuilder.Entity<Rol>().ToTable("t_rol");
            modelBuilder.Entity<Paciente>().ToTable("t_paciente");
            modelBuilder.Entity<Doctor>().ToTable("t_doctor");
            modelBuilder.Entity<Cita>().ToTable("t_cita");
            modelBuilder.Entity<Examen>().ToTable("t_examen");
            modelBuilder.Entity<Resultado>().ToTable("t_resultado");

            base.OnModelCreating(modelBuilder);

            // 🔹 Relaciones

            // Usuario ↔ Rol (N:1)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor ↔ Usuario (1:1)
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Usuario)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Paciente ↔ Usuario (N:1)
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Paciente ↔ Doctor (N:1)
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Doctor)
                .WithMany()
                .HasForeignKey(p => p.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict);

            // Cita ↔ Paciente (N:1)
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.IdPaciente)
                .OnDelete(DeleteBehavior.Cascade);

            // Cita ↔ Doctor (N:1)
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Citas)
                .HasForeignKey(c => c.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict);

            // Examen ↔ Paciente (N:1)
            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Paciente)
                .WithMany(p => p.Examenes)
                .HasForeignKey(e => e.IdPaciente)
                .OnDelete(DeleteBehavior.Cascade);

            // Examen ↔ Cita (N:1)
            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Cita)
                .WithMany(c => c.Examenes)
                .HasForeignKey(e => e.IdCita)
                .OnDelete(DeleteBehavior.Cascade);

            // Resultado ↔ Examen (N:1)
            modelBuilder.Entity<Resultado>()
                .HasOne(r => r.Examen)
                .WithMany()
                .HasForeignKey(r => r.IdExamen)
                .OnDelete(DeleteBehavior.Cascade);

            // Resultado ↔ Doctor (N:1)
            modelBuilder.Entity<Resultado>()
                .HasOne(r => r.Doctor)
                .WithMany()
                .HasForeignKey(r => r.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

