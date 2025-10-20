using Microsoft.AspNetCore.Mvc;

namespace RoomFlow.Controllers
{
	public class AdminPanelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
