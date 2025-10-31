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
    [Table("t_resultado")]
    public class Resultado
    {
        [Key]
        [Column("idresultado")]
        public int Id { get; set; }

        [Required, StringLength(500)]
        [Column("detalle")]
        public string Detalle { get; set; }

        [Required]
        [Column("fechaemision")]
        public DateTime FechaEmision { get; set; }

        // Relación con Examen (N:1)
        [Required]
        [Column("idexamen")]
        [ForeignKey("Examen")]
        public int IdExamen { get; set; }
        [JsonIgnore]
        public Examen? Examen { get; set; }

        // Relación con Doctor (N:1)
        [Required]
        [Column("iddoctor")]
        [ForeignKey("Doctor")]
        public int IdDoctor { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // ✅ MÉTODOS CON LÓGICA PARA PRUEBAS UNITARIAS

        /// <summary>
        /// Activa el resultado.
        /// </summary>
        public void Activar()
        {
            Estado = true;
        }

        /// <summary>
        /// Desactiva el resultado.
        /// </summary>
        public void Desactivar()
        {
            Estado = false;
        }

        /// <summary>
        /// Verifica si el resultado está activo.
        /// </summary>
        public bool EstaActivo()
        {
            return Estado;
        }

        /// <summary>
        /// Determina si el resultado es reciente (emitido en los últimos 7 días).
        /// </summary>
        public bool EsReciente()
        {
            return FechaEmision >= DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// Obtiene un resumen legible del resultado.
        /// </summary>
        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"Resultado #{Id}: {Detalle} - Emitido el {FechaEmision:dd/MM/yyyy} [{estadoTexto}]";
        }
    }
}
