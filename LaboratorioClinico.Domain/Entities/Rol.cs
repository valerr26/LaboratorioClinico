using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required, StringLength(20)]
        [Column("estado")]
        public string Estado { get; set; } = "ACTIVO"; // ⬅️ Valor inicial

        public ICollection<Usuario>? Usuarios { get; set; }

        // ----------- MÉTODOS LÓGICOS -----------

        public void Activar() => Estado = "ACTIVO";

        public void Desactivar() => Estado = "INACTIVO";

        public bool EstaActivo() => Estado == "ACTIVO";

        public string ObtenerDescripcionCompleta()
        {
            return $"{Nombre} - {Descripcion}";
        }

        public string ObtenerResumen()
        {
            return $"Rol: {Nombre} ({Estado}) - {Descripcion}";
        }
    }
}
