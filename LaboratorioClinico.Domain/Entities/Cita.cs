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

        [Column("idpaciente")]
        [Required]
        public int IdPaciente {  get; set; }

        [ForeignKey("Paciente")]
        public Paciente Paciente { get; set; }

        [Column("iddoctor")]
        [Required]
        public int IdDoctor { get; set; }

        [ForeignKey("Doctor")]
        public Doctor Doctor { get; set; }

        public ICollection<Examen>? Examenes { get; set; }
        public ICollection<Resultado>? Resultados { get; set; }

    }
}

