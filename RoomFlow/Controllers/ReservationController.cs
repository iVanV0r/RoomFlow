using Microsoft.AspNetCore.Mvc;

namespace RoomFlow.Controllers
{
	public class ReservationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
