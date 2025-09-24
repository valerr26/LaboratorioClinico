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

        public async Task<IEnumerable<Resultado>> ObtenerResultadosAsync()
        {
            return await _repository.GetResultadosAsync();
        }

        public async Task<Resultado?> ObtenerResultadoPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repository.GetResultadoByIdAsync(id);
        }

        public async Task<string> AgregarResultadoAsync(Resultado resultado)
        {
            if (string.IsNullOrWhiteSpace(resultado.Detalle))
                return "Error: El detalle del resultado es obligatorio";

            var resultadoInsertado = await _repository.AddResultadoAsync(resultado);
            return resultadoInsertado != null ? "Resultado agregado correctamente" : "Error: No se pudo agregar el resultado";
        }

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

            var actualizado = await _repository.UpdateResultadoAsync(existente);
            return actualizado != null ? "Resultado modificado correctamente" : "Error: No se pudo modificar el resultado";
        }

        public async Task<string> EliminarResultadoAsync(int id)
        {
            var resultado = await _repository.DeleteResultadoAsync(id);
            return resultado ? "Resultado eliminado correctamente" : "Error: No se encontró el resultado";
        }
    }
}
