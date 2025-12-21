using System.ComponentModel.DataAnnotations;

namespace RoomFlow.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Введите email или имя пользователя")]
		[EmailAddress(ErrorMessage = "Неверный формат email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Введите пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
