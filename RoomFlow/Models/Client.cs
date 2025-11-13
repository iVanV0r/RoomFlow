using System.ComponentModel.DataAnnotations;

namespace RoomFlow.Models
{
	public class Client
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Введите полное имя клиента")]
		[StringLength(150)]
		public string FullName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите email клиента")]
		[EmailAddress(ErrorMessage = "Некорректный формат email")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите дату рождения")]
		[DataType(DataType.Date)]
		public DateTime DateOfBorn { get; set; }

		[Required(ErrorMessage = "Укажите пол")]
		[StringLength(10)]
		public string Gender { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите номер паспорта")]
		[Range(1000000000, 9999999999, ErrorMessage = "Номер паспорта должен содержать 10 цифр")]
		public long Passport { get; set; }

		[Required(ErrorMessage = "Введите номер телефона")]
		[Phone(ErrorMessage = "Некорректный номер телефона")]
		public string Phone { get; set; } = string.Empty;
	}

}
