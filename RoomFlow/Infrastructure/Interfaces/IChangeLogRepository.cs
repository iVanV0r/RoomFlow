using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Interfaces
{
    public interface IChangeLogRepository
    {
		Task<List<ChangeLog>> GetAllAsync();
		Task<ChangeLog?> GetByIdAsync(int id);
		Task AddAsync(ChangeLog log);
		void Update(ChangeLog log);
		void Remove(ChangeLog log);
		Task<bool> ExistsAsync(int id);
	}
}
