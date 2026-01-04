using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;
using AgricultureFrontEnd.Models.Vm.AfforestationVM;

namespace AgricultureFrontEnd.Controllers
{
    [Authorize(Roles = "User")]
    public class UserRecordsController : Controller
    {
        private readonly HttpClient _client;

        public UserRecordsController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
        }

        public async Task<IActionResult> DataAdded()
        {
            // Get user ID
             var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");


            // Calculate last 30 days range
            var fromDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd");
            var toDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

            
          

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    ModelState.AddModelError("", "Invalid user ID.");
                    return View();
                }

            var searchRequest = new
            {
                fromDate,
                toDate,
                selectedUserId = userId
            };


            // Send POST with JSON body
            var response = await _client.PostAsJsonAsync("api/Afforestation/search", searchRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to load records.");
                return View();
            }

            var model = await response.Content.ReadFromJsonAsync<List<AfforestationReadVM>>();

            return View(model);
        }
    }
}