using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomFlow.Models
{
	public class Employee
	{
		public int Id { get; set; }

		private string _fullName = string.Empty;
		[Required(ErrorMessage = "Введите полное имя сотрудника")]
		[StringLength(100, ErrorMessage = "ФИО не должно превышать 100 символов")]
		[Display(Name = "ФИО")]
		public string FullName
		{
			get => _fullName;
			set => _fullName = NormalizeInput(value);
		}

		private string _position = string.Empty;
		[Required(ErrorMessage = "Введите должность")]
		[StringLength(50, ErrorMessage = "Должность не должна превышать 50 символов")]
		[Display(Name = "Должность")]
		public string Position
		{
			get => _position;
			set => _position = NormalizeInput(value);
		}

		private string? _email;
		[EmailAddress(ErrorMessage = "Некорректный формат email")]
		[Display(Name = "Email")]
		public string? Email
		{
			get => _email;
			set => _email = NormalizeNullable(value);
		}

		private string? _phone;
		[Phone(ErrorMessage = "Некорректный номер телефона")]
		[Display(Name = "Телефон")]
		public string? Phone
		{
			get => _phone;
			set => _phone = NormalizeNullable(value);
		}

		[Column(TypeName = "decimal(10,2)")]
		[Range(0, 1000000, ErrorMessage = "Введите корректную сумму")]
		[Display(Name = "Оклад (₽)")]
		public decimal Salary { get; set; }

		[DataType(DataType.Date)]
		[Required(ErrorMessage = "Укажите дату найма")]
		[Display(Name = "Дата найма")]
		public DateTime HireDate { get; set; } = DateTime.Now.Date;

		private static string NormalizeInput(string value) =>
			string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();

		private static string? NormalizeNullable(string? value) =>
			string.IsNullOrWhiteSpace(value) ? null : value.Trim();

		public string GetFormattedSalary() => $"{Salary:N2} ₽";
		public string GetShortHireDate() => HireDate.ToShortDateString();
	}
}
