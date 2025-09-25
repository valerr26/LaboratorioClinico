using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Column("idexamen")]
        [Required ]
        public int IdExamen { get; set; }
        [ForeignKey("Examen")]
        public Examen Examen { get; set; }

        [Column("iddoctor")]
        [Required ]
        public int IdDoctor { get; set; }

        [ForeignKey("Doctor")]
        public Doctor Doctor { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }
    }
}
