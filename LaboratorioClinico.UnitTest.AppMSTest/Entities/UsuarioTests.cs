using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaboratorioClinico.Domain.Entities;

namespace LaboratorioClinico.Domain.Entities.Tests
{
    [TestClass()]
    public class UsuarioTests
    {
        [TestMethod]
        public void Activar_DeberiaCambiarEstadoAActivo()
        {
            var usuario = new Usuario { Estado = "Inactivo" };
            usuario.Activar();
            Assert.AreEqual("Activo", usuario.Estado);
        }

        [TestMethod]
        public void Inactivar_DeberiaCambiarEstadoAInactivo()
        {
            var usuario = new Usuario { Estado = "Activo" };
            usuario.Inactivar();
            Assert.AreEqual("Inactivo", usuario.Estado);
        }

        [TestMethod]
        public void EstaActivo_DeberiaRetornarTrueSiEstadoEsActivo()
        {
            var usuario = new Usuario { Estado = "Activo" };
            Assert.IsTrue(usuario.EstaActivo());
        }

        [TestMethod]
        public void ObtenerResumen_DeberiaIncluirUsernameYRol()
        {
            var rol = new Rol { Nombre = "Administrador" };

            var usuario = new Usuario
            {
                Username = "jmartinez",
                Estado = "Activo",
                Rol = rol
            };

            var resumen = usuario.ObtenerResumen();

            Assert.AreEqual("Usuario: jmartinez (Activo) | Rol: Administrador", resumen);
        }
    }
}
