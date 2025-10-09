using LaboratorioClinico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.UnitTest.AppxUnit
{
    public class RolTests
    {
        [Fact]
        public void ObtenerDescripcionCompleta_DeberiaRetornarNombreYDescripcion()
        {
            // Arrange
            var rol = new Rol
            {
                Nombre = "Administrador",
                Descripcion = "Acceso total al sistema"
            };

            // Act
            var resultado = rol.ObtenerDescripcionCompleta();

            // Assert
            Assert.Equal("Administrador - Acceso total al sistema", resultado);
        }

        [Fact]
        public void Activar_DeberiaCambiarEstadoATrue()
        {
            // Arrange
            var rol = new Rol { Estado = false };

            // Act
            rol.Activar();

            // Assert
            Assert.True(rol.Estado);
        }

        [Fact]
        public void Desactivar_DeberiaCambiarEstadoAFalse()
        {
            // Arrange
            var rol = new Rol { Estado = true };

            // Act
            rol.Desactivar();

            // Assert
            Assert.False(rol.Estado);
        }

        [Fact]
        public void EstaActivo_DeberiaRetornarTrueSiRolEstaActivo()
        {
            // Arrange
            var rol = new Rol { Estado = true };

            // Act
            var resultado = rol.EstaActivo();

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void ObtenerResumen_DeberiaIncluirNombreDescripcionYEstado()
        {
            // Arrange
            var rol = new Rol
            {
                Nombre = "Recepcionista",
                Descripcion = "Atiende pacientes y coordina citas",
                Estado = false
            };

            // Act
            var resumen = rol.ObtenerResumen();

            // Assert
            Assert.Equal("Rol: Recepcionista (Inactivo) - Atiende pacientes y coordina citas", resumen);
        }
    }
}
