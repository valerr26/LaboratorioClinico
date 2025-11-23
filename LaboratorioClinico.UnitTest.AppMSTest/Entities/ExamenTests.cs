using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass]
    public class ExamenTests
    {
        [TestMethod]
        public void ObtenerEstado_DeberiaRetornarElEstadoActual()
        {
            var examen = new Examen { Estado = Examen.EnProceso };
            Assert.AreEqual("En proceso", examen.ObtenerEstado());
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoAPendiente()
        {
            var examen = new Examen { Estado = Examen.Cancelado };
            examen.Activar();
            Assert.AreEqual(Examen.Pendiente, examen.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoACancelado()
        {
            var examen = new Examen { Estado = Examen.Pendiente };
            examen.Desactivar();
            Assert.AreEqual(Examen.Cancelado, examen.Estado);
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarTrueSiFechaDentroDeUltimos7Dias()
        {
            var examen = new Examen { Fecha = DateTime.Now.AddDays(-3) };
            Assert.IsTrue(examen.EsReciente());
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarFalseSiFechaAnteriorA7Dias()
        {
            var examen = new Examen { Fecha = DateTime.Now.AddDays(-10) };
            Assert.IsFalse(examen.EsReciente());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaRetornarTextoCorrecto()
        {
            var examen = new Examen
            {
                Id = 1,
                TipoExamen = "Orina",
                Descripcion = "Examen general de orina",
                Fecha = new DateTime(2025, 10, 8),
                Estado = Examen.Completado
            };

            var resumen = examen.ObtenerResumen();

            Assert.AreEqual(
                "Examen #1: Orina - Examen general de orina (08/10/2025) [Completado]",
                resumen
            );
        }
    }
}
