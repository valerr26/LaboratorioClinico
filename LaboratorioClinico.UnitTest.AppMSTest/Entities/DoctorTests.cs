using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;

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
        public void CambiarEstado_DeberiaCambiarAEstadoValido()
        {
            var doctor = new Doctor { Estado = "Activo" };
            var cambio = doctor.CambiarEstado("Inactivo");

            Assert.IsTrue(cambio);
            Assert.AreEqual("Inactivo", doctor.Estado);
        }

        [TestMethod]
        public void CambiarEstado_NoDebeCambiarSiEstadoNoEsValido()
        {
            var doctor = new Doctor { Estado = "Activo" };
            var cambio = doctor.CambiarEstado("Desconocido");

            Assert.IsFalse(cambio);
            Assert.AreEqual("Activo", doctor.Estado); // No cambia
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
            var doctor = new Doctor
            {
                Estado = "Activo",
                LicenciaMedica = "LIC-9876"
            };

            Assert.IsTrue(doctor.PuedeAtender());
        }

        [TestMethod]
        public void PuedeAtender_DeberiaRetornarFalseSiNoEstaActivo()
        {
            var doctor = new Doctor
            {
                Estado = "Inactivo",
                LicenciaMedica = "LIC-9876"
            };

            Assert.IsFalse(doctor.PuedeAtender());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaGenerarResumenCorrecto()
        {
            var doctor = new Doctor
            {
                Nombre = "Juan",
                Apellido = "Martínez",
                Especialidad = "Pediatría",
                Estado = "Activo",
                LicenciaMedica = "LIC-2025"
            };

            var resumen = doctor.ObtenerResumen();
            Assert.AreEqual("Dr. Juan Martínez - Pediatría | Estado: Activo | Licencia: LIC-2025", resumen);
        }
    }
}
