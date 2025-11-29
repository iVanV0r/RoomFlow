using RoomFlow.Models;

namespace RoomFlow.Application.Builder.Interfaces
{
	public interface IEmployeeBuilder
	{
		IEmployeeBuilder SetFullName(string fullName);
		IEmployeeBuilder SetPosition(string position);
		IEmployeeBuilder SetSalary(decimal salary);
		IEmployeeBuilder SetHireDate(DateTime hireDate);
		IEmployeeBuilder SetEmail(string email);
		IEmployeeBuilder SetPhone(string phone);

		Employee Build(); // возврат итогового объекта
	}
}
