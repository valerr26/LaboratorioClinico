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
    public class ResultadoTests
    {
        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiEstadoEsTrue()
        {
            var resultado = new Resultado { Estado = true };
            Assert.IsTrue(resultado.EstaActivo());
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var resultado = new Resultado { Estado = false };
            resultado.Activar();
            Assert.IsTrue(resultado.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var resultado = new Resultado { Estado = true };
            resultado.Desactivar();
            Assert.IsFalse(resultado.Estado);
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarTrueSiFechaDentroDeUltimos7Dias()
        {
            var resultado = new Resultado { FechaEmision = DateTime.Now.AddDays(-3) };
            Assert.IsTrue(resultado.EsReciente());
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarFalseSiFechaAnteriorA7Dias()
        {
            var resultado = new Resultado { FechaEmision = DateTime.Now.AddDays(-10) };
            Assert.IsFalse(resultado.EsReciente());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirDetalleFechaYEstado()
        {
            var resultado = new Resultado
            {
                Id = 1,
                Detalle = "Examen de sangre completo",
                FechaEmision = new DateTime(2025, 10, 8),
                Estado = true
            };
            var resumen = resultado.ObtenerResumen();
            Assert.AreEqual("Resultado #1: Examen de sangre completo - Emitido el 2025/08/10 [Activo]", resumen);
        }
    }
}