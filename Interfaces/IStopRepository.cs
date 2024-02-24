using Api_AIKO.Models;

namespace Api_AIKO.Interfaces
{
    public interface IStopRepository
    {
        Task<Stop> GetByIdAsync(long id);
        Task<List<Stop>> GetAllAsync();
        Task<Stop> CreateAsync(Stop stop);
        Task UpdateAsync(Stop stop);
        Task DeleteAsync(long id);
    }
}
