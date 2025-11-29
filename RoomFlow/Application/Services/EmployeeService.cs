using RoomFlow.Application.Builder.Interfaces;
using RoomFlow.Application.Interfaces;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;
using System.Numerics;

namespace RoomFlow.Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEmployeeBuilder _builder;

		public EmployeeService(IUnitOfWork unitOfWork, IEmployeeBuilder builder)
		{
			_unitOfWork = unitOfWork;
			_builder = builder;
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

			var employee = _builder
				.SetFullName(e.FullName)
				.SetPosition(e.Position)
				.SetSalary(e.Salary)
				.SetEmail(e.Email ?? "")
				.SetPhone(e.Phone ?? "")
				.Build();

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
