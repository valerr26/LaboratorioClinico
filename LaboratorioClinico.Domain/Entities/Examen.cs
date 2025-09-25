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
        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }
    }
}