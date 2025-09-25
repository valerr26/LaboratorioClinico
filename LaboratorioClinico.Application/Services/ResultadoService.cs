using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class ResultadoService
    {
        private readonly IResultadoRepository _repository;

        public ResultadoService(IResultadoRepository repository)
        {
            _repository = repository;
        }

        // Obtener resultados activos
        public async Task<IEnumerable<Resultado>> ObtenerResultadosActivosAsync()
        {
            var resultados = await _repository.GetResultadosAsync();
            return resultados.Where(r => r.Estado);
        }

        // Obtener resultado por Id (si está activo)
        public async Task<Resultado?> ObtenerResultadoPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var resultado = await _repository.GetResultadoByIdAsync(id);
            return (resultado != null && resultado.Estado) ? resultado : null;
        }

        // Agregar resultado
        public async Task<string> AgregarResultadoAsync(Resultado nuevoResultado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoResultado.Detalle))
                    return "Error: El detalle del resultado es obligatorio";

                nuevoResultado.Estado = true; // Activo por defecto
                var resultadoInsertado = await _repository.AddResultadoAsync(nuevoResultado);

                if (resultadoInsertado == null || resultadoInsertado.Id <= 0)
                    return "Error: No se pudo agregar el resultado";

                return "Resultado agregado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Modificar resultado
        public async Task<string> ModificarResultadoAsync(Resultado resultado)
        {
            if (resultado.Id <= 0)
                return "Error: Id de resultado no válido";

            var existente = await _repository.GetResultadoByIdAsync(resultado.Id);
            if (existente == null)
                return "Error: Resultado no encontrado";

            existente.Detalle = resultado.Detalle;
            existente.FechaEmision = resultado.FechaEmision;
            existente.IdExamen = resultado.IdExamen;
            existente.IdDoctor = resultado.IdDoctor;
            existente.Estado = resultado.Estado;

            var actualizado = await _repository.UpdateResultadoAsync(existente);

            return actualizado != null ? "Resultado modificado correctamente" : "Error: No se pudo actualizar el resultado";
        }

        // Eliminar resultado (borrado lógico)
        public async Task<string> EliminarResultadoAsync(int id)
        {
            var resultado = await _repository.GetResultadoByIdAsync(id);
            if (resultado == null || !resultado.Estado)
                return "Error: Resultado no encontrado";

            resultado.Estado = false;
            await _repository.UpdateResultadoAsync(resultado);

            return "Resultado eliminado correctamente";
        }
    }
}