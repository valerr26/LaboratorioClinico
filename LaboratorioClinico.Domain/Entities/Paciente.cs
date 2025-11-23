using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_paciente")]
    public class Paciente
    {
        [Key]
        [Column("idpaciente")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required, StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [Column("fechanacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [StringLength(200)]
        [Column("direccion")]
        public string Direccion { get; set; }

        // Relación con Usuario (N:1)
        [Required]
        [Column("idusuario")]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        [Required]
        [StringLength(30)]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        // Estados permitidos
        public static readonly string[] EstadosPermitidos =
        {
            "Activo",
            "Inactivo",
            "En tratamiento",
            "Suspendido",
            "Fallecido"
        };

        // -----------------------------------------------------
        // Relación con Doctor
        // -----------------------------------------------------
        [Required]
        [Column("iddoctor")]
        [ForeignKey("Doctor")]
        public int IdDoctor { get; set; }

        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        // Relación: Paciente → Citas
        public ICollection<Cita>? Citas { get; set; }

        // Relación: Paciente → Exámenes
        public ICollection<Examen>? Examenes { get; set; }

        // Relación: Paciente → Consultas
        public ICollection<Consulta>? Consultas { get; set; }



        // --------------------- MÉTODOS LÓGICOS --------------------------

        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        public bool CambiarEstado(string nuevoEstado)
        {
            if (!EstadosPermitidos.Contains(nuevoEstado))
                return false;

            Estado = nuevoEstado;
            return true;
        }

        public bool EsMayorEdad()
        {
            var edad = DateTime.Now.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
            return edad >= 18;
        }

        public string ObtenerResumen()
        {
            return $"Paciente: {Nombre} {Apellido} | Estado: {Estado} | Nacido el {FechaNacimiento:dd/MM/yyyy}";
        }
    }
}
