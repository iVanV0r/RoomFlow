using Microsoft.AspNetCore.Mvc;

namespace RoomFlow.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
