using Microsoft.AspNetCore.Mvc;

namespace RoomFlow.Controllers
{
	public class BookingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
