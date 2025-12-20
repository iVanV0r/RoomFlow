using Microsoft.AspNetCore.Mvc;
using RoomFlow.Data;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{

    public class RoomsListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: admin/RoomsList
        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }

        // GET: admin/RoomsList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/RoomsList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }
    }
}
