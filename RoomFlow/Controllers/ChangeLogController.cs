using Microsoft.AspNetCore.Mvc;
using RoomFlow.Application.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
	public class ChangeLogController : Controller
	{
		private readonly IChangeLogService _changeLogService;

		public ChangeLogController(IChangeLogService changeLogService)
		{
			_changeLogService = changeLogService;
		}

		// GET: ChangeLog
		public async Task<IActionResult> Index()
		{
			var logs = await _changeLogService.GetAllAsync();
			return View(logs);
		}

		// GET: ChangeLog/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var log = await _changeLogService.GetByIdAsync(id);
			if (log == null)
				return NotFound();

			return View(log);
		}

		// GET: ChangeLog/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ChangeLog/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ChangeLog log)
		{
			if (!ModelState.IsValid)
				return View(log);

			await _changeLogService.AddAsync(log);
			return RedirectToAction(nameof(Index));
		}

		// GET: ChangeLog/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var log = await _changeLogService.GetByIdAsync(id);
			if (log == null)
				return NotFound();

			return View(log);
		}

		// POST: ChangeLog/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, ChangeLog log)
		{
			if (id != log.Id)
				return BadRequest();

			if (!ModelState.IsValid)
				return View(log);

			await _changeLogService.UpdateAsync(log);
			return RedirectToAction(nameof(Index));
		}

		// GET: ChangeLog/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			var log = await _changeLogService.GetByIdAsync(id);
			if (log == null)
				return NotFound();

			return View(log);
		}

		// POST: ChangeLog/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _changeLogService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
