using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_examen")]
    public class Examen
    {
        // --- ESTADOS PERMITIDOS ---
        public const string Pendiente = "Pendiente";
        public const string EnProceso = "En proceso";
        public const string Completado = "Completado";
        public const string Entregado = "Entregado";
        public const string Cancelado = "Cancelado";

        [Key]
        [Column("idexamen")]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Column("tipoexamen")]
        public string TipoExamen { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required, StringLength(200)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("idpaciente")]
        public int IdPaciente { get; set; }
        [JsonIgnore]
        public Paciente? Paciente { get; set; }

        [Required]
        [Column("idcita")]
        public int IdCita { get; set; }
        [JsonIgnore]
        public Cita? Cita { get; set; }

        [Required]
        [Column("estado")]
        [StringLength(50)]
        public string Estado { get; set; } = Pendiente;


        // --- MÉTODOS ---
        public string ObtenerEstado() => Estado;

        public void Activar() => Estado = Pendiente;

        public void Desactivar() => Estado = Cancelado;

        public bool EsReciente() => Fecha >= DateTime.Now.AddDays(-7);

        public string ObtenerResumen()
            => $"Examen #{Id}: {TipoExamen} - {Descripcion} ({Fecha:dd/MM/yyyy HH:mm}) [{Estado}]";
    }
}
