using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Infrastructure.Repositories;

namespace RoomFlow.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmployeeRepository _employeeRepository;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			_employeeRepository = new EmployeeRepository(_context);
		}

		public IEmployeeRepository Employees => _employeeRepository;

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
