using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required, MaxLength(100)]
        [Column("password")]
        public string PasswordHash { get; set; } = string.Empty;

        [NotMapped]
        public string? Password { get; set; }

        [Required]
        [Column("idrol")]
        public int IdRol { get; set; }

        public Rol? Rol { get; set; }

        // ----------- CAMBIO AQUÍ: Estado es STRING -----------
        [Required, StringLength(20)]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        public ICollection<Doctor>? Doctores { get; set; }


        // ---------- MÉTODOS ----------

        public void Activar() => Estado = "Activo";

        public void Inactivar() => Estado = "Inactivo";

        public void Bloquear() => Estado = "Bloqueado";

        public void MarcarPendiente() => Estado = "Pendiente";

        public bool EstaActivo() => Estado == "Activo";

        public string ObtenerResumen()
        {
            return $"Usuario: {Username} ({Estado}) | Rol: {Rol?.Nombre ?? "Sin rol"}";
        }
    }
}
