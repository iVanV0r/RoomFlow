using RoomFlow.Data;
using RoomFlow.Models;
using RoomFlow.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomFlow.Application.Services
{
    public class ClientRoomService : IClientRoomService
    {
        private readonly ApplicationDbContext _context;

        public ClientRoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAvailableRoomsAsync()
        {
            return await _context.Rooms
                .Where(r => r.Status == RoomStatus.Available)
                .ToListAsync();
        }
    }
}
