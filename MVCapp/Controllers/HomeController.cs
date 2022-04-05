using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;
using MVCapp.ViewModels;
using System.Diagnostics;

namespace MVCapp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index(string name)
		{
			UserData data = new UserData()
			{
				Name = name
			};
			return View(data);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}