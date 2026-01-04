using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class ShowDataController : Controller
{
    private readonly HttpClient _httpClient;

    public ShowDataController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
    }
    // Get All Tree With its type 
    public async Task<IActionResult> GetAllTreeWithTypeId(int typeId)
    {
        var response = await _httpClient.GetAsync($"Tree/GetByTypeId/{typeId}");

        if (!response.IsSuccessStatusCode)
            return Json(new { success = false, message = "❌ فشل في جلب النباتات" });

        var trees = await response.Content.ReadFromJsonAsync<List<TreeReadVM>>();
        return Json(new { success = true, data = trees });
    }


    // Get All Trees
    public async Task<IActionResult> AllTrees()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        return View(response);
    }
    
    //Get All Location With TypeId
    [HttpGet]
    public async Task<IActionResult> GetAllLocationWithTypeId(int typeId)
    {
        if (typeId <= 0)
            return Json(new { success = false, message = "❌ TypeId غير صالح" });

        var response = await _httpClient.GetAsync($"Location/GetByLocationTypeId/{typeId}");

        if (!response.IsSuccessStatusCode)
            return Json(new { success = false, message = "❌ فشل في جلب المواقع" });

        var locations = await response.Content.ReadFromJsonAsync<List<LocationNameReadVM>>();

        return Json(new { success = true, data = locations });
    }


    // Get All Locations
    public async Task<IActionResult> AllLocations()
    {
        var response = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
        return View(response);
    }

    // Delete Tree
    [HttpDelete]
    public async Task<IActionResult> DeleteTree(int id)
    {
        var response = await _httpClient.DeleteAsync($"Tree/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Json(new { success = true, message = "Tree deleted successfully." });
        }
        return Json(new { success = false, message = "❌ النبات مرتبط بسجل، احذف السجل أولاً" });
    }

    // Delete Location
    [HttpDelete]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var response = await _httpClient.DeleteAsync($"Location/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Json(new { success = true, message = "Location deleted successfully." });
        }

        return Json(new { success = false, message = "❌ الموقع مرتبط بسجل، احذف السجل أولاً" });
    }

    
    [HttpPut]
    public async Task<IActionResult> UpdateTree([FromBody] TreeUpdateVM treeUpdateVm)
    {
        var response = await _httpClient.PutAsJsonAsync("Tree/UpdateTree", treeUpdateVm);

        if (response.IsSuccessStatusCode)
            return Json(new { success = true, message = "Tree updated successfully." });

        return Json(new { success = false, message = "Reload the Page First." });
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateLocation([FromBody] LocationUpdateVM location)
    {
        var response = await _httpClient.PutAsJsonAsync("Location/UpdateLocation", location);

        if (response.IsSuccessStatusCode)
            return Json(new { success = true, message = "Location updated successfully." });

        return Json(new { success = false, message = "Reload the  Page First." });
    }

}