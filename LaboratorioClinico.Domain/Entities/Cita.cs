using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_cita")]
    public class Cita
    {
        [Key]
        [Column("idcita")]
        public int Id { get; set; }

        [Required]
        [Column("fechahora")]
        public DateTime FechaHora { get; set; }

        [Required, StringLength(200)]
        [Column("motivo")]
        public string Motivo { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        [StringLength(300)]
        [Column("notasconsulta")]
        public string NotasConsulta { get; set; }

        // 🔹 Relación con Paciente
        [Column("idpaciente")]
        public int IdPaciente { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        public Paciente Paciente { get; set; }

        // 🔹 Relación con Doctor
        [Column("iddoctor")]
        public int IdDoctor { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        public Doctor Doctor { get; set; }


        // 🔹 Relación 1:N con Resultados
        public ICollection<Examen>? Examenes { get; set; }
    }
}