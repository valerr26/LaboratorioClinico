using LaboratorioClinico.Domain.Entities;
using LaboratorioClinico.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioClinico.Application.Services
{
    public class ConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository)
        {
            _repository = repository;
        }

        // ✔ Mostrar solo consultas que NO estén canceladas
        public async Task<IEnumerable<Consulta>> ObtenerConsultasActivasAsync()
        {
            var consultas = await _repository.GetConsultasAsync();
            return consultas.Where(c => c.Estado != Consulta.Cancelada);
        }

        // ✔ Obtener consulta por ID solo si no está cancelada
        public async Task<Consulta?> ObtenerConsultaPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var consulta = await _repository.GetConsultaByIdAsync(id);

            return (consulta != null && consulta.Estado != Consulta.Cancelada)
                ? consulta
                : null;
        }

        // ✔ Nueva consulta inicia en estado Activa
        public async Task<string> AgregarConsultaAsync(Consulta consulta)
        {
            if (consulta.IdPaciente <= 0)
                return "Error: Id del paciente es obligatorio";

            if (consulta.IdDoctor <= 0)
                return "Error: Id del doctor es obligatorio";

            if (string.IsNullOrWhiteSpace(consulta.Motivo))
                return "Error: El motivo de la consulta es obligatorio";

            consulta.Estado = Consulta.Activa;

            var consultaInsertada = await _repository.AddConsultaAsync(consulta);

            return consultaInsertada != null
                ? "Consulta agregada correctamente"
                : "Error: No se pudo agregar la consulta";
        }

        // ✔ Modificar consulta (incluye estado string)
        public async Task<string> ModificarConsultaAsync(Consulta consulta)
        {
            if (consulta.Id <= 0)
                return "Error: Id de consulta no válido";

            var existente = await _repository.GetConsultaByIdAsync(consulta.Id);
            if (existente == null)
                return "Error: Consulta no encontrada";

            existente.Motivo = consulta.Motivo;
            existente.Diagnostico = consulta.Diagnostico;
            existente.Tratamiento = consulta.Tratamiento;
            existente.Observaciones = consulta.Observaciones;
            existente.FechaConsulta = consulta.FechaConsulta;
            existente.IdPaciente = consulta.IdPaciente;
            existente.IdDoctor = consulta.IdDoctor;

            // 🔥 Cambiar estado string
            existente.Estado = consulta.Estado;

            var actualizado = await _repository.UpdateConsultaAsync(existente);

            return actualizado != null
                ? "Consulta modificada correctamente"
                : "Error: No se pudo modificar la consulta";
        }

        // ✔ Eliminación lógica usando Estado = "Cancelada"
        public async Task<string> EliminarConsultaAsync(int id)
        {
            var consulta = await _repository.GetConsultaByIdAsync(id);

            if (consulta == null || consulta.Estado == Consulta.Cancelada)
                return "Error: Consulta no encontrada";

            consulta.Estado = Consulta.Cancelada;
            await _repository.UpdateConsultaAsync(consulta);

            return "Consulta eliminada correctamente";
        }
    }
}

