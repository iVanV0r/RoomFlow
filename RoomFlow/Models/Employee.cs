using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomFlow.Models
{
	public class Employee
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Введите полное имя сотрудника")]
		[StringLength(100)]
		[Display(Name = "ФИО")]
		public string FullName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите должность")]
		[StringLength(50)]
		[Display(Name = "Должность")]
		public string Position { get; set; } = string.Empty;

		[EmailAddress(ErrorMessage = "Некорректный формат email")]
		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Phone(ErrorMessage = "Некорректный номер телефона")]
		[Display(Name = "Телефон")]
		public string? Phone { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		[Range(0, 1000000, ErrorMessage = "Введите корректную сумму")]
		[Display(Name = "Оклад (₽)")]
		public decimal Salary { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Дата найма")]
		public DateTime HireDate { get; set; } = DateTime.UtcNow;
	}
}
