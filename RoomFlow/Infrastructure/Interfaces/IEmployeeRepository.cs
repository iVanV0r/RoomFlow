using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Interfaces
{
	public interface IEmployeeRepository
	{
		Task<List<Employee>> GetAllAsync();
		Task<Employee?> GetByIdAsync(int id);
		Task AddAsync(Employee employee);
		void Update(Employee employee);
		void Remove(Employee employee);
		Task<bool> ExistsAsync(int id);
	}
}
