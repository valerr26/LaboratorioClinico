using LaboratorioClinico.Domain.Entities;
using Xunit;

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
        public void Activar_DeberiaCambiarEstadoAActivo()
        {
            // Arrange
            var rol = new Rol { Estado = "INACTIVO" };

            // Act
            rol.Activar();

            // Assert
            Assert.Equal("ACTIVO", rol.Estado);
        }

        [Fact]
        public void Desactivar_DeberiaCambiarEstadoAInactivo()
        {
            // Arrange
            var rol = new Rol { Estado = "ACTIVO" };

            // Act
            rol.Desactivar();

            // Assert
            Assert.Equal("INACTIVO", rol.Estado);
        }

        [Fact]
        public void EstaActivo_DeberiaRetornarTrueSiRolEstaActivo()
        {
            // Arrange
            var rol = new Rol { Estado = "ACTIVO" };

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
                Estado = "INACTIVO"
            };

            // Act
            var resumen = rol.ObtenerResumen();

            // Assert
            Assert.Equal("Rol: Recepcionista (Inactivo) - Atiende pacientes y coordina citas", resumen);
        }
    }
}
