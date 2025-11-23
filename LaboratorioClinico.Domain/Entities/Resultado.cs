using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_resultado")]
    public class Resultado
    {
        [Key]
        [Column("idresultado")]
        public int Id { get; set; }

        [Required, StringLength(500)]
        [Column("detalle")]
        public string Detalle { get; set; }

        [Required]
        [Column("fechaemision")]
        public DateTime FechaEmision { get; set; }

        // -------- PACIENTE --------
        [Required]
        [Column("idpaciente")]
        public int IdPaciente { get; set; }

        [JsonIgnore]
        public Paciente? Paciente { get; set; }


        // -------- EXAMEN --------
        [Required]
        [Column("idexamen")]
        public int IdExamen { get; set; }

        [JsonIgnore]
        public Examen? Examen { get; set; }


        // -------- DOCTOR --------
        [Required]
        [Column("iddoctor")]
        public int IdDoctor { get; set; }

        [JsonIgnore]
        public Doctor? Doctor { get; set; }


        // -------- ESTADO --------
        [Required]
        [Column("estado")]
        [StringLength(50)]
        public string Estado { get; set; } = Validado;

        public const string Validado = "Validado";
        public const string Entregado = "Entregado";
        public const string Anulado = "Anulado";

        public string ObtenerEstado() => Estado;
        public void Validar() => Estado = Validado;
        public void Entregar() => Estado = Entregado;
        public void Anular() => Estado = Anulado;

        public bool EsReciente() =>
            FechaEmision >= DateTime.Now.AddDays(-7);

        public string ObtenerResumen() =>
            $"Resultado #{Id}: {Detalle} - Emitido el {FechaEmision:dd/MM/yyyy} [{Estado}]";
    }
}
