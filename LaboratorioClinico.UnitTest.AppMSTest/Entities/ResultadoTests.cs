using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass]
    public class ResultadoTests
    {
        [TestMethod]
        public void ObtenerEstado_DeberiaRetornarElEstadoActual()
        {
            var resultado = new Resultado { Estado = Resultado.Entregado };
            Assert.AreEqual(Resultado.Entregado, resultado.ObtenerEstado());
        }

        [TestMethod]
        public void Validar_DeberiaCambiarEstadoAValidado()
        {
            var resultado = new Resultado { Estado = Resultado.Entregado };
            resultado.Validar();
            Assert.AreEqual(Resultado.Validado, resultado.Estado);
        }

        [TestMethod]
        public void Entregar_DeberiaCambiarEstadoAEntregado()
        {
            var resultado = new Resultado { Estado = Resultado.Validado };
            resultado.Entregar();
            Assert.AreEqual(Resultado.Entregado, resultado.Estado);
        }

        [TestMethod]
        public void Anular_DeberiaCambiarEstadoAAnulado()
        {
            var resultado = new Resultado { Estado = Resultado.Validado };
            resultado.Anular();
            Assert.AreEqual(Resultado.Anulado, resultado.Estado);
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarTrueSiFechaDentroDeUltimos7Dias()
        {
            var resultado = new Resultado
            {
                FechaEmision = DateTime.Now.AddDays(-3)
            };

            Assert.IsTrue(resultado.EsReciente());
        }

        [TestMethod]
        public void EsReciente_DeberiaRetornarFalseSiFechaAnteriorA7Dias()
        {
            var resultado = new Resultado
            {
                FechaEmision = DateTime.Now.AddDays(-10)
            };

            Assert.IsFalse(resultado.EsReciente());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaRetornarTextoCorrecto()
        {
            var resultado = new Resultado
            {
                Id = 1,
                Detalle = "Examen de sangre completo",
                FechaEmision = new DateTime(2025, 10, 8),
                Estado = Resultado.Validado
            };

            var resumen = resultado.ObtenerResumen();

            Assert.AreEqual(
                "Resultado #1: Examen de sangre completo - Emitido el 08/10/2025 [Validado]",
                resumen
            );
        }
    }
}
