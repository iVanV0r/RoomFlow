using RoomFlow.Application.Factories.Interfaces;
using RoomFlow.Application.Interfaces;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEntityFactory _factory;

		public EmployeeService(IUnitOfWork unitOfWork, IEntityFactory factory)
		{
			_unitOfWork = unitOfWork;
			_factory = factory;
		}

		public async Task<List<Employee>> GetAllAsync()
		{
			return await _unitOfWork.Employees.GetAllAsync();
		}

		public async Task<Employee?> GetByIdAsync(int id)
		{
			return await _unitOfWork.Employees.GetByIdAsync(id);
		}

		public async Task AddAsync(Employee e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));

			var employee = _factory.CreateEmployee(e.FullName, e.Position, e.Salary, e.HireDate, e.Email, e.Phone);
			await _unitOfWork.Employees.AddAsync(employee);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task UpdateAsync(Employee employee)
		{
			if (employee == null)
				throw new ArgumentNullException(nameof(employee));

			_unitOfWork.Employees.Update(employee);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var employee = await _unitOfWork.Employees.GetByIdAsync(id);
			if (employee == null)
				throw new InvalidOperationException("Сотрудник не найден");

			_unitOfWork.Employees.Remove(employee);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _unitOfWork.Employees.ExistsAsync(id);
		}
	}
}
