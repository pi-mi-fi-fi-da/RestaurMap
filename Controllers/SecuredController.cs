using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurMap.Controllers;

[Authorize(Roles = "Admin")]
public class SecuredController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Operations/Index.cshtml");
    }
}
