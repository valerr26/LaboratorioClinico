using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities
{
    [Table("t_cita")]
    public class Cita
    {
        [Key]
        [Column("idcita")]
        public int Id { get; set; }

        [Required]
        [Column("fechahora")]
        public DateTime FechaHora { get; set; }

        [Required, StringLength(200)]
        [Column("motivo")]
        public string Motivo { get; set; }

        [Required]
        [Column("estado")]
        public bool Estado { get; set; }

        [StringLength(300)]
        [Column("notasconsulta")]
        public string NotasConsulta { get; set; }

        // 🔹 Relación con Paciente
        [Column("idpaciente")]
        [Required]
        public int IdPaciente { get; set; }        
        public Paciente Paciente { get; set; }

        // 🔹 Relación con Doctor
        [Column("iddoctor")]
        [Required]
        public int IdDoctor { get; set; }
        public Doctor Doctor { get; set; }


        // 🔹 Relación 1:N con Resultados
        public ICollection<Examen>? Examenes { get; set; }

        // ✅ MÉTODOS PARA PRUEBAS UNITARIAS Y LÓGICA DE DOMINIO

        /// <summary>
        /// Retorna true si la cita está activa.
        /// </summary>
        public bool EstaActiva()
        {
            return Estado;
        }

        /// <summary>
        /// Cancela la cita (cambia estado a false).
        /// </summary>
        public void Cancelar()
        {
            Estado = false;
        }

        /// <summary>
        /// Reactiva una cita previamente cancelada.
        /// </summary>
        public void Reactivar()
        {
            Estado = true;
        }

        /// <summary>
        /// Verifica si la cita es futura (fecha mayor a la actual).
        /// </summary>
        public bool EsCitaFutura()
        {
            return FechaHora > DateTime.Now;
        }

        /// <summary>
        /// Obtiene un resumen legible de la cita.
        /// </summary>
        public string ObtenerResumen()
        {
            string estadoTexto = Estado ? "Activa" : "Cancelada";
            return $"Cita #{Id}: {Motivo} - {FechaHora:dd/MM/yyyy HH:mm} ({estadoTexto})";
        }
    }
}