using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public AccountController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginVm dto)
    {
        var client = _clientFactory.CreateClient();

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7197/User/Login/login", content);

        if (response.IsSuccessStatusCode)
        {
            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponseVm>(resultJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                throw new Exception("Token from API is null or empty.");
            }

            if (result.Role != null)
            {
                if (dto.Email != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dto.Email),
                        new Claim(ClaimTypes.Role, result.Role),
                        new Claim("UserId", result.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
                }
            }

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Invalid credentials.";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Login");
    }
}
