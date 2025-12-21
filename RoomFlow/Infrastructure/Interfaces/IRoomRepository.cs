using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Interfaces
{
    public interface IRoomRepository
    {
		Task<List<Room>> GetAllAsync();
		Task<List<Room>> GetAvailableRoomsAsync();
		Task<Room?> GetByIdAsync(int id);
		Task AddAsync(Room room);
		void Update(Room room);
		void Remove(Room room);
		Task<bool> ExistsAsync(int id);
	}
}
