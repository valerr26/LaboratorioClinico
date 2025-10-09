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
    public class DoctorTests
    {
        [TestMethod]
        public void ObtenerNombreCompleto_DeberiaConcatenarNombreYApellido()
        {
            var doctor = new Doctor { Nombre = "María", Apellido = "Ayala" };
            var resultado = doctor.ObtenerNombreCompleto();
            Assert.AreEqual("María Ayala", resultado);
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var doctor = new Doctor { Estado = false };
            doctor.Activar();
            Assert.IsTrue(doctor.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var doctor = new Doctor { Estado = true };
            doctor.Desactivar();
            Assert.IsFalse(doctor.Estado);
        }

        [TestMethod]
        public void TieneLicenciaValida_DeberiaRetornarTrueSiLicenciaNoEsVacia()
        {
            var doctor = new Doctor { LicenciaMedica = "LIC-12345" };
            Assert.IsTrue(doctor.TieneLicenciaValida());
        }

        [TestMethod]
        public void PuedeAtender_DeberiaRetornarTrueSiActivoYLicenciaValida()
        {
            var doctor = new Doctor { Estado = true, LicenciaMedica = "LIC-9876" };
            Assert.IsTrue(doctor.PuedeAtender());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirEspecialidadYEstado()
        {
            var doctor = new Doctor
            {
                Nombre = "Juan",
                Apellido = "Martínez",
                Especialidad = "Pediatría",
                Estado = true,
                LicenciaMedica = "LIC-2025"
            };

            var resumen = doctor.ObtenerResumen();
            Assert.AreEqual("Dr. Juan Martínez - Pediatría (Activo) | Licencia: LIC-2025", resumen);
        }
    }
}