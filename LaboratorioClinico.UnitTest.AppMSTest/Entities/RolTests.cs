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
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var rol = new Rol { Estado = false };
            rol.Activar();
            Assert.IsTrue(rol.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var rol = new Rol { Estado = true };
            rol.Desactivar();
            Assert.IsFalse(rol.Estado);
        }

        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiElRolEstaActivo()
        {
            var rol = new Rol { Estado = true };
            Assert.IsTrue(rol.EstaActivo());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirNombreYEstado()
        {
            var rol = new Rol { Nombre = "Recepcionista", Descripcion = "Atiende pacientes", Estado = false };
            var resumen = rol.ObtenerResumen();
            Assert.AreEqual("Rol: Recepcionista (Inactivo) - Atiende pacientes", resumen);
        }
    }
}