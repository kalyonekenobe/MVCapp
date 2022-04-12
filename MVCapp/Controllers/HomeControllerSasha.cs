using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;
using MVCapp.ViewModels;
using System.Diagnostics;

namespace MVCapp.Controllers
{
	public class HomeControllerSasha : Controller
	{
		private readonly ILogger<HomeControllerSasha> _logger;

		public HomeControllerSasha(ILogger<HomeControllerSasha> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index(string name)
		{
			VisitorViewModel data = new VisitorViewModel()
			{
				Name = name
			};
			return View(data.Name == null ? "Index" : "Welcome", data);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}