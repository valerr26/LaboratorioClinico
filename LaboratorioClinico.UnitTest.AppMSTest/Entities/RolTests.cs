using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;
using System;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass()]
    public class RolTests
    {
        [TestMethod]
        public void ObtenerDescripcionCompleta_DeberiaDevolverNombreYDescripcion()
        {
            var rol = new Rol { Nombre = "Administrador", Descripcion = "Acceso total" };
            var resultado = rol.ObtenerDescripcionCompleta();
            Assert.AreEqual("Administrador - Acceso total", resultado);
        }

        [TestMethod]
        public void Activar_DeberiaCambiarEstadoAActivo()
        {
            var rol = new Rol { Estado = "INACTIVO" };
            rol.Activar();
            Assert.AreEqual("ACTIVO", rol.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAInactivo()
        {
            var rol = new Rol { Estado = "ACTIVO" };
            rol.Desactivar();
            Assert.AreEqual("INACTIVO", rol.Estado);
        }

        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiElRolEstaActivo()
        {
            var rol = new Rol { Estado = "ACTIVO" };
            Assert.IsTrue(rol.EstaActivo());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirNombreYEstado()
        {
            var rol = new Rol
            {
                Nombre = "Recepcionista",
                Descripcion = "Atiende pacientes",
                Estado = "INACTIVO"
            };

            var resumen = rol.ObtenerResumen();

            Assert.AreEqual("Rol: Recepcionista (Inactivo) - Atiende pacientes", resumen);
        }
    }
}
