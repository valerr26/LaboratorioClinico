using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_examen")]
    public class Examen
    {
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

        [Column("idpaciente")]
        [Required]
        public int IdPaciente { get; set; }

        [ForeignKey("Paciente")]
        public Paciente Paciente { get; set; }

        [Column("idCita")]
        [Required]
        public int IdCita { get; set; }

        [ForeignKey("Cita")]
        public Cita Cita { get; set; }

        [Column("idResultado")]
        [Required]
        public int IdResultado { get; set; }

        [ForeignKey("Resultado")]
        public Resultado Resultado { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        public ICollection<Cita>? Citas { get; set; }

    }
}