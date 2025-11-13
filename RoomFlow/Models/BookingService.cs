using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomFlow.Models
{
	public class BookingService
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Введите название услуги")]
		[StringLength(100)]
		[Display(Name = "Название услуги")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите описание услуги")]
		[StringLength(500)]
		[Display(Name = "Описание")]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите категорию услуги")]
		[StringLength(50)]
		[Display(Name = "Категория")]
		public string Category { get; set; } = string.Empty;

		[Column(TypeName = "decimal(10,2)")]
		[Range(0, 100000, ErrorMessage = "Введите корректную стоимость")]
		[Display(Name = "Стоимость (₽)")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Укажите тип ценообразования")]
		[StringLength(20)]
		[Display(Name = "Тип цены")]
		public string PriceType { get; set; } = "за услугу"; // за услугу, за час, за сутки

		[Display(Name = "Доступна для бронирования")]
		public bool IsAvailable { get; set; } = true;

		[Range(0, 1000, ErrorMessage = "Введите корректное время выполнения")]
		[Display(Name = "Время выполнения (мин)")]
		public int? DurationMinutes { get; set; }

		[Display(Name = "Требуется предварительное бронирование")]
		public bool RequiresReservation { get; set; }

		[Display(Name = "Включено в стоимость проживания")]
		public bool IsIncludedInStay { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Время начала")]
		public TimeSpan? StartTime { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Время окончания")]
		public TimeSpan? EndTime { get; set; }

		[Display(Name = "Максимальное количество гостей")]
		[Range(1, 100, ErrorMessage = "Введите корректное количество гостей")]
		public int? MaxGuests { get; set; }

		// Навигационные свойства
		public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
	}
}
