using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass()]
    public class CitaTests
    {
        [TestMethod]
        public void EstaActiva_DeberiaRetornarTrueSiEstadoEsTrue()
        {
            var cita = new Cita { Estado = true };
            Assert.IsTrue(cita.EstaActiva());
        }

        [TestMethod]
        public void Cancelar_DeberiaCambiarEstadoAFalse()
        {
            var cita = new Cita { Estado = true };
            cita.Cancelar();
            Assert.IsFalse(cita.Estado);
        }

        [TestMethod]
        public void Reactivar_DeberiaCambiarEstadoATrue()
        {
            var cita = new Cita { Estado = false };
            cita.Reactivar();
            Assert.IsTrue(cita.Estado);
        }

        [TestMethod]
        public void EsCitaFutura_DeberiaRetornarTrueSiFechaMayorAHoy()
        {
            var cita = new Cita { FechaHora = DateTime.Now.AddDays(1) };
            Assert.IsTrue(cita.EsCitaFutura());
        }

        [TestMethod]
        public void EsCitaFutura_DeberiaRetornarFalseSiFechaPasada()
        {
            var cita = new Cita { FechaHora = DateTime.Now.AddDays(-1) };
            Assert.IsFalse(cita.EsCitaFutura());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirMotivoFechaYEstado()
        {
            var cita = new Cita
            {
                Id = 1,
                Motivo = "Consulta general",
                FechaHora = new DateTime(2025, 10, 10, 9, 30, 0),
                Estado = true
            };
            var resumen = cita.ObtenerResumen();
            Assert.AreEqual("Cita #1: Consulta general - 10/10/2025 09:30 (Activa)", resumen);
        }
    }
}