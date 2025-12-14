using Microsoft.AspNetCore.Mvc;
using RoomFlow.Application.Interfaces;
using System.Threading.Tasks;

namespace RoomFlow.Controllers
{
    public class ClientRoomsController : Controller
    {
        private readonly IClientRoomService _roomService;

        public ClientRoomsController(IClientRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var freeRooms = await _roomService.GetAvailableRoomsAsync();
            return View(freeRooms);
        }
    }
}
