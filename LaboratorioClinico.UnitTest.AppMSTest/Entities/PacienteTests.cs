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
    public class PacienteTests
    {
        [TestMethod]
        public void ObtenerNombreCompleto_DeberiaConcatenarNombreYApellido()
        {
            var paciente = new Paciente { Nombre = "Juan", Apellido = "Pérez" };
            Assert.AreEqual("Juan Pérez", paciente.ObtenerNombreCompleto());
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var paciente = new Paciente { Estado = false };
            paciente.Activar();
            Assert.IsTrue(paciente.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var paciente = new Paciente { Estado = true };
            paciente.Desactivar();
            Assert.IsFalse(paciente.Estado);
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
                Estado = true
            };
            var resumen = paciente.ObtenerResumen();
            Assert.AreEqual("Paciente: Ana Gómez (Activo) - Nacido el 2005/02/05", resumen);
        }
    }
}