using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_cita")]
    public class Cita
    {
        [Key]
        [Column("idcita")]
        public int Id { get; set; }

        // ----------- NUEVO: Tipo de cita -----------
        [Required]
        [Column("tipocita")]
        [StringLength(20)]
        public string TipoCita { get; set; } = "EXAMEN";
        // Valores: EXAMEN | CONSULTA

        // ----------- Fecha y hora -----------
        [Required]
        [Column("fechahora")]
        public DateTime FechaHora { get; set; }

        // ----------- Motivo -----------
        [Required, StringLength(200)]
        [Column("motivo")]
        public string Motivo { get; set; }

        // ----------- NUEVO: Estado como texto -----------
        [Required]
        [Column("estado")]
        [StringLength(20)]
        public string Estado { get; set; } = "Activo";
        // Valores sugeridos: Activo, Cancelado, Pendiente

        [StringLength(300)]
        [Column("notasconsulta")]
        public string NotasConsulta { get; set; }

        // ----------- Relación con Paciente -----------
        [ForeignKey("Paciente")]
        [Column("idpaciente")]
        [Required]
        public int IdPaciente { get; set; }

        [JsonIgnore]
        public Paciente? Paciente { get; set; }

        // ----------- Relación con Doctor -----------
        // AHORA ES OPCIONAL (solo requerido en CONSULTA)
        [ForeignKey("Doctor")]
        [Column("iddoctor")]
        public int? IdDoctor { get; set; }

        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        // ----------- Relación con Exámenes -----------
        public ICollection<Examen>? Examenes { get; set; }



        // -------------------- MÉTODOS DE DOMINIO --------------------

        public bool EstaActiva() => Estado == "Activo";

        public void Cancelar() => Estado = "Cancelado";

        public void Reactivar() => Estado = "Activo";

        public bool EsCitaFutura() => FechaHora > DateTime.Now;

        public string ObtenerResumen()
        {
            return $"Cita #{Id}: {Motivo} - {FechaHora:dd/MM/yyyy HH:mm} ({Estado}) [{TipoCita}]";
        }

        // -------------------- REGLA DE LÓGICA AUTOMÁTICA --------------------

        /// <summary>
        /// Valida reglas segun tipo de cita.
        /// </summary>
        public void Validar()
        {
            if (TipoCita == "CONSULTA" && IdDoctor == null)
                throw new Exception("Debe seleccionar un doctor para una consulta.");

            if (TipoCita == "EXAMEN")
                IdDoctor = null; // Nunca debe tener doctor
        }
    }
}
