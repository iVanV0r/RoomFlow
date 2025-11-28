using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EmployeesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Employees
		public async Task<IActionResult> Index()
		{
			var employees = await _context.Employees.ToListAsync();
			return View(employees);
		}

		// GET: Employees/Details/id
		public async Task<IActionResult> Details(int id)
		{
			var employee = await _context.Employees
				.FirstOrDefaultAsync(m => m.Id == id);

			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// GET: Employees/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Employees/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Employee employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			_context.Add(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Employees/Edit/id
		public async Task<IActionResult> Edit(int id)
		{
			var employee = await GetEmployeeById(id);

			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// POST: Employees/Edit/id
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Employee employee)
		{
			if (id != employee.Id)
				return NotFound();

			if (!ModelState.IsValid)
				return View(employee);

			_context.Update(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Employees/Delete/id
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _context.Employees
				.FirstOrDefaultAsync(m => m.Id == id);

			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// POST: Employees/DeleteComfirmed/id
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var employee = await GetEmployeeById(id);

			if (employee == null)
				return NotFound();

			_context.Remove(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private async Task<Employee?> GetEmployeeById(int id) =>
			await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
	}
}
