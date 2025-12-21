using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Infrastructure.Repositories;

namespace RoomFlow.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IChangeLogRepository _changeLogRepository;
		private readonly IReservationRepository _bookingRepository;
		private readonly IRoomRepository _roomRepository;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			_employeeRepository = new EmployeeRepository(_context);
			_changeLogRepository = new ChangeLogRepository(_context);
			_bookingRepository = new ReservationRepository(_context);
			_roomRepository = new RoomRepository(_context);
		}

		public IEmployeeRepository Employees => _employeeRepository;

        public IChangeLogRepository ChangeLogs => _changeLogRepository;

		public IReservationRepository Bookings => _bookingRepository;

        public IRoomRepository Rooms => _roomRepository;

        public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
