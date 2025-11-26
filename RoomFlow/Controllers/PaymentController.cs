using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;

namespace RoomFlow.Controllers
{
	public class PaymentController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PaymentController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Payment()
		{
			return View();
		}

		// GET: все платежи
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
		{
			return await _context.Payments
				.Include(p => p.Booking)
				.ToListAsync();
		}

		// GET: платеж по id
		[HttpGet("{id}")]
		public async Task<ActionResult<Payment>> GetById(int id)
		{
			var payment = await _context.Payments
				.Include(p => p.Booking)
				.FirstOrDefaultAsync(p => p.Id == id);

			return payment == null ? NotFound() : payment;
		}

		// POST: создание платежа
		[HttpPost]
		public async Task<ActionResult<Payment>> Create(Payment payment)
		{
			_context.Payments.Add(payment);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
		}

		// PUT: обновление платежа
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Payment payment)
		{
			if (id != payment.Id) return BadRequest();
			_context.Entry(payment).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		// DELETE: удаление платежа
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var payment = await _context.Payments.FindAsync(id);
			if (payment == null) return NotFound();

			_context.Payments.Remove(payment);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		// GET: платежи по бронированию
		[HttpGet("booking/{bookingId}")]
		public async Task<ActionResult<IEnumerable<Payment>>> GetByBooking(int bookingId)
		{
			return await _context.Payments
				.Where(p => p.BookingId == bookingId)
				.ToListAsync();
		}

		// GET: платежи по статусу
		[HttpGet("status/{status}")]
		public async Task<ActionResult<IEnumerable<Payment>>> GetByStatus(PaymentStatus status)
		{
			return await _context.Payments
				.Where(p => p.Status == status)
				.ToListAsync();
		}

		// PUT: обновление статуса
		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateStatus(int id, PaymentStatus status)
		{
			var payment = await _context.Payments.FindAsync(id);
			if (payment == null) return NotFound();

			payment.Status = status;
			await _context.SaveChangesAsync();
			return NoContent();
		}

	}
}
