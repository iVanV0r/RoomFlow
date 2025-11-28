namespace RoomFlow.Infrastructure.Interfaces
{
	public interface IUnitOfWork
	{
		IEmployeeRepository Employees { get; }
		Task<int> SaveChangesAsync();
	}
}
