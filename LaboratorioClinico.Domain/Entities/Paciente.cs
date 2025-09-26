using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_paciente")]
    public class Paciente
    {
        [Key]
        [Column("idpaciente")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required, StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [Column("fechanacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [StringLength(200)]
        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("idusuario")]
        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("idusuario")]
        public Usuario  Usuario { get; set; }

        [Column("idcita")]
        [Required]
        public int  IdCita  { get; set; }

        [ForeignKey("idcita")]
        public Cita Cita { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // Relación inversa: un rol puede tener muchas Citas
        public ICollection<Cita>? Citas { get; set; }

    }
}
