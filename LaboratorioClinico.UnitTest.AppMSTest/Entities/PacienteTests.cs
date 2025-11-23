using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass]
    public class PacienteTests
    {
        [TestMethod]
        public void ObtenerNombreCompleto_DeberiaConcatenarNombreYApellido()
        {
            var paciente = new Paciente { Nombre = "Juan", Apellido = "Pérez" };
            Assert.AreEqual("Juan Pérez", paciente.ObtenerNombreCompleto());
        }

        [TestMethod]
        public void CambiarEstado_DeberiaCambiarEstadoAActivo()
        {
            var paciente = new Paciente { Estado = "Inactivo" };
            var resultado = paciente.CambiarEstado("Activo");
            Assert.IsTrue(resultado);
            Assert.AreEqual("Activo", paciente.Estado);
        }

        [TestMethod]
        public void CambiarEstado_DeberiaFallarSiEstadoNoPermitido()
        {
            var paciente = new Paciente { Estado = "Activo" };
            var resultado = paciente.CambiarEstado("Desconocido");
            Assert.IsFalse(resultado);
            Assert.AreEqual("Activo", paciente.Estado);
        }

        [TestMethod]
        public void EsMayorEdad_DeberiaRetornarTrueSiPacienteTiene18Omas()
        {
            var paciente = new Paciente { FechaNacimiento = DateTime.Now.AddYears(-20) };
            Assert.IsTrue(paciente.EsMayorEdad());
        }

        [TestMethod]
        public void EsMayorEdad_DeberiaRetornarFalseSiPacienteTieneMenosDe18()
        {
            var paciente = new Paciente { FechaNacimiento = DateTime.Now.AddYears(-15) };
            Assert.IsFalse(paciente.EsMayorEdad());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirNombreYEstado()
        {
            var paciente = new Paciente
            {
                Nombre = "Ana",
                Apellido = "Gómez",
                FechaNacimiento = new DateTime(2005, 5, 20),
                Estado = "Activo"
            };

            var resumen = paciente.ObtenerResumen();

            Assert.AreEqual(
                "Paciente: Ana Gómez | Estado: Activo | Nacido el 20/05/2005",
                resumen
            );
        }
    }
}
