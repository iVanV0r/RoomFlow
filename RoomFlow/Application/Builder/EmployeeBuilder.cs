using RoomFlow.Application.Builder.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Application.Builder
{
	public class EmployeeBuilder : IEmployeeBuilder
	{
		private readonly Employee _employee = new();

		public IEmployeeBuilder SetFullName(string fullName)
		{
			_employee.FullName = fullName.Trim();
			return this;
		}

		public IEmployeeBuilder SetPosition(string position)
		{
			_employee.Position = position.Trim();
			return this;
		}

		public IEmployeeBuilder SetSalary(decimal salary)
		{
			_employee.Salary = salary;
			return this;
		}

		public IEmployeeBuilder SetHireDate(DateTime hireDate)
		{
			_employee.HireDate = hireDate;
			return this;
		}

		public IEmployeeBuilder SetEmail(string email)
		{
			_employee.Email = email;
			return this;
		}

		public IEmployeeBuilder SetPhone(string phone)
		{
			_employee.Phone = phone;
			return this;
		}

		public Employee Build()
		{
			return _employee;
		}
	}
}
