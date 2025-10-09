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
        public ICollection<Usuario>? Usuarios { get; set; }

        // ✅ MÉTODOS CON LÓGICA PARA PRUEBAS UNITARIAS

        /// <summary>
        /// Devuelve una descripción completa combinando nombre y descripción.
        /// </summary>
        public string ObtenerDescripcionCompleta()
        {
            return $"{Nombre} - {Descripcion}";
        }

        /// <summary>
        /// Activa el rol (cambia Estado a true).
        /// </summary>
        public void Activar()
        {
            Estado = true;
        }

        /// <summary>
        /// Desactiva el rol (cambia Estado a false).
        /// </summary>
        public void Desactivar()
        {
            Estado = false;
        }

        /// <summary>
        /// Verifica si el rol está activo.
        /// </summary>
        public bool EstaActivo()
        {
            return Estado;
        }

        /// <summary>
        /// Devuelve un resumen legible del rol (para logs o vistas).
        /// </summary>
        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"Rol: {Nombre} ({estadoTexto}) - {Descripcion}";
        }


    }
}

