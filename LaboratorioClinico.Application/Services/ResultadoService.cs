using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // ✔ Obtener solo resultados activos (que NO estén Anulados)
        public async Task<IEnumerable<Resultado>> ObtenerResultadosActivosAsync()
        {
            var resultados = await _repository.GetResultadosAsync();
            return resultados.Where(r => r.Estado != "Anulado");
        }

        // ✔ Obtener resultado por ID si no está Anulado
        public async Task<Resultado?> ObtenerResultadoPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var resultado = await _repository.GetResultadoByIdAsync(id);

            return (resultado != null && resultado.Estado != "Anulado")
                ? resultado
                : null;
        }

        // ✔ Agregar resultado (Estado por defecto = "Validado")
        public async Task<string> AgregarResultadoAsync(Resultado resultado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(resultado.Detalle))
                    return "Error: El detalle del resultado es obligatorio";

               if (resultado.IdPaciente <= 0)
                   return "Error: El paciente es obligatorio";

                // Estado inicial
                resultado.Estado = "Validado"; // o "Entregado"

                var insertado = await _repository.AddResultadoAsync(resultado);

                return insertado != null
                    ? "Resultado agregado correctamente"
                    : "Error: No se pudo agregar el resultado";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ✔ Modificar resultado (incluye estado e IdPaciente)
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
            existente.IdPaciente = resultado.IdPaciente;

            // Estado string (Validado / Entregado / Anulado)
            existente.Estado = resultado.Estado;

            var actualizado = await _repository.UpdateResultadoAsync(existente);

            return actualizado != null
                ? "Resultado modificado correctamente"
                : "Error: No se pudo actualizar el resultado";
        }

        // ✔ Eliminación lógica usando Estado = "Anulado"
        public async Task<string> EliminarResultadoAsync(int id)
        {
            var resultado = await _repository.GetResultadoByIdAsync(id);

            if (resultado == null || resultado.Estado == "Anulado")
                return "Error: Resultado no encontrado";

            resultado.Estado = "Anulado"; 
            await _repository.UpdateResultadoAsync(resultado);

            return "Resultado eliminado correctamente";
        }
    }
}
