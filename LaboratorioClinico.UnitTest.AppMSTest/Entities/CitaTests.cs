using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass()]
    public class CitaTests
    {
        [TestMethod]
        public void EstaActiva_DeberiaRetornarTrueSiEstadoEsActivo()
        {
            var cita = new Cita { Estado = "ACTIVO" };
            Assert.IsTrue(cita.EstaActiva());
        }

        [TestMethod]
        public void Cancelar_DeberiaCambiarEstadoAInactivo()
        {
            var cita = new Cita { Estado = "ACTIVO" };
            cita.Cancelar();
            Assert.AreEqual("INACTIVO", cita.Estado);
        }

        [TestMethod]
        public void Reactivar_DeberiaCambiarEstadoAActivo()
        {
            var cita = new Cita { Estado = "INACTIVO" };
            cita.Reactivar();
            Assert.AreEqual("ACTIVO", cita.Estado);
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
                Estado = "ACTIVO"
            };

            var resumen = cita.ObtenerResumen();

            Assert.AreEqual("Cita #1: Consulta general - 10/10/2025 09:30 (Activa)", resumen);
        }
    }
}
