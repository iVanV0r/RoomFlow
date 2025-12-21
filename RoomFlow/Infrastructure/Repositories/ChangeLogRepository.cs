using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

public class ChangeLogRepository : IChangeLogRepository
{
	private readonly ApplicationDbContext _context;

	public ChangeLogRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<List<ChangeLog>> GetAllAsync()
	{
		return await _context.ChangeLogs
			.AsNoTracking()               // <-- метод расширения EF Core
			.OrderByDescending(c => c.Timestamp)
			.ToListAsync();               // <-- метод расширения EF Core
	}

	public async Task<ChangeLog?> GetByIdAsync(int id)
	{
		return await _context.ChangeLogs
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task AddAsync(ChangeLog log)
	{
		await _context.ChangeLogs.AddAsync(log);
	}

	public void Update(ChangeLog log)
	{
		_context.ChangeLogs.Update(log);
	}

	public void Remove(ChangeLog log)
	{
		_context.ChangeLogs.Remove(log);
	}

	public async Task<bool> ExistsAsync(int id)
	{
		return await _context.ChangeLogs.AnyAsync(c => c.Id == id);
	}
}
