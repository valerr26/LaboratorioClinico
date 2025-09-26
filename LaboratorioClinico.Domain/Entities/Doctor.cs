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

        [Column("idusuario")]
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("Usuario")]
        public Usuario Usuario { get; set; }

        [Column("idcita")]
        [Required]
        public int IdCita { get; set; }

        [ForeignKey("cita")]
        public Cita Cita { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        public ICollection<Cita>? Citas { get; set; }

    }
}
