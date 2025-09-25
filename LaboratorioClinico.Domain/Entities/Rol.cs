using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_rol")]
    public class Rol
    {
        [Key]
        [Column("idrol")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(200)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // Relación inversa: un rol puede tener muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; }

    }
}

