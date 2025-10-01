using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_doctor")]
    public class Doctor
    {
        [Key]
        [Column("iddoctor")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required, StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required, StringLength(100)]
        [Column("especialidad")]
        public string Especialidad { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required, StringLength(50)]
        [Column("licenciamedica")]
        public string LicenciaMedica { get; set; }

        // Relación con Usuario
        [Required]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }

        // Relación con Citas (1 doctor -> muchas citas)
        public ICollection<Cita> Citas { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }
    }
}