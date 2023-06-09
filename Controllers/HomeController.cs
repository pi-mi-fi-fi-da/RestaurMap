using Microsoft.AspNetCore.Mvc;
using RestaurMap.Models;
using RestaurMap.Models.View;
using RestaurMap.Services;
using System.Diagnostics;

namespace RestaurMap.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly Scrapper _scrapper;

		public HomeController(ILogger<HomeController> logger, Scrapper scrapper)
		{
			_logger = logger;
			_scrapper = scrapper;
			_scrapper.Scrapp();
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}