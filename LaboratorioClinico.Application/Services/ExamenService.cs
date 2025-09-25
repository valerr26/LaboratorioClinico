using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class ExamenService
    {
        private readonly IExamenRepository _repository;

        public ExamenService(IExamenRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Examen>> ObtenerExamenesActivosAsync()
        {
            var examenes = await _repository.GetExamenesAsync();
            return examenes.Where(e => e.Estado);
        }

        public async Task<Examen?> ObtenerExamenPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var examen = await _repository.GetExamenByIdAsync(id);
            return (examen != null && examen.Estado) ? examen : null;
        }

        public async Task<string> AgregarExamenAsync(Examen examen)
        {
            if (string.IsNullOrWhiteSpace(examen.TipoExamen))
                return "Error: El tipo de examen es obligatorio";

            if (string.IsNullOrWhiteSpace(examen.Descripcion))
                return "Error: La descripción del examen es obligatoria";

            examen.Estado = true;
            var examenInsertado = await _repository.AddExamenAsync(examen);

            return examenInsertado != null ? "Examen agregado correctamente" : "Error: No se pudo agregar el examen";
        }

        public async Task<string> ModificarExamenAsync(Examen examen)
        {
            if (examen.Id <= 0)
                return "Error: Id de examen no válido";

            var existente = await _repository.GetExamenByIdAsync(examen.Id);
            if (existente == null)
                return "Error: Examen no encontrado";

            existente.TipoExamen = examen.TipoExamen;
            existente.Descripcion = examen.Descripcion;
            existente.Fecha = examen.Fecha;
            existente.IdPaciente = examen.IdPaciente;
            existente.Estado = examen.Estado;

            var actualizado = await _repository.UpdateExamenAsync(existente);

            return actualizado != null ? "Examen modificado correctamente" : "Error: No se pudo modificar el examen";
        }

        public async Task<string> EliminarExamenAsync(int id)
        {
            var examen = await _repository.GetExamenByIdAsync(id);
            if (examen == null || !examen.Estado)
                return "Error: Examen no encontrado";

            examen.Estado = false;
            await _repository.UpdateExamenAsync(examen);

            return "Examen eliminado correctamente";
        }
    }
}