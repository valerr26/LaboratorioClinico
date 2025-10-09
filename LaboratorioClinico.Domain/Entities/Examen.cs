using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_examen")]
    public class Examen
    {
        [Key]
        [Column("idexamen")]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Column("tipoexamen")]
        public string TipoExamen { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required, StringLength(200)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        // Relación con Paciente (N:1)
        [Required]
        [Column("idpaciente")]
        public int IdPaciente { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        public Paciente Paciente { get; set; }

        // Relación con Cita (N:1)
        [Required]
        [Column("idcita")]
        public int IdCita { get; set; }

        [ForeignKey(nameof(IdCita))]
        public Cita Cita { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        // ✅ MÉTODOS PARA PRUEBAS UNITARIAS Y DOMINIO

        /// <summary>
        /// Verifica si el examen está activo.
        /// </summary>
        public bool EstaActivo()
        {
            return Estado;
        }

        /// <summary>
        /// Activa el examen.
        /// </summary>
        public void Activar()
        {
            Estado = true;
        }

        /// <summary>
        /// Desactiva el examen.
        /// </summary>
        public void Desactivar()
        {
            Estado = false;
        }

        /// <summary>
        /// Determina si el examen es reciente (realizado en los últimos 7 días).
        /// </summary>
        public bool EsReciente()
        {
            return Fecha >= DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// Obtiene un resumen legible del examen.
        /// </summary>
        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"Examen #{Id}: {TipoExamen} - {Descripcion} ({Fecha:dd/MM/yyyy}) [{estadoTexto}]";
        }
    }
}