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

        // Relación con Rol
        [Required]
        [ForeignKey("Rol")]
        [Column("idrol")]
        public int IdRol { get; set; }

        public Rol Rol { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // ✅ MÉTODOS CON LÓGICA PARA PRUEBAS UNITARIAS

        public void Activar()
        {
            Estado = true;
        }

        public void Desactivar()
        {
            Estado = false;
        }

        public bool EstaActivo()
        {
            return Estado;
        }

        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"Usuario: {Username} ({estadoTexto}) | Rol: {Rol?.Nombre ?? "Sin rol"}";
        }
    }
}