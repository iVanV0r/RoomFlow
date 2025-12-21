using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomFlow.Data;
using RoomFlow.Models;

namespace RoomFlow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RoomsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public RoomsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: получение всех комнат
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Room>>> GetAll()
		{
			return await _context.Rooms.ToListAsync();
		}

		// GET: получение номера по id
		[HttpGet("{id}")]
		public async Task<ActionResult<Room>> GetById(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room == null) return NotFound();
			return room;
		}

		// POST: создание номера
		[HttpPost]
		public async Task<ActionResult<Room>> Create(Room room)
		{
			_context.Rooms.Add(room);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
		}

		// PUT:метод обновления
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Room room)
		{
			if (id != room.Id) return BadRequest();
			_context.Entry(room).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Rooms.Any(e => e.Id == id))
					return NotFound();
				throw;
			}

			return NoContent();
		}

		// DELETE: метод удаления
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room == null) return NotFound();

			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		// ДОПОЛНИТЕЛЬНЫЕ МЕТОДЫ ДЛЯ НОМЕРОВ:

		// GET: получение только свободных номеров
		[HttpGet("available")]
		public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms()
		{
			return await _context.Rooms
				.Where(r => r.Status == RoomStatus.Available)
				.ToListAsync();
		}

		// GET: фильтрация по типу номеров
		[HttpGet("type/{roomType}")]
		public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByType(string roomType)
		{
			return await _context.Rooms
				.Where(r => r.RoomType == roomType)
				.ToListAsync();
		}

		// GET: поиск по этажу
		[HttpGet("floor/{floor}")]
		public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByFloor(int floor)
		{
			return await _context.Rooms
				.Where(r => r.Floor == floor)
				.ToListAsync();
		}

		// GET: поиск по цене (диапозон)
		[HttpGet("price-range")]
		public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
		{
			return await _context.Rooms
				.Where(r => r.BasePricePerNight >= min && r.BasePricePerNight <= max)
				.ToListAsync();
		}


		// GET: получение истории бронирования
		[HttpGet("{id}/bookings")]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetRoomBookings(int id)
		{
			var room = await _context.Rooms
				.Include(r => r.Bookings)
				.FirstOrDefaultAsync(r => r.Id == id);

			if (room == null) return NotFound();

			return Ok(room.Bookings);
		}
	}

}
