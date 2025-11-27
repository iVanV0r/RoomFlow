using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Models;


namespace RoomFlow.Controllers
{
    public class ClientRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Страница для клиентов — показываем свободные номера
        public async Task<IActionResult> Index()
        {
            var freeRooms = await _context.Rooms
                .Where(r => r.Status == RoomStatus.Available)
                .ToListAsync();

            return View(freeRooms);
        }
    }
}
