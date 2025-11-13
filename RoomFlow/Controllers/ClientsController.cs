using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients.AsNoTracking().ToListAsync();
            return View(clients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest("Не указан идентификатор клиента.");

            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return client is null ? NotFound("Клиент не найден.") : View(client);
        }

        // GET: Clients/Create
        public IActionResult Create() => View();

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
                return View(client);

            try
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Клиент успешно добавлен.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка при добавлении клиента: {ex.Message}");
                return View(client);
            }
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest("Не указан идентификатор клиента.");

            var client = await _context.Clients.FindAsync(id);
            return client is null ? NotFound("Клиент не найден.") : View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
                return BadRequest("ID клиента не совпадает.");

            if (!ModelState.IsValid)
                return View(client);

            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Данные клиента успешно обновлены.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                    return NotFound("Клиент не найден при сохранении.");
                throw;
            }
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest("Не указан идентификатор клиента.");

            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return client is null ? NotFound("Клиент не найден.") : View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client is not null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Клиент успешно удалён.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id) =>
            _context.Clients.Any(c => c.Id == id);
    }
}
