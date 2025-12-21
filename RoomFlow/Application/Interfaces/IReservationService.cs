using RoomFlow.Models;

namespace RoomFlow.Application.Interfaces
{
    public interface IReservationService
    {
		Task<List<Reservation>> GetAllAsync();
		Task AddAsync(Reservation booking);
		Task<Reservation> GetByIdAsync(int id);
		Task UpdateAsync(Reservation booking);
		Task DeleteAsync(int id);
	}
}
