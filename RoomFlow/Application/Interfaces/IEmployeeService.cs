using RoomFlow.Models;

namespace RoomFlow.Application.Interfaces
{
	public interface IEmployeeService
	{
		Task<List<Employee>> GetAllAsync();
		Task<Employee?> GetByIdAsync(int id);
		Task AddAsync(Employee employee);
		Task UpdateAsync(Employee employee);
		Task DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}
