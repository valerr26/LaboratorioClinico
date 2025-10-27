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

        [Column("password")]
        [Required, MaxLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        // Campo transitorio: NO va a BD; solo para entrada (registro/login)
        [NotMapped]
        public string? Password { get; set; }   // vendrá en el JSON del request

        [Column("idrol")]
        [Required]
        public int IdRol { get; set; }

       
        public Rol? Rol { get; set; }

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