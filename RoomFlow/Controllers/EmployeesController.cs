using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Application.Interfaces;
using RoomFlow.Data;
using RoomFlow.Models;
using System.Diagnostics;

namespace RoomFlow.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IEmployeeService _employeeService;

		public EmployeesController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		// GET: Employees
		public async Task<IActionResult> Index()
		{
			var employees = await _employeeService.GetAllAsync();
			return View(employees);
		}

		// GET: Employees/Details/id
		public async Task<IActionResult> Details(int id)
		{
			var employee = await _employeeService.GetByIdAsync(id);
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

			await _employeeService.AddAsync(employee);
			return RedirectToAction(nameof(Index));
		}

		// GET: Employees/Edit/id
		public async Task<IActionResult> Edit(int id)
		{
			var employee = await _employeeService.GetByIdAsync(id);
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

			await _employeeService.UpdateAsync(employee);
			return RedirectToAction(nameof(Index));
		}

		// GET: Employees/Delete/id
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _employeeService.GetByIdAsync(id);
			if (employee == null)
				return NotFound();

			return View(employee);
		}

		// POST: Employees/DeleteConfirmed/id
		[HttpPost, ActionName("DeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _employeeService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
