using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_consulta")]
    public class Consulta
    {
        // --- ESTADOS PERMITIDOS ---
        public const string Activa = "Activa";
        public const string Inactiva = "Inactiva";
        public const string Cancelada = "Cancelada";
        public const string Finalizada = "Finalizada";

        [Key]
        [Column("idconsulta")]
        public int Id { get; set; }

        // Relación con Cita
        [Required]
        [Column("idcita")]
        public int IdCita { get; set; }
        [JsonIgnore]
        public Cita? Cita { get; set; }

        // Relación con Paciente
        [Required]
        [Column("idpaciente")]
        public int IdPaciente { get; set; }
        [JsonIgnore]
        public Paciente? Paciente { get; set; }

        // Relación con Doctor
        [Required]
        [Column("iddoctor")]
        public int IdDoctor { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        // Información de la consulta
        [Required]
        [Column("fechaconsulta")]
        public DateTime FechaConsulta { get; set; }

        [Required, StringLength(200)]
        [Column("motivo")]
        public string Motivo { get; set; } = string.Empty;

        [StringLength(300)]
        [Column("diagnostico")]
        public string Diagnostico { get; set; } = string.Empty;

        [StringLength(300)]
        [Column("tratamiento")]
        public string Tratamiento { get; set; } = string.Empty;

        [StringLength(300)]
        [Column("observaciones")]
        public string Observaciones { get; set; } = string.Empty;

        // Estado string (igual que Examen)
        [Required]
        [Column("estado")]
        [StringLength(50)]
        public string Estado { get; set; } = Activa;


        // --- MÉTODOS ---

        public string ObtenerEstado() => Estado;

        public void Activar() => Estado = Activa;

        public void Desactivar() => Estado = Inactiva;

        public void Cancelar() => Estado = Cancelada;

        public void Finalizar() => Estado = Finalizada;

        public bool EsReciente()
            => FechaConsulta >= DateTime.Now.AddDays(-3);

        public string ObtenerResumen()
            => $"Consulta #{Id}: {Motivo} - {FechaConsulta:dd/MM/yyyy HH:mm} [{Estado}]";
    }
}
