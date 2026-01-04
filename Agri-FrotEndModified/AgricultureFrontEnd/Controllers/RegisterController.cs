using Microsoft.AspNetCore.Authorization;

namespace AgricultureFrontEnd.Controllers;
[Authorize(Roles = "Admin")]
public class RegisterController : Controller
{
    private readonly HttpClient _client;

    public RegisterController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
       var response = await _client.PostAsJsonAsync("User/Register/register", model);
       if (response.IsSuccessStatusCode)
       {
           TempData["Success"] = "تم التسجيل بنجاح ✅";
           return RedirectToAction(nameof(Register));
       }
       else
       {
           TempData["Error"] = "فشل التسجيل ❌";
           return RedirectToAction(nameof(Register));
       }
    }
    
   
}