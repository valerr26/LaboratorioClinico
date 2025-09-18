using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_usuario")]
    public class Usuario
    {
        [Key]
        [Column("idusuario")]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required, StringLength(100)]
        [Column("password")]
        public string Password { get; set; }

        [Column("idrol")]
        [ForeignKey("Rol")]
        public int IdRol { get; set; }
    }
}
