using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurMap.Models;
using RestaurMap.Services;
using System.Data;
using System.Security.Cryptography;
using System.Threading;

namespace RestaurMap.Controllers;

[Authorize(Roles = "Admin")]
public class RestaurantController : Controller
{
    private readonly IRestaurantsService _restaurantService;
    public RestaurantController(IRestaurantsService restaurantsService)
    {
        _restaurantService = restaurantsService;   
    }
    // GET: RestaurantController
    public async Task<ActionResult> Index(CancellationToken cancellationToken)
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        restaurants = await _restaurantService.GetAllAsync(cancellationToken);
        return View(restaurants);
    }
    // GET: RestaurantController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: RestaurantController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: RestaurantController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Restaurant restaurant)
    {
        await _restaurantService.CreateAsync(restaurant);
        return RedirectToAction("Index");
    }
    // POST: RestaurantController/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Restaurant restaurant)
    {
        await _restaurantService.EditOneAsync(id, restaurant);
        return RedirectToAction("Index");
    }
    // GET: RestaurantController/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var toRemove = await _restaurantService.GetOneAsync(id, cancellationToken);
        return View(toRemove);
    }

    // POST: RestaurantController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Restaurant restaurant, CancellationToken cancellationToken)
    {
        await _restaurantService.RemoveAsync(restaurant.Id ,cancellationToken);
        return RedirectToAction("Index");
    }
}
