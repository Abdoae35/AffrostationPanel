using Microsoft.AspNetCore.Mvc;
using AgricultureFrontEnd.Models.Vm.UsersVM;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly HttpClient _client;

    public UsersController(HttpClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> ShowUsers()
    {
        var response = await _client.GetFromJsonAsync<List<UserReadVM>>("User/GetAll");

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
        await _client.PutAsJsonAsync("User/UpdateUser", vm);
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

        var response = await _client.DeleteAsync($"User/DeleteUser/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest($"Failed to delete user with ID {id}.");
        }

        var users = await _client.GetFromJsonAsync<List<UserReadVM>>("User/GetAll");
        return PartialView("_UsersTable", users ?? new List<UserReadVM>());
    }
}