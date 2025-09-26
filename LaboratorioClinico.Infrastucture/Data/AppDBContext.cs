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

            // Rol ↔ Usuario (1:N)
            modelBuilder.Entity<Usuario>()
              .HasOne(u => u.Rol)
              .WithMany(r => r.Usuarios)
              .HasForeignKey(u => u.IdRol);

            // Paciente ↔ Cita (1:N)
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas) 
                .HasForeignKey(c => c.IdPaciente);

            // Doctor ↔ Cita (1:N)
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Citas) 
                .HasForeignKey(c => c.IdDoctor);

            // Cita ↔ Examen (1:N)
            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Cita)
                .WithMany(c => c.Examenes) 
                .HasForeignKey(e => e.IdCita);

            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Resultado)
                .WithMany(r => r.Examenes)
                .HasForeignKey(e => e.IdResultado);


        }
    }
}

