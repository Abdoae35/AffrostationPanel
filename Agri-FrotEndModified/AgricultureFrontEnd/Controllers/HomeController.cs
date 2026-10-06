using Microsoft.AspNetCore.Authorization;

namespace AgricultureFrontEnd.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        ViewBag.Role = role; 
        return View();
    }
}