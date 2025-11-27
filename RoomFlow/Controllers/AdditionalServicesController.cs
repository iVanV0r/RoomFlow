using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
	public class AdditionalServicesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdditionalServicesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: AdditionalServices
		public async Task<IActionResult> Index()
		{
			var services = await _context.AdditionalServices.AsNoTracking().ToListAsync();
			return View(services);
		}

		// GET: AdditionalServices/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null)
				return BadRequest("Не указан идентификатор услуги.");

			var service = await _context.AdditionalServices
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.Id == id);

			return service is null ? NotFound("Услуга не найдена.") : View(service);
		}

		// GET: AdditionalServices/Create
		public IActionResult Create() => View();

		// POST: AdditionalServices/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AdditionalService service)
		{
			if (!ModelState.IsValid)
				return View(service);

			try
			{
				service.CreatedAt = DateTime.UtcNow;
				_context.Add(service);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Дополнительная услуга успешно добавлена.";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Ошибка при добавлении услуги: {ex.Message}");
				return View(service);
			}
		}

		// GET: AdditionalServices/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null)
				return BadRequest("Не указан идентификатор услуги.");

			var service = await _context.AdditionalServices.FindAsync(id);
			return service is null ? NotFound("Услуга не найдена.") : View(service);
		}

		// POST: AdditionalServices/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, AdditionalService service)
		{
			if (id != service.Id)
				return BadRequest("ID услуги не совпадает.");

			if (!ModelState.IsValid)
				return View(service);

			try
			{
				_context.Update(service);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Данные услуги успешно обновлены.";
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AdditionalServiceExists(service.Id))
					return NotFound("Услуга не найдена при сохранении.");
				throw;
			}
		}

		// GET: AdditionalServices/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null)
				return BadRequest("Не указан идентификатор услуги.");

			var service = await _context.AdditionalServices
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.Id == id);

			return service is null ? NotFound("Услуга не найдена.") : View(service);
		}

		// POST: AdditionalServices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var service = await _context.AdditionalServices.FindAsync(id);
			if (service is not null)
			{
				_context.AdditionalServices.Remove(service);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Дополнительная услуга успешно удалена.";
			}

			return RedirectToAction(nameof(Index));
		}

		private bool AdditionalServiceExists(int id) =>
			_context.AdditionalServices.Any(s => s.Id == id);
	}
}

