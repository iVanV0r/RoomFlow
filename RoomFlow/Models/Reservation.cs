using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomFlow.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Room")]
		public int RoomId { get; set; }  // Внешний ключ на номер

		public Room Room { get; set; }

		[Required]
		[StringLength(100)]
		public string GuestName { get; set; }  // Имя гостя

		[Required]
		[StringLength(100)]
		public string GuestEmail { get; set; } // Email гостя

		[Required]
		[StringLength(20)]
		public string GuestPhone { get; set; } // Телефон гостя

		[Required]
		[DataType(DataType.Date)]
		public DateTime CheckInDate { get; set; } // Дата заезда

		[Required]
		[DataType(DataType.Date)]
		public DateTime CheckOutDate { get; set; } // Дата выезда

		[Required]
		public decimal TotalPrice { get; set; } // Общая стоимость

		[StringLength(50)]
		public string Status { get; set; } = "Забронировано"; // Статус: "Забронировано", "Отменено", "Завершено"
	}

}
