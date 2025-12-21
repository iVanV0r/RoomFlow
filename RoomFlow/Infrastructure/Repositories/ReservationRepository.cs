using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Infrastructure.Repositories
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly ApplicationDbContext _context;

		public ReservationRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Reservation>> GetAllAsync()
		{
			return await _context.Reservations
				.ToListAsync();
		}

		public async Task<Reservation?> GetByIdAsync(int id)
		{
			return await _context.Reservations
				.AsNoTracking()
				.FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task AddAsync(Reservation booking)
		{
			await _context.Reservations.AddAsync(booking);
		}

		public void Update(Reservation booking)
		{
			_context.Reservations.Update(booking);
		}

		public void Remove(Reservation booking)
		{
			_context.Reservations.Remove(booking);
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _context.Reservations.AnyAsync(b => b.Id == id);
		}
	}
}
