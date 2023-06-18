using Microsoft.AspNetCore.Mvc;
using RestaurMap.Models;
using RestaurMap.Models.View;
using RestaurMap.Services;
using System.Diagnostics;
using System.Threading;

namespace RestaurMap.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
    private readonly IRestaurantsService _restaurantsService;

    public HomeController(ILogger<HomeController> logger, IRestaurantsService restaurantsService)
	{
		_logger = logger;
        _restaurantsService = restaurantsService;
    }

    public async Task<ActionResult<List<Restaurant>>> Index(CancellationToken cancelationToken)
    {
        var restaurants = await _restaurantsService.GetAllAsync(cancelationToken);
        return View(restaurants);
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