using Microsoft.EntityFrameworkCore;
using LaboratorioClinico.Domain.Entities;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;

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
            modelBuilder.Entity<Usuario>().ToTable("t_Usuario");
            modelBuilder.Entity<Rol>().ToTable("t_Rol");
            modelBuilder.Entity<Paciente>().ToTable("t_Paciente");
            modelBuilder.Entity<Doctor>().ToTable("t_Doctor");
            modelBuilder.Entity<Cita>().ToTable("t_Cita");
            modelBuilder.Entity<Examen>().ToTable("t_Examen");
            modelBuilder.Entity<Resultado>().ToTable("t_Resultado");

            base.OnModelCreating(modelBuilder);
        }
    }
}

