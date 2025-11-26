using Microsoft.AspNetCore.Mvc;

namespace RoomFlow.Controllers
{
	public class Reservation : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
