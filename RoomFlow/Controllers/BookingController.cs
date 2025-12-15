using Microsoft.AspNetCore.Mvc;
using RoomFlow.Application.Interfaces;
using RoomFlow.Models;

public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    // ✅ ДОБАВЛЯЕМ КОНСТРУКТОР
    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllAsync();
        return View(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Booking booking)
    {
        if (!ModelState.IsValid)
            return View(booking);

        await _bookingService.AddAsync(booking);
        return RedirectToAction("Index");
    }
}
