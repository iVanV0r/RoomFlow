using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Interfaces
{
	public interface IUnitOfWork
	{
		IEmployeeRepository Employees { get; }
		IChangeLogRepository ChangeLogs { get; }
		IReservationRepository Bookings { get; }
		IRoomRepository Rooms { get; }
		Task<int> SaveChangesAsync();
	}
}
