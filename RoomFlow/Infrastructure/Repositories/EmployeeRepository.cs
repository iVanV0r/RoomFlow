using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _context;

		public EmployeeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Employee>> GetAllAsync()
		{
			return await _context.Employees
				.AsNoTracking() // для чтения без отслеживания изменений
				.ToListAsync();
		}

		public async Task<Employee?> GetByIdAsync(int id)
		{
			return await _context.Employees
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task AddAsync(Employee employee)
		{
			await _context.Employees.AddAsync(employee);
		}

		public void Update(Employee employee)
		{
			_context.Employees.Update(employee);
		}

		public void Remove(Employee employee)
		{
			_context.Employees.Remove(employee);
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _context.Employees.AnyAsync(e => e.Id == id);
		}
	}
}
