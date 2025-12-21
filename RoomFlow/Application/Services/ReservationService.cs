using RoomFlow.Application.Interfaces;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Application.Services
{
	public class ReservationService : IReservationService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ReservationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<Reservation>> GetAllAsync()
		{
			return await _unitOfWork.Bookings.GetAllAsync();
		}

		public async Task<Reservation> GetByIdAsync(int id)
		{
			var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
			if (booking == null)
				throw new InvalidOperationException($"Бронирование с Id {id} не найдено.");
			return booking;
		}

		public async Task AddAsync(Reservation booking)
		{
			if (booking == null)
				throw new ArgumentNullException(nameof(booking));

			await _unitOfWork.Bookings.AddAsync(booking);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task UpdateAsync(Reservation booking)
		{
			if (booking == null)
				throw new ArgumentNullException(nameof(booking));

			_unitOfWork.Bookings.Update(booking);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
			if (booking == null)
				throw new InvalidOperationException($"Бронирование с Id {id} не найдено.");

			_unitOfWork.Bookings.Remove(booking);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
