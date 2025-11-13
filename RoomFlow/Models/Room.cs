using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomFlow.Models
{
	public class Room
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Введите номер комнаты")]
		[StringLength(10)]
		[Display(Name = "Номер комнаты")]
		public string RoomNumber { get; set; } = string.Empty;

		[Required(ErrorMessage = "Выберите тип комнаты")]
		[StringLength(50)]
		[Display(Name = "Тип комнаты")]
		public string RoomType { get; set; } = "Стандарт";

		[Required(ErrorMessage = "Введите этаж")]
		[Range(1, 100, ErrorMessage = "Этаж должен быть от 1 до 100")]
		[Display(Name = "Этаж")]
		public int Floor { get; set; } = 1;

		[Required(ErrorMessage = "Введите количество мест")]
		[Range(1, 10, ErrorMessage = "Количество мест должно быть от 1 до 10")]
		[Display(Name = "Количество мест")]
		public int Capacity { get; set; } = 2;

		[Column(TypeName = "decimal(10,2)")]
		[Range(0, 1000000, ErrorMessage = "Введите корректную стоимость")]
		[Display(Name = "Базовая цена за ночь (₽)")]
		public decimal BasePricePerNight { get; set; }

		[Required(ErrorMessage = "Выберите статус комнаты")]
		[Display(Name = "Статус")]
		public RoomStatus Status { get; set; } = RoomStatus.Available;

		[StringLength(500)]
		[Display(Name = "Описание")]
		public string? Description { get; set; }

		[Display(Name = "Удобства")]
		public string? Amenities { get; set; }

		// Навигационное свойство для истории бронирований
		public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

		[DataType(DataType.DateTime)]
		[Display(Name = "Дата создания")]
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}

	// Перечисление для статусов комнаты
	public enum RoomStatus
	{
		[Display(Name = "Свободен")]
		Available,

		[Display(Name = "Забронирован")]
		Booked,

		[Display(Name = "Занят")]
		Occupied,

		[Display(Name = "На обслуживании")]
		Maintenance,

		[Display(Name = "Недоступен")]
		Unavailable
	}

	// Класс для истории бронирований/заселений
	public class RoomBooking
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "ID комнаты")]
		public int RoomId { get; set; }

		[Required]
		[Display(Name = "ID клиента")]
		public int ClientId { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Дата заселения")]
		public DateTime CheckInDate { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Дата выселения")]
		public DateTime CheckOutDate { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		[Display(Name = "Фактическая цена (₽)")]
		public decimal ActualPrice { get; set; }

		[Required]
		[Display(Name = "Статус брони")]
		public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

		[Display(Name = "Количество гостей")]
		public int GuestCount { get; set; } = 1;

		[StringLength(500)]
		[Display(Name = "Примечания")]
		public string? Notes { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Дата создания брони")]
		public DateTime BookingDate { get; set; } = DateTime.UtcNow;

		// Навигационные свойства
		public virtual Room? Room { get; set; }
		public virtual Client? Client { get; set; }
	}

	public enum BookingStatus
	{
		[Display(Name = "Подтверждено")]
		Confirmed,

		[Display(Name = "Заселен")]
		CheckedIn,

		[Display(Name = "Выселен")]
		CheckedOut,

		[Display(Name = "Отменено")]
		Cancelled,

		[Display(Name = "Неявка")]
		NoShow
	}
}
