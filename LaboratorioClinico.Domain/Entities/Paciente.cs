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

            // Relación con Usuario (N:1)
            [Required]
            [Column("idusuario")]
            public int IdUsuario { get; set; }

            [ForeignKey(nameof(IdUsuario))]
            public Usuario Usuario { get; set; }

            [Required]
            [Column("estado")]
            public bool Estado { get; set; }

            // 🔹 Relación con Doctor
            [Column("iddoctor")]
            public int IdDoctor { get; set; }

            [ForeignKey(nameof(IdDoctor))]
            public Doctor Doctor { get; set; }

        // Relación: un paciente puede tener muchas citas
        public ICollection<Cita>? Citas { get; set; }

            // Relación: un paciente puede tener muchos exámenes
            public ICollection<Examen>? Examenes { get; set; }
        }
    }
