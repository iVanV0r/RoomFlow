using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Interfaces
{
    public interface IReservationRepository
    {
		Task<List<Reservation>> GetAllAsync();
		Task<Reservation?> GetByIdAsync(int id);
		Task AddAsync(Reservation booking);
		void Update(Reservation booking);
		void Remove(Reservation booking);
		Task<bool> ExistsAsync(int id);
	}
}
