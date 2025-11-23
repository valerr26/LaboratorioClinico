using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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

        [Required]
        [ForeignKey("Usuario")]
        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        // Estado STRING validado
        [Required]
        [StringLength(30)]
        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        // Relaciones
        public ICollection<Cita>? Citas { get; set; }
        public ICollection<Consulta>? Consultas { get; set; }

        // --------------------- LISTA DE ESTADOS PERMITIDOS ---------------------
        public static readonly string[] EstadosPermitidos =
        {
            "Activo",
            "Inactivo",
            "De vacaciones",
            "Suspendido",
            "Fuera de horario"
        };

        // ----------------------------- MÉTODOS --------------------------------

        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }

        /// <summary>
        /// Cambia el estado del doctor a uno de los estados permitidos.
        /// </summary>
        public bool CambiarEstado(string nuevoEstado)
        {
            if (!EstadosPermitidos.Contains(nuevoEstado))
                return false; // Estado inválido

            Estado = nuevoEstado;
            return true;
        }

        public bool PuedeAtender()
        {
            return Estado == "Activo" && TieneLicenciaValida();
        }

        public bool TieneLicenciaValida()
        {
            return !string.IsNullOrWhiteSpace(LicenciaMedica);
        }

        public string ObtenerResumen()
        {
            return $"Dr. {Nombre} {Apellido} - {Especialidad} | Estado: {Estado} | Licencia: {LicenciaMedica}";
        }
    }
}
