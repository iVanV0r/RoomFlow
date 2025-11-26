using System.ComponentModel.DataAnnotations;

namespace RoomFlow.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Поле Email или имя пользователя обязательно")]
		[Display(Name = "Email или имя пользователя")]
		public string EmailOrUsername { get; set; }

		[Required(ErrorMessage = "Поле Пароль обязательно")]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }
	}
}
