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

        // Relación con Usuario
        [Required]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        
        public Usuario Usuario { get; set; }

        // Relación con Citas (1 doctor -> muchas citas)
        public ICollection<Cita> Citas { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // ✅ MÉTODOS PARA LÓGICA Y PRUEBAS UNITARIAS

        /// <summary>
        /// Devuelve el nombre completo del doctor.
        /// </summary>
        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        /// <summary>
        /// Activa el doctor (cambia Estado a true).
        /// </summary>
        public void Activar()
        {
            Estado = true;
        }

        /// <summary>
        /// Desactiva el doctor (cambia Estado a false).
        /// </summary>
        public void Desactivar()
        {
            Estado = false;
        }

        /// <summary>
        /// Verifica si la licencia médica es válida (no nula ni vacía).
        /// </summary>
        public bool TieneLicenciaValida()
        {
            return !string.IsNullOrWhiteSpace(LicenciaMedica);
        }

        /// <summary>
        /// Determina si el doctor puede atender (activo y con licencia).
        /// </summary>
        public bool PuedeAtender()
        {
            return Estado && TieneLicenciaValida();
        }

        /// <summary>
        /// Devuelve un resumen legible de la información del doctor.
        /// </summary>
        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"Dr. {Nombre} {Apellido} - {Especialidad} ({estadoTexto}) | Licencia: {LicenciaMedica}";
        }
    }
}