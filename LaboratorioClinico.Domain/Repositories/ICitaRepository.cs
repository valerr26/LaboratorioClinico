using LaboratorioClinico.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaboratorioClinico.Domain.Repositories
{
    public interface ICitaRepository
    {
        // Obtener todas las citas
        Task<IEnumerable<Cita>> GetCitasAsync();

        // Obtener todas las citas activas
        Task<IEnumerable<Cita>> GetCitasActivasAsync();

        // Obtener una cita por su ID
        Task<Cita?> GetCitaByIdAsync(int id);

        // Agregar una nueva cita
        Task<Cita> AddCitaAsync(Cita cita);

        // Actualizar una cita existente
        Task<Cita> UpdateCitaAsync(Cita cita);

        // Eliminar una cita (lógica o física)
        Task<bool> DeleteCitaAsync(int id);

        public interface ICitaRepository
        {
            Task<bool> ExisteCitaAsync(int id);
        }


        // ----------- NUEVO: Para disponibilidad de horarios ----------
        Task<bool> ExisteCitaEnFechaHoraAsync(DateTime fechaHora);

        // Obtener citas por día (para mostrar horas ocupadas)
        Task<IEnumerable<Cita>> GetCitasPorFechaAsync(DateTime fecha);
    }
}
