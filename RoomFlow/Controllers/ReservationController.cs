using Microsoft.AspNetCore.Mvc;
using RoomFlow.Application.Interfaces;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
	public class ReservationController : Controller
	{
		private readonly IReservationService _bookingService;

		public ReservationController(IReservationService bookingService)
		{
			_bookingService = bookingService;
		}

		public async Task<IActionResult> Index()
		{
			var bookings = await _bookingService.GetAllAsync();
			return View(bookings);
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		public async Task<IActionResult> Success()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Reservation booking)
		{
			if (!ModelState.IsValid)
				return View(booking);

			await _bookingService.AddAsync(booking);
			return RedirectToAction("Success");
		}
	}
}
