using Microsoft.EntityFrameworkCore;
using RoomFlow.Application.Interfaces;
using RoomFlow.Data;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomFlow.Application.Services
{
    public class ClientRoomService : IClientRoomService
    {
		private readonly IUnitOfWork _unitOfWork;

		public ClientRoomService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<Room>> GetAvailableRoomsAsync()
		{
			return await _unitOfWork.Rooms.GetAvailableRoomsAsync();
		}
	}
}
