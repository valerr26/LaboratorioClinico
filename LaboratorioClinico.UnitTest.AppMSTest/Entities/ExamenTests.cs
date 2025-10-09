using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass]
    public class ExamenTests
    {
        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiEstadoEsTrue()
        {
            var examen = new Examen { Estado = true };
            Assert.IsTrue(examen.EstaActivo());
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var examen = new Examen { Estado = false };
            examen.Activar();
            Assert.IsTrue(examen.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var examen = new Examen { Estado = true };
            examen.Desactivar();
            Assert.IsFalse(examen.Estado);
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
        public void ObtenerResumen_DeberiaIncluirTipoDescripcionFechaYEstado()
        {
            var examen = new Examen
            {
                Id = 1,
                TipoExamen = "Orina",
                Descripcion = "Examen general de orina",
                Fecha = new DateTime(2025, 10, 8),
                Estado = true
            };
            var resumen = examen.ObtenerResumen();
            Assert.AreEqual("Examen #1: Orina - Examen general de orina (08/10/2025) [Activo]", resumen);
        }
    }
}