using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Repositories
{
	public class RoomRepository : IRoomRepository
	{
		private readonly ApplicationDbContext _context;

		public RoomRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Room>> GetAllAsync()
		{
			return await _context.Rooms
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<Room>> GetAvailableRoomsAsync()
		{
			return await _context.Rooms
				.AsNoTracking()
				.Where(r => r.Status == RoomStatus.Available)
				.ToListAsync();
		}

		public async Task<Room?> GetByIdAsync(int id)
		{
			return await _context.Rooms
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task AddAsync(Room room)
		{
			await _context.Rooms.AddAsync(room);
		}

		public void Update(Room room)
		{
			_context.Rooms.Update(room);
		}

		public void Remove(Room room)
		{
			_context.Rooms.Remove(room);
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _context.Rooms.AnyAsync(r => r.Id == id);
		}
	}
}
