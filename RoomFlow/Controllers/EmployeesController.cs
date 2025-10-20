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

		// GET: Employees/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

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
		public async Task<IActionResult> Create(Employee employee)
		{
			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}

		// GET: Employees/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var employee = await _context.Employees.FindAsync(id);
			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// POST: Employees/Edit/5
		[HttpPost]
		public async Task<IActionResult> Edit(int id, Employee employee)
		{
			if (id != employee.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(employee);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeeExists(employee.Id))
						return NotFound();
					else
						throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}

		// GET: Employees/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var employee = await _context.Employees
				.FirstOrDefaultAsync(m => m.Id == id);

			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee != null)
			{
				_context.Employees.Remove(employee);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		private bool EmployeeExists(int id)
		{
			return _context.Employees.Any(e => e.Id == id);
		}
	}
}
