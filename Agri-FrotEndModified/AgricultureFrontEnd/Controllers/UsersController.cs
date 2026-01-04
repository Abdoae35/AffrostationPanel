using Microsoft.AspNetCore.Mvc;
using AgricultureFrontEnd.Models.Vm.UsersVM;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client;

    public UsersController(IHttpClientFactory httpClientFactory, HttpClient client)
    {
        _httpClientFactory = httpClientFactory;
        _client = client;
        _client.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
    }

    public async Task<IActionResult> ShowUsers()
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<UserReadVM>>(
            "http://agricultureachievement.runasp.net/User/GetAll");

        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> ResetPassword(int id)
    {
        var response = await _client.GetFromJsonAsync<UserReadVM>($"User/GetById/{id}");
        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(UserUpdateVM vm)
    {
        await _client.PutAsJsonAsync($"http://agricultureachievement.runasp.net/User/UpdateUser", vm);
        TempData["SuccessMessage"] = "Password was updated successfully!";
        return RedirectToAction(nameof(ShowUsers));
        
    }
    

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid user ID.");
        }

        using var client = _httpClientFactory.CreateClient();
        var url = $"http://agricultureachievement.runasp.net/User/DeleteUser/{id}";

        var response = await client.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest($"Failed to delete user with ID {id}.");
        }

        var users = await client.GetFromJsonAsync<List<UserReadVM>>("http://agricultureachievement.runasp.net/User/GetAll");
        return PartialView("_UsersTable", users ?? new List<UserReadVM>());
    }
}