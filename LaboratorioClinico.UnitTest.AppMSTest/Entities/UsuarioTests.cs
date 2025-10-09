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
    public class UsuarioTests
    {
        [TestMethod]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            var usuario = new Usuario { Estado = false };
            usuario.Activar();
            Assert.IsTrue(usuario.Estado);
        }

        [TestMethod]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            var usuario = new Usuario { Estado = true };
            usuario.Desactivar();
            Assert.IsFalse(usuario.Estado);
        }

        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiEstadoEsTrue()
        {
            var usuario = new Usuario { Estado = true };
            Assert.IsTrue(usuario.EstaActivo());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirUsernameYRol()
        {
            var rol = new Rol { Nombre = "Administrador" };
            var usuario = new Usuario
            {
                Username = "jmartinez",
                Estado = true,
                Rol = rol
            };
            var resumen = usuario.ObtenerResumen();
            Assert.AreEqual("Usuario: jmartinez (Activo) | Rol: Administrador", resumen);
        }
    }
}