using RoomFlow.Models;

namespace RoomFlow.Application.Interfaces
{
    public interface IChangeLogService
    {
		Task<List<ChangeLog>> GetAllAsync();
		Task<ChangeLog?> GetByIdAsync(int id);
		Task AddAsync(ChangeLog log);
		Task UpdateAsync(ChangeLog log);
		Task DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}
