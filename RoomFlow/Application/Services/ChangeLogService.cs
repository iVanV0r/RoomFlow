using RoomFlow.Application.Interfaces;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Application.Services
{
	public class ChangeLogService : IChangeLogService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ChangeLogService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<ChangeLog>> GetAllAsync()
		{
			return await _unitOfWork.ChangeLogs.GetAllAsync();
		}

		public async Task<ChangeLog?> GetByIdAsync(int id)
		{
			return await _unitOfWork.ChangeLogs.GetByIdAsync(id);
		}

		public async Task AddAsync(ChangeLog log)
		{
			if (log == null)
				throw new ArgumentNullException(nameof(log));

			await _unitOfWork.ChangeLogs.AddAsync(log);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task UpdateAsync(ChangeLog log)
		{
			if (log == null)
				throw new ArgumentNullException(nameof(log));

			_unitOfWork.ChangeLogs.Update(log);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var log = await _unitOfWork.ChangeLogs.GetByIdAsync(id);
			if (log == null)
				throw new InvalidOperationException("Запись журнала не найдена");

			_unitOfWork.ChangeLogs.Remove(log);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _unitOfWork.ChangeLogs.ExistsAsync(id);
		}
	}
}
